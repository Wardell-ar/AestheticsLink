using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Dto
{
    public class PlaceOrderDto
    {
        public string id { get; set; }
        public List<ProjectDto> items { get; set; }
        public string hospital { get; set; }

        public string couponid { get; set; }

    }
}
