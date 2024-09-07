using HosAndDepService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Entity;

namespace HosAndDepService
{
    public interface IHosAndDepInfoService
    {
        public decimal CalculatePayout(string depId, List<SERVER> servers);

        public decimal CalculateDepartmentMonthlyIncome(string depId, string hosId);

        Task<List<QueryHADInfoResponseDto>> GetHosAndDepInfoByCriteria(Dictionary<string, object> criteria);
    }
}
