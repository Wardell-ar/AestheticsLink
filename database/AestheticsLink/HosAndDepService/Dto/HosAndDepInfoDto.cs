using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosAndDepService.Dto
{
    public class QueryHADInfoReceptionDto
    {
        public string branchId { get; set; }
        public string hosId { get; set; }
        public string branchName { get; set; }
        public string status { get; set; }
    }

    public class QueryHADInfoResponseDto
    {
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string status { get; set; }
        public decimal income { get; set; }
        public decimal expense { get; set; }
    }

    public class DeleteHADInfoReceptionDto
    {
        public string hosId { get; set; }
        public string branchId { get; set; }
    }

    public class CreateHADInfoReceptionDto
    {
        public string hosId { get; set; }
        public string branchName { get; set; }
    }

    public class UpdateHADInfoReceptionDto
    {
        public string hosId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
    }
}
