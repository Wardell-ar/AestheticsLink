using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebService.User.Dto;
using WenCommon.Db;

namespace WebService.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        //登录
        public async Task<CUSTOMER> CheckLogin(LoginDto login)
        {
            return await DbContext.db.Queryable<CUSTOMER>().FirstAsync(m => m.CUS_ID.Equals(login.CUS_ID) && m.PASSWORD.Equals(login.PASSWORD));
        }
        //注册
        public UserDto AddUser(InputUserDto login)
        {
            CUSTOMER user = TransInputDto(login);
            if (!DbContext.db.Queryable<CUSTOMER>().Any(m => m.PHONE_NUM.Equals(login.PHONE_NUM)))
            {
                DbContext.db.Insertable(user).ExecuteCommand();
                return new UserDto
                {
                    CUS_ID = user.CUS_ID,  
                    PHONE_NUM = user.PHONE_NUM,
                    FOUND_DATE = user.FOUND_DATE,
                    BIRTHDAY = user.BIRTHDAY,
                    GENDER = user.GENDER,
                    NAME = user.NAME,
                    EX = user.EX,
                    PASSWORD = user.PASSWORD,
                    BALANCE = user.BALANCE,
                    VIPLEVEL = user.VIPLEVEL,
                };
            }
            else throw new Exception("手机号已存在");
        }
        private CUSTOMER TransInputDto(InputUserDto input)
        {
            var user = new CUSTOMER();
            var date = DateTime.Now;

            user.CUS_ID = input.CUS_ID;
            user.PHONE_NUM = input.PHONE_NUM;
            user.FOUND_DATE = date;
            user.BIRTHDAY = input.BIRTHDAY;
            user.GENDER = input.GENDER;
            user.NAME = input.NAME;
            user.EX = 0;
            user.PASSWORD = input.PASSWORD;
            user.BALANCE = 0;
            user.VIPLEVEL = "Gold";

            return user;
        }

    }
}