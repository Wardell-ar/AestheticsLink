﻿using Dm.filter;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LogRegService;
using LogRegService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerInformation;
using ServerInformation.Dto;
using ServerSigninService.Signin.Dto;
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
                ServerInfoRequest serverInfoRequest = new ServerInfoRequest();
                serverInfoRequest.id = serverinforequest.id;
                var server = _serverInfoService.CheckSignin(serverInfoRequest).Result;
                if (server != null)
                {
                    _logger.LogInformation("Received login request: SER_ID={SER_ID}", serverinforequest.id);
                    var ser_id = server.SER_ID;
                    if (string.IsNullOrEmpty(ser_id))
                    {
                        _logger.LogError("ser_id is null or empty");
                        return Unauthorized();
                    }
                    string sql = "SELECT s.SER_ID as id, s.NAME as name, s.GENDER as gender, s.BIRTHDAY as birthday, s.POSITION as title, h.NAME as hospital, s.IS_WORK_TODAY as attendanceStatus, b.BILL_ID as order_id, p.NAME as order_name, b.FOUND_DATE as order_date " +
                        "FROM SERVER s " +
                        "JOIN OPERATE o ON s.SER_ID = o.SER_ID " +
                        "JOIN HOSPITAL h ON h.HOS_ID = s.HOS_ID " +
                        "JOIN BILL b ON o.BILL_ID = b.BILL_ID " +
                        "JOIN PROJECT p ON o.PROJ_ID = p.PROJ_ID " +
                        "WHERE s.SER_ID = @ser_id";
                    var serverinfo = DbContext.db.Ado.SqlQuery<dynamic>(sql, new { server.SER_ID });

                    if (serverinfo == null || !serverinfo.Any())
                    {
                        _logger.LogWarning("No server info found for ser_id: {ser_id}", ser_id);
                        return NotFound();
                    }
                    // 将 serverinfo 转换为 List<object>
                    List<object> serverinfoList = serverinfo as List<object>;

                    // 遍历每个动态对象
                    foreach (var item in serverinfoList)
                    {
                        // 将动态对象转换为 IDictionary<string, object>
                        IDictionary<string, object> dictionaryItem = item as IDictionary<string, object>;

                        // 遍历动态对象的属性
                        foreach (var property in dictionaryItem)
                        {
                            Console.WriteLine($"{property.Key}: {property.Value}");
                        }
                    }
                    var firstItem = serverinfo.First();

                    var serverInfo = new ServerInfoDto
                    {
                        id = firstItem.ID,
                        name = firstItem.NAME,
                        gender = firstItem.GENDER,
                        age = CalculateAge((DateTime)firstItem.BIRTHDAY),
                        title = firstItem.TITLE,
                        hospital = firstItem.HOSPITAL,
                        attendanceStatus = firstItem.ATTENDANCESTATUS.ToString(),
                        orderData = serverinfo.Select(item => new OrderData
                        {
                            order_id = item.ORDER_ID,
                            order_name = item.ORDER_NAME,
                            order_year = ((DateTime)item.ORDER_DATE).Year.ToString(),
                            order_month = ((DateTime)item.ORDER_DATE).Month.ToString(),
                            order_day = ((DateTime)item.ORDER_DATE).Day.ToString()
                        }).ToList()
                    };
                    //事物提交
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
            /*ServerInfoRequest serverInfoRequest = new ServerInfoRequest();
            serverInfoRequest.id = serverinforequest.id;
            var server = _serverInfoService.CheckSignin(serverInfoRequest).Result;
            if (server != null)
            {
                _logger.LogInformation("Received login request: SER_ID={SER_ID}", serverinforequest.id);
                var ser_id = server.SER_ID;
                if (string.IsNullOrEmpty(ser_id))
                {
                    _logger.LogError("ser_id is null or empty");
                    return Unauthorized();
                }
                string sql = "SELECT s.SER_ID as id, s.NAME as name, s.GENDER as gender, s.BIRTHDAY as birthday, s.POSITION as title, h.NAME as hospital, s.IS_WORK_TODAY as attendanceStatus, b.BILL_ID as order_id, p.NAME as order_name, b.FOUND_DATE as order_date " +
                    "FROM SERVER s " +
                    "JOIN OPERATE o ON s.SER_ID = o.SER_ID " +
                    "JOIN HOSPITAL h ON h.HOS_ID = s.HOS_ID " +
                    "JOIN BILL b ON o.BILL_ID = b.BILL_ID " +
                    "JOIN PROJECT p ON o.PROJ_ID = p.PROJ_ID " +
                    "WHERE s.SER_ID = @ser_id";
                var serverinfo = DbContext.db.Ado.SqlQuery<dynamic>(sql, new { server.SER_ID });

                if (serverinfo == null || !serverinfo.Any())
                {
                    _logger.LogWarning("No server info found for ser_id: {ser_id}", ser_id);
                    return NotFound();
                }
                // 将 serverinfo 转换为 List<object>
                List<object> serverinfoList = serverinfo as List<object>;

                // 遍历每个动态对象
                foreach (var item in serverinfoList)
                {
                    // 将动态对象转换为 IDictionary<string, object>
                    IDictionary<string, object> dictionaryItem = item as IDictionary<string, object>;

                    // 遍历动态对象的属性
                    foreach (var property in dictionaryItem)
                    {
                        Console.WriteLine($"{property.Key}: {property.Value}");
                    }
                }
                var firstItem = serverinfo.First();

                var serverInfo = new ServerInfoDto
                {
                    id = firstItem.ID,
                    name = firstItem.NAME,
                    gender = firstItem.GENDER,
                    age = CalculateAge((DateTime)firstItem.BIRTHDAY),
                    title = firstItem.TITLE,
                    hospital = firstItem.HOSPITAL,
                    attendanceStatus = firstItem.ATTENDANCESTATUS.ToString(),
                    orderData = serverinfo.Select(item => new OrderData
                    {
                        order_id = item.ORDER_ID,
                        order_name = item.ORDER_NAME,
                        order_year = ((DateTime)item.ORDER_DATE).Year.ToString(),
                        order_month = ((DateTime)item.ORDER_DATE).Month.ToString(),
                        order_day = ((DateTime)item.ORDER_DATE).Day.ToString()
                    }).ToList()
                };

                return Ok(serverInfo);
            }
            else
            {
                return Ok("0");
            }*/
        }
    }
}