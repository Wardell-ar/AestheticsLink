using ORScheduleService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORScheduleService
{
    public interface IO_RScheduleService
    {
        Task<List<QueryO_RScheduleDto>> GetO_RScheduleByCriteria(Dictionary<string, object> criteria);
    }
}
