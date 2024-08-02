using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OperateService.Dto;
using OrderService;
using OrderService.Dto;
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
                if (orderState)
                {
                    var timeNow = DateTime.Now;
                    //医生和手术时间、手术室自动分配
                    //找到可用的时间和手术室,从当前时间往后找找到第一个可用时间
                    var availableTimes = DbContext.db.Ado.SqlQuery<OperateTimeDto>(
                        "SELECT OP_TIME_ID FROM OPERATE_TIME WHERE STATUS = :status AND START_TIME > :time AND DAY = :day ",
                        new
                        {
                            status = "0",
                            time = timeNow,
                            day = timeNow.Date,
                        });
                    //选择两个链表里的第一个
                    var project = DbContext.db.Ado.SqlQuery<string>("SELECT PROJ_ID FROM BILL NATURAL JOIN OPERATE WHERE BILL_ID = :billID",
                        new { billID = restChoiceDto.BILL_ID });
                    int i = 0;
                    foreach (var proj in project)
                    {
                        DbContext.db.Ado.ExecuteCommand(
                        "UPDATE OPERATE SET OP_TIME_ID = :timeID WHERE BILL_ID = :billID AND PROJ_ID = :projID",
                        new
                        {
                            timeID = availableTimes[i].OP_TIME_ID,
                            billID = restChoiceDto.BILL_ID,
                            projID = proj,
                        });
                        DbContext.db.Ado.ExecuteCommand(
                        "UPDATE OPERATE_TIME SET STATUS = :status WHERE OP_TIME_ID = :timeID",
                        new
                        {
                            status = "1",
                            timeID = availableTimes[i].OP_TIME_ID,
                        });

                        //找到在可用时间可用的医生
                        var availableDoctors = DbContext.db.Ado.SqlQuery<SERVER>(
                            "SELECT SER_ID " +
                              "FROM SERVER " +
                              "WHERE SER_ID NOT IN (" +
                                  "SELECT SER_ID " +
                                  "FROM OPERATE " +
                                  "NATURAL JOIN OPERATE_TIME " +
                                  "WHERE STATUS = :status AND START_TIME = :time AND DAY = :day AND HOS_ID = :hosID) ",
                            new
                            {
                                status = "1",
                                time = availableTimes[i].START_TIME,
                                day = availableTimes[i].DAY,
                                hosID = restChoiceDto.HOS_ID,
                            });
                        DbContext.db.Ado.ExecuteCommand(
                            "UPDATE OPERATE SET SER_ID = :serID WHERE BILL_ID = :billID AND PROJ_ID = :projID",
                            new
                            {
                                serID = availableDoctors[i].SER_ID,
                                billID = restChoiceDto.BILL_ID,
                                projID = proj,
                            });
                        i++;
                    }
                    //核销优惠券
                    if (restChoiceDto.COU_ID != null)
                    {
                        DbContext.db.Ado.ExecuteCommand(
                        "DELETE FROM CUS_COU WHERE COU_ID = :couID",
                        new
                        {
                            couID = restChoiceDto.COU_ID,
                        });
                    }
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
