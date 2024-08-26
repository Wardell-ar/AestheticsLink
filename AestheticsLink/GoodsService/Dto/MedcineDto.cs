using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedcineService.Dto
{
    public class CheckStorageDto
    {
        public string hos_id { get; set; }  // 可选的医院ID
        public string name { get; set; }    // 可选的药品名称
    }

    public class StorageResultDto
        {
            public int Storage { get; set; }
       }

    public class MedicineInfoDto
    {
        public string g_id { get; set; }
        public string name { get; set; }
        public string hos_id { get; set; }
        public int storage { get; set; }
        public string producer { get; set; }
        public DateTime sell_by_date { get; set; }
        public decimal price { get; set; }
    }
}
