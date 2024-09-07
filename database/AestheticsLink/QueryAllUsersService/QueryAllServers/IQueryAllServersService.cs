using QueryAllUsersService.QueryAllServers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAllUsersService.QueryAllServers
{
    public interface IQueryAllServersService
    {
        Task<List<QueryServersDto>> GetServersByCriteria(Dictionary<string, object> criteria);
    }
}
