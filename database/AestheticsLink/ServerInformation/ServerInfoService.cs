using ServerInformation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace ServerInformation
{
    public class ServerInfoService : IServerInfoService
    {
        public async Task<SERVER> CheckSignin(ServerInfoRequest serverinforequest)
        {
            return await DbContext.db.Queryable<SERVER>().FirstAsync(s => s.SER_ID.Equals(serverinforequest.id));
        }
    }
}
