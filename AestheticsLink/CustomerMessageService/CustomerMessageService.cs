using CustomerMessageService.Dto;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;


namespace CustomerMessageService
{
    public class CustomerMessageService : ICustomerMessageService
    {


        public async Task<CustomerDto> GetCustomerInfoAsync(string cusId)
        {
            // 查询顾客基本信息
            var customer = await DbContext.db.Queryable<CUSTOMER>()
                                           .Where(c => c.CUS_ID == cusId)
                                           .FirstAsync();

            if (customer == null)
            {
                return null;
            }

            // 查询顾客的优惠券
            var coupons = await DbContext.db.Queryable<COUPON, CUS_COU>((c, cc) => new object[] {
                                                    JoinType.Inner, c.COU_ID == cc.COU_ID })
                                          .Where((c, cc) => cc.CUS_ID == cusId)
                                          .Select((c, cc) => new CouponDto
                                          {
                                              Name = c.NAME,
                                              Type = c.TYPE,
                                              Price = c.PRICE
                                          })
                                          .ToListAsync();

            // 查询顾客的手术信息
            var operations = await DbContext.db.Queryable<OPERATE, BILL, SERVER, PROJECT>((op, b, s, p) => new object[] {
                                                    JoinType.Inner, op.BILL_ID == b.BILL_ID,
                                                    JoinType.Inner, op.SER_ID == s.SER_ID,
                                                    JoinType.Inner, op.PROJ_ID == p.PROJ_ID })
                                             .Where((op, b) => b.CUS_ID == cusId)
                                             .Select((op, b, s, p) => new OperationDto
                                             {
                                                 ServerName = s.NAME,
                                                 ProjectName = p.NAME,
                                                 Price = p.PRICE,
                                                 FoundDate = op.FOUND_DATE
                                             })
                                             .ToListAsync();

            // 构造返回的 DTO
            return new CustomerDto
            {
                CusId = customer.CUS_ID,
                Name = customer.NAME,
                PhoneNum = customer.PHONE_NUM,
                FoundDate = customer.FOUND_DATE,
                Birthday = customer.BIRTHDAY,
                Gender = customer.GENDER,
                Balance = customer.BALANCE,
                VIPLevel = customer.VIPLEVEL,
                Coupons = coupons,
                Operations = operations
            };
        }
    }
}
