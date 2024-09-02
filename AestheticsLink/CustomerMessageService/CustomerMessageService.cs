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
                Year = customer.BIRTHDAY.Year.ToString(),      // 年
                Month = customer.BIRTHDAY.Month.ToString(),    // 月
                Day = customer.BIRTHDAY.Day.ToString(),        // 日
                Gender = customer.GENDER,
                VIPLevel = customer.VIPLEVEL,
                Password = customer.PASSWORD
            };
        }

        public async Task<int> UpdateCustomerPasswordAsync(string cusId, string newPassword)
        {
            // 查询顾客
            var customer = await DbContext.db.Queryable<CUSTOMER>()
                                           .Where(c => c.CUS_ID == cusId)
                                           .FirstAsync();

            if (customer == null)
            {
                return 0; // 修改失败，顾客不存在
            }

            // 更新密码
            customer.PASSWORD = newPassword;

            // 保存修改
            var updateResult = await DbContext.db.Updateable(customer).ExecuteCommandAsync();

            return updateResult > 0 ? 1 : 0; // 修改成功返回 1，失败返回 0
        }



        public async Task<VipDto> GetCustomerVipInfoAsync(string cusId)
        {
            // 查询顾客基本信息
            var customer = await DbContext.db.Queryable<CUSTOMER>()
                                           .Where(c => c.CUS_ID == cusId)
                                           .FirstAsync();

            if (customer == null)
            {
                return null;
            }

            // 查询顾客的VIP等级和对应的折扣
            var vipInfo = await DbContext.db.Queryable<CUSTOMER, MEMBER>((cu, m) => new object[] {
                                                 JoinType.Inner, cu.VIPLEVEL == m.VIPLEVEL })
                                            .Where(cu => cu.CUS_ID == cusId)
                                            .Select((cu, m) => new
                                            {
                                                VIPLevel = cu.VIPLEVEL,
                                                Discount = m.DISCOUNT  // 获取对应的折扣
                                            })
                                            .FirstAsync();

            // 构造返回的 DTO
            return new VipDto
            {
                vipLevel = vipInfo.VIPLevel,
                discount= vipInfo.Discount
            };
        }

        public async Task<List<CouponDto>> GetCustomerCouponInfoAsync(string cusId)
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

            // 获取当前时间并计算当月最后一天
            DateTime now = DateTime.Now;
            DateTime lastDayOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            // 构造返回的 DTO 列表
            var couponList = coupons.Select(coupon => new CouponDto
            {
                Name = coupon.Name,
                Type = coupon.Type,
                Price = coupon.Price,
                Year = lastDayOfMonth.Year.ToString(),
                Month = lastDayOfMonth.Month.ToString(),
                Day = lastDayOfMonth.Day.ToString()
            }).ToList();

            return couponList; // 返回所有优惠券信息
        }
    }
}
