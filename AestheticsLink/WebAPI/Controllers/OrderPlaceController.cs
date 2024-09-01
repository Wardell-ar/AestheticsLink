using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OperateService.Dto;
using OrderService;
using OrderService.Dto;
using SqlSugar;
using WebCommon.Database;
using WebModel.Entity;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class OrderPlaceController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<LoginController> _logger;// 日志
        public OrderPlaceController(IOrderService orderService, ILogger<LoginController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost("CheckOrder")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderDto placeOrderDto)
        {
            _logger.LogInformation("Received place order request: UID={UID}", placeOrderDto.clientid);

            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                // 检查信息是否有效
                bool orderState = _orderService.CheckProject(placeOrderDto);

                if (orderState)
                {
                    var order = _orderService.AddBill(placeOrderDto);
                    //核销优惠券
                    if (order.COU_ID != null)
                    {
                        DbContext.db.Ado.ExecuteCommand(
                        "DELETE FROM CUS_COU WHERE COU_ID = :couID",
                        new
                        {
                            couID = order.COU_ID,
                        });
                    }
                    //会员打折
                    var viplevel = DbContext.db.Ado.SqlQuerySingle<MEMBER>(
                    "SELECT * FROM CUSTOMER NATURAL JOIN MEMBER WHERE CUS_ID = :id ",
                    new
                    {
                        id = placeOrderDto.clientid,
                    });
                    decimal pay = order.PAID_AMOUNT * viplevel.DISCOUNT;
                    //减去付款金额
                    DbContext.db.Ado.ExecuteCommand(
                        "UPDATE CUSTOMER SET BALANCE = BALANCE - :balance WHERE CUS_ID = :cusID",
                        new
                        {
                            balance = pay,
                            cusID = order.CUS_ID,
                        });
                    //事物提交
                    DbContext.db.Ado.CommitTran();
                    return Ok("1");
                }
                else
                {
                    // 下单失败，有项目不在数据库中，出现异常
                    _logger.LogInformation("Received error order request: UID={UID}", placeOrderDto.clientid);
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return Ok("0");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the order request: UID={UID}", placeOrderDto.clientid);
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return Ok("0");
            }
        }

        [HttpPost("Cus_Coupons")]
        public IActionResult Cus_Coupons([FromBody] Cus_CouReceptionDto cus_CouReception)
        {
            _logger.LogInformation("Received Cus_Coupons request: UID={UID}", cus_CouReception.id);

            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                // 检查顾客id是否有效
                bool cusState = _orderService.CheckCustomer(cus_CouReception);

                if (cusState)
                {
                    // 检查成功，返回优惠券列表
                    var response = DbContext.db.Queryable<CUS_COU, COUPON>((cc, c) => new object[]
                    {
                        JoinType.Inner, cc.COU_ID == c.COU_ID
                    })
                    .Where(cc => cc.CUS_ID == cus_CouReception.id)
                    .Select((cc, c) => new Cus_CouResponseDto
                    {
                        id = cc.COU_ID,
                        name = c.NAME
                    }).ToList();

                    // 事物提交
                    DbContext.db.Ado.CommitTran();
                    return Ok(response);
                }
                else
                {
                    // 下单失败，有项目不在数据库中，出现异常
                    _logger.LogInformation("Received error order request: UID={UID}", cus_CouReception.id);
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest(new { error = "客户信息有误" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the order request: UID={UID}", cus_CouReception.id);
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

    }
}
