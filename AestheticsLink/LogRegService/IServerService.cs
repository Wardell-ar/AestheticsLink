using LogRegService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;

namespace LogRegService
{
    public interface IServerService
    {
        Task<SERVER> CheckLogin(LoginDto login);

    }
}
