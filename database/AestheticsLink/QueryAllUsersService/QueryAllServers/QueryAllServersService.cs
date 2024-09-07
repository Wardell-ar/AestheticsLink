using QueryAllUsersService.QueryAllServers.Dto;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace QueryAllUsersService.QueryAllServers
{
    public class QueryAllServersService : IQueryAllServersService
    {
        public async Task<List<QueryServersDto>> GetServersByCriteria(Dictionary<string, object> criteria)
        {
            var query = DbContext.db.Queryable<SERVER, HOSPITAL, DEPARTMENT>((s, h, d) =>
                new object[]
                {
                    JoinType.Left, s.HOS_ID == h.HOS_ID,
                    JoinType.Left, s.DEP_ID == d.DEP_ID
                });
            foreach (var criterion in criteria)
            {
                switch (criterion.Key.ToLower())
                {
                    case "phone_num":
                        query = query.Where(s => s.PHONE_NUM == criterion.Value.ToString());
                        break;
                    case "name":
                        query = query.Where(s => s.NAME == criterion.Value.ToString());
                        break;
                    case "ser_id":
                        query = query.Where(s => s.SER_ID == criterion.Value.ToString());
                        break;
                }
            }

            var servers = await query.Select((s, h, d) => new QueryServersDto
            {
                name = s.NAME,
                ser_id = s.SER_ID,
                gender = s.GENDER,
                joined_date = s.JOINED_DATE,
                phone_num = s.PHONE_NUM,
                salary = s.BASICSALARY,
                position = s.POSITION,
                hos_name = h.NAME,
                dep_name = d.NAME
            }).ToListAsync();

            // 在内存中计算工资上下限
            foreach (var server in servers)
            {
                // 确保 salarymin 和 salarymax 的转换是正确的
                int salaryMin = criteria.ContainsKey("salarymin") ? criteria["salarymin"].ObjToInt() : 0;
                int salaryMax = criteria.ContainsKey("salarymax") ? criteria["salarymax"].ObjToInt() : int.MaxValue;

                servers = servers.Where(s => s.salary >= salaryMin && s.salary <= salaryMax).ToList();
            }

            return servers;

        }
    }
}
