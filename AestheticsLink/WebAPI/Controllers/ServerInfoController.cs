using Dm.filter;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LogRegService;
using LogRegService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerInformation;
using ServerInformation.Dto;
using ServerSigninService.Signin.Dto;
using SqlSugar;
using System.Dynamic;
using WebCommon.Database;
using WebModel.Entity;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ServerInfoController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IServerInfoService _serverInfoService;

        public ServerInfoController(IServerInfoService serverInfoService, ILogger<LoginController> logger)
        {
            _serverInfoService = serverInfoService;
            _logger = logger;
        }

        private int CalculateAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age;
        }

        [HttpPost("ServerInfo")]
        public async Task<IActionResult> ServerInfo([FromBody] ServerInfoRequest serverinforequest)
        {
            try
            {
                //事物开始
                DbContext.db.Ado.BeginTran();
                ServerInfoRequest serverInfoRequest = new ServerInfoRequest
                {
                    id = serverinforequest.id
                };
                var server = await _serverInfoService.CheckSignin(serverInfoRequest);

                if (server != null)
                {
                    _logger.LogInformation("Received login request: SER_ID={SER_ID}", serverinforequest.id);
                    var ser_id = server.SER_ID;
                    
                    if (string.IsNullOrEmpty(ser_id))
                    {
                        _logger.LogError("ser_id is null or empty");
                        return Unauthorized();
                    }

                    var serverinfo = await DbContext.db.Queryable<SERVER, OPERATE, HOSPITAL, BILL, PROJECT>((s, o, h, b, p) => new object[]
                    {
                        JoinType.Left, s.SER_ID == o.SER_ID,
                        JoinType.Left, s.HOS_ID == h.HOS_ID,
                        JoinType.Left, o.BILL_ID == b.BILL_ID,
                        JoinType.Left, o.PROJ_ID == p.PROJ_ID
                    })
                    .Where((s, o, h, b, p) => s.SER_ID == ser_id)
                    .Select((s, o, h, b, p) => new
                    {
                        id = s.SER_ID,
                        name = s.NAME,
                        gender = s.GENDER,
                        birthday = s.BIRTHDAY,
                        title = s.POSITION,
                        hospital = h.NAME,
                        attendanceStatus = s.IS_WORK_TODAY,
                        order_id = b.BILL_ID,
                        order_name = p.NAME,
                        order_date = (DateTime?)b.FOUND_DATE  // 确保是 DateTime? 类型
                    })
                    .ToListAsync();

                    if (serverinfo == null || !serverinfo.Any())
                    {
                        _logger.LogWarning("No server info found for ser_id: {ser_id}", ser_id);
                        DbContext.db.Ado.RollbackTran(); // 事务回滚
                        return NotFound();
                    }

                    var firstItem = serverinfo.First();

                    var serverInfo = new ServerInfoDto
                    {
                        id = firstItem.id,
                        name = firstItem.name,
                        gender = firstItem.gender,
                        age = CalculateAge((DateTime)firstItem.birthday),
                        title = firstItem.title,
                        hospital = firstItem.hospital,
                        attendanceStatus = firstItem.attendanceStatus.ToString(),
                        orderData = serverinfo.Select(item => new OrderData
                        {
                            order_id = item.order_id,
                            order_name = item.order_name,
                            order_year = item.order_date != null ? item.order_date.Value.Year.ToString() : "N/A",
                            order_month = item.order_date != null ? item.order_date.Value.Month.ToString() : "N/A",
                            order_day = item.order_date != null ? item.order_date.Value.Day.ToString() : "N/A"
                        }).ToList()
                    };

                    // 事务提交
                    DbContext.db.Ado.CommitTran();
                    return Ok(serverInfo);
                }
                else
                {
                    //事物回滚
                    DbContext.db.Ado.RollbackTran();
                    return NotFound("0");
                }
            }
            catch (Exception ex)
            {
                //事物回滚
                DbContext.db.Ado.RollbackTran();
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
