using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.User.Dto;

namespace WebService.Config
{
    //管理映射关系
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Users, UserDto>();
            CreateMap<InputUserDto, Users>();
        }
    }
}
