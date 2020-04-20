using Di2.Data.Common.Models;
using Di2.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Data.Models
{
    public class Order : BaseDeletableModel<string>
    {
        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public DateTime IssuedOn { get; set; }

        public double Quantity { get; set; }

        public string OrdererId { get; set; }

        public ApplicationUser Orderer { get; set; }

        public int StatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
