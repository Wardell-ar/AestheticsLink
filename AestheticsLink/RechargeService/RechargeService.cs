using RechargeService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace RechargeService
{
    public class RechargeServices : IRechargeService
    {
        public bool CustomerRecharge(RechargeDto recharge)
        {
            //检查客户ID是否存在
            var customer = DbContext.db.Ado.SqlQuery<CUSTOMER>(
            "SELECT CUS_ID FROM CUSTOMER WHERE CUS_ID = :cus_ID",
            new
            {
                cus_ID = recharge.CUS_ID
            }
        );
            if (customer.Count == 0)
            {
                return false;
            }
            //更改客户余额
            DbContext.db.Ado.ExecuteCommand(
                "UPDATE CUSTOMER SET BALANCE = BALANCE + :rechargeAmount WHERE CUS_ID =:cusID",
                new
                {
                    rechargeAmount = recharge.RECHARGE_AMOUNT,
                    cusID = recharge.CUS_ID,
                });
            //判断客户会员等级并升级，copper一次性充值1000升级silver，silver充值2000升级gold
            var vipLevel = DbContext.db.Ado.SqlQuerySingle<string>(
                "SELECT VIPLEVEL FROM CUSTOMER WHERE CUS_ID = :cus_ID",
                new
                {
                    cus_ID = recharge.CUS_ID,
                });
            switch (vipLevel)
            {
                case "copper":
                    if (recharge.RECHARGE_AMOUNT >= 1000)
                        DbContext.db.Ado.ExecuteCommand(
                            "UPDATE BILL SET VIPLEVEL = silver WHERE CUS_ID =:cusID",
                            new
                            {
                                cusID = recharge.CUS_ID,
                            });
                    break;
                case "silver":
                    if (recharge.RECHARGE_AMOUNT >= 2000)
                        DbContext.db.Ado.ExecuteCommand(
                            "UPDATE BILL SET VIPLEVEL = gold WHERE CUS_ID =:cusID",
                            new
                            {
                                cusID = recharge.CUS_ID,
                            });
                    break;
            }
            return true;
        }
    }
}
