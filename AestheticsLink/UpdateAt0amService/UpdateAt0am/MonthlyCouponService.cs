using Quartz;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;
using WebCommon.Database;

namespace UpdateAt0amService.UpdateAt0am
{
    public class MonthlyCouponService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await MonthlyCouponUpdate();
        }

        public async Task MonthlyCouponUpdate()
        {

            // 删除上个月未使用的优惠券
            await DbContext.db.Deleteable<CUS_COU>().ExecuteCommandAsync();

            // 为顾客发放新的优惠券
            var customers = await DbContext.db.Queryable<CUSTOMER>().ToListAsync();

            var copperCoupons = await DbContext.db.Queryable<COUPON>().Where(c => c.PRICE == 25).ToListAsync();
            var sliverCoupons = await DbContext.db.Queryable<COUPON>().Where(c => c.PRICE == 50).ToListAsync();
            var goldCoupons = await DbContext.db.Queryable<COUPON>().Where(c => c.PRICE == 100).ToListAsync();

            foreach (var customer in customers)
            {
                var couponsToAssign = customer.VIPLEVEL switch
                {
                    "Copper" => copperCoupons,
                    "Sliver" => sliverCoupons,
                    "Gold" => goldCoupons,
                    _ => null
                };

                if (couponsToAssign != null)
                {
                    foreach (var coupon in couponsToAssign)
                    {
                        var cusCou = new CUS_COU
                        {
                            CUS_ID = customer.CUS_ID,
                            COU_ID = coupon.COU_ID
                        };

                        await DbContext.db.Insertable(cusCou).ExecuteCommandAsync();
                    }
                }
            }
        }
    }
}
