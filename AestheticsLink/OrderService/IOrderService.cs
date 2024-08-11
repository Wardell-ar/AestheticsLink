using OrderService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService
{
    public interface IOrderService
    {
        public bool CheckProject(PlaceOrderDto order);

        public List<HospitalChoiceDto> GetHospital();

        public BillDto AddBill(PlaceOrderDto order);
    }
}
