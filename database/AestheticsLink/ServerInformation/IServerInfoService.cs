using ServerInformation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;

namespace ServerInformation
{
    public interface IServerInfoService
    {
        Task<SERVER> CheckSignin(ServerInfoRequest serverinforequest);
    }
}
