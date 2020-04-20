using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Materials.ViewModels;
using Di2.Web.ViewModels.Orders.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Orders.ViewModels
{
    public class OrderViewModel:IMapFrom<Order>,IMapTo<Order>, IMapTo<OrderInputModel>
    {
        public string Id { get; set; }

        public int MaterialId { get; set; }

        public MaterialsViewModel Material { get; set; }

        public double Quantity { get; set; }

        public string OrdererId { get; set; }

        public ApplicationUser Orderer { get; set; }

        public int StatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
