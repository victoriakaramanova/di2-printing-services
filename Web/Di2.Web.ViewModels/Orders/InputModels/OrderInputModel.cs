using Di2.Services.Mapping;
using Di2.Web.ViewModels.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Orders.InputModels
{
    public class OrderInputModel:IMapTo<OrderViewModel>
    {
        public int MaterialId { get; set; }

        public int Quantity { get; set; }
    }
}
