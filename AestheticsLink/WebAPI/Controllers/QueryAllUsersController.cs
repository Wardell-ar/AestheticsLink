using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryAllUsersService.QueryAllCustomers;
using QueryAllUsersService.QueryAllCustomers.Dto;
using ServerInformation.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class QueryAllUsersController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IQueryAllCustomersService _queryAllCustomerService;

        public QueryAllUsersController(IQueryAllCustomersService queryAllCustomersService, ILogger<LoginController> logger)
        {
            _queryAllCustomerService = queryAllCustomersService;
            _logger = logger;
        }

        [HttpPost("QueryCustomers")]
        public async Task<ActionResult<List<QueryCustomersDto>>> QueryCustomers([FromBody] Dictionary<string, object> criteria)
        {
            _logger.LogInformation("Received login request!");

            var customers = await _queryAllCustomerService.GetCustomersByCriteria(criteria);
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }

            return Ok(customers);
        }
    }
}
