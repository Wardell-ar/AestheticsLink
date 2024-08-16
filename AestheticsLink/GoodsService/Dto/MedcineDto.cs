using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedcineService.Dto
{
       public class CheckStorageDto
       {
            public string HOS_ID { get; set; }
            public string G_ID { get; set; }
       }

       public class StorageResultDto
        {
            public int Storage { get; set; }
       }
}
