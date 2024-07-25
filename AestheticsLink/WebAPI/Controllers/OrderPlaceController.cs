using LogRegService.Dto;
using Microsoft.AspNetCore.Mvc;
using OrderService;
using OrderService.Dto;
using System.DirectoryServices.Protocols;
using WebCommon.Database;

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
            _logger.LogInformation("Received place order request: UID={UID}", placeOrderDto.CUS_ID);

            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();

                // 检查信息是否有效
                bool orderState = _orderService.CheckProject(placeOrderDto);

                if (orderState)
                {
                    _orderService.AddBill(placeOrderDto);
                    // 下单成功，返回医院列表
                    var response = _orderService.GetHospital();
                    //事物提交
                    DbContext.db.Ado.CommitTran();
                    return Ok(response);
                }
                else
                {
                    // 下单失败，有项目不在数据库中，出现异常
                    _logger.LogInformation("Received error order request: UID={UID}", placeOrderDto.CUS_ID);
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest(new { error = "项目或客户信息有误，或是客户余额不足" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the order request: UID={UID}", placeOrderDto.CUS_ID);
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
        //选择医院和优惠券，并为本次订单的手术排班
        [HttpPost("CheckRest")]
        public IActionResult ChooseRest([FromBody] RestChoiceDto restChoiceDto)
        {
            _logger.LogInformation("Received choose order's hospital request: UID={UID}", restChoiceDto.CUS_ID);

            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                //检验信息是否对应
                bool orderState = _orderService.CheckInfo(restChoiceDto);
                if(orderState) 
                {
                    //医生和手术时间、手术室自动分配，下单后优惠券核销，扣除余额


                    //核销优惠券
                    DbContext.db.Ado.ExecuteCommand(
                        "DELETE FROM CUS_COU WHERE COU_ID = :couID",
                        new
                        {
                            couID = restChoiceDto.COU_ID,
                        });
                    //减去付款金额
                    DbContext.db.Ado.ExecuteCommand(
                        "UPDATE CUSTOMER SET BALANCE = BALANCE - :balance WHERE CUS_ID = :cusID",
                        new
                        {
                            balance = restChoiceDto.PAID_AMOUNT,
                            cusID = restChoiceDto.CUS_ID,
                        });
                    //事物提交
                    DbContext.db.Ado.CommitTran();
                    return Ok();
                }
                else
                {
                    // 医院选择失败，医院客户订单不匹配，出现异常
                    _logger.LogInformation("Received error order request: UID={UID}", restChoiceDto.CUS_ID);
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return BadRequest(new { error = "项目或客户信息有误" });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the order request: UID={UID}", restChoiceDto.CUS_ID);
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
