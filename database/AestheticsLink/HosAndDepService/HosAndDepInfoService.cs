using HosAndDepService.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommon.Database;
using WebModel.Entity;

namespace HosAndDepService
{
    public class HosAndDepInfoService : IHosAndDepInfoService
    {
        // 计算部门的所有服务器的薪资总和
        public decimal CalculatePayout(string depId, List<SERVER> servers)
        {
            return servers.Where(ser => ser.DEP_ID == depId).Sum(ser => ser.TAKEHOMEPAY);
        }

        // 计算部门当月的手术费收入总和
        public decimal CalculateDepartmentMonthlyIncome(string depId, string hosId)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var income = DbContext.db.Queryable<OPERATE, BILL, SERVER>((op, bill, ser) => new object[]
            {
                JoinType.Inner, op.BILL_ID == bill.BILL_ID,
                JoinType.Inner, op.SER_ID == ser.SER_ID
            })
            .Where((op, bill, ser) => ser.DEP_ID == depId && ser.HOS_ID == hosId
                                        && op.FOUND_DATE.Month == currentMonth
                                        && op.FOUND_DATE.Year == currentYear)
            .Sum((op, bill, ser) => bill.PAID_AMOUNT);

            return income;
        }

        public Task<List<QueryHADInfoResponseDto>> GetHosAndDepInfoByCriteria(Dictionary<string, object> criteria)
        {
            // 从字典中获取hos_id
            string? hosId = criteria.ContainsKey("hosId") ? criteria["hosId"]?.ToString() : null;
            string? branchId = criteria.ContainsKey("branchId") ? criteria["branchId"].ToString() : null;
            string? branchName = criteria.ContainsKey("branchName") ? criteria["branchName"].ToString() : null;
            string? status = criteria.ContainsKey("status") ? criteria["status"].ToString() : null;

            // 检查hos_id是否存在
            if (string.IsNullOrEmpty(hosId))
            {
                throw new ArgumentException("hos_id is required.");
            }

            // 获取与hos_id相关的所有部门ID
            var departmentIds = DbContext.db.Queryable<HOS_DEP>()
                                            .Where(hd => hd.H_ID == hosId)
                                            .Select(hd => hd.D_ID)
                                            .ToList();

            // 获取所有符合条件的部门
            var departments = DbContext.db.Queryable<DEPARTMENT>()
                                          .Where(dep => departmentIds.Contains(dep.DEP_ID))
                                          .WhereIF(!string.IsNullOrEmpty(branchId), dep => dep.DEP_ID == branchId)
                                          .WhereIF(!string.IsNullOrEmpty(branchName), dep => dep.NAME == branchName)
                                          .ToList();

            // 获取所有服务器信息
            var servers = DbContext.db.Queryable<SERVER>()
                .WhereIF(!string.IsNullOrEmpty(hosId), ser => ser.HOS_ID == hosId)
                .ToList();

            // 获取所有手术和账单信息
            var operations = DbContext.db.Queryable<OPERATE>().ToList();
            var bills = DbContext.db.Queryable<BILL>().ToList();

            // 将部门和服务器信息结合
            var result = departments.Select(dep => new QueryHADInfoResponseDto
            {
                branchId = dep.DEP_ID,
                branchName = dep.NAME,
                status = servers.Any(ser => ser.DEP_ID == dep.DEP_ID) ? "1" : "0",
                expense = CalculatePayout(dep.DEP_ID, servers),
                income = CalculateDepartmentMonthlyIncome(dep.DEP_ID, hosId) // 传入hosId
            }).ToList();

            if (!string.IsNullOrEmpty(status) && status == "1")
            {
                result = result.Where(dto => dto.status == "1").ToList();
            }
            else if (!string.IsNullOrEmpty(status) && status == "0")
            {
                result = result.Where(dto => dto.status == "0").ToList();
            }


            return Task.FromResult(result);
        }
    }
}
