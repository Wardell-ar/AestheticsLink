using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Dto
{
    public class PlaceOrderDto
    {
        public string CUS_ID { get; set; }
        public List<ProjectDto> PROJECT { get; set; }

        public int PAID_AMOUNT { get; set; }

    }
}
