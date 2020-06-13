using Di2.Data.Models;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Pictures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Orders.ViewModels
{
    public class FinalOrderViewModel : IMapFrom<Order>, IMapFrom<OrderViewModel>
    {
        public string Id { get; set; }

        //public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        //public string Description { get; set; }

        //public string ExtraInfo { get; set; }

        //public string Image { get; set; }

        //public string SubCategoryName { get; set; }

        //public DateTime IssuedOn { get; set; }

        public double Quantity { get; set; }

        public decimal AvgPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrdererId { get; set; }

        public ApplicationUser Orderer { get; set; }

        //public int StatusId { get; set; }

        public string ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

        //public HashSet<PictureViewModel> Pictures { get; set; }
    }
}
