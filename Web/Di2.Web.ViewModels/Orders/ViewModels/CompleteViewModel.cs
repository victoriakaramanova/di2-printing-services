namespace Di2.Web.ViewModels.Orders.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Data.Models.Enums;
    using Di2.Services.Mapping;
    using Di2.Web.Infrastructure.Attributes;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using Di2.Web.ViewModels.Orders.InputModels;
    using Di2.Web.ViewModels.Pictures;

    public class CompleteViewModel : IMapFrom<Order>, IMapTo<Order>, IMapFrom<OrderInputModel> // , IMapTo<OrderInputModel>
    {
        public string Id { get; set; }

        public int MaterialId { get; set; }

        // public MaterialsViewModel Material { get; set; }
        public string MaterialName { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public string Image { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime IssuedOn { get; set; }

        //[OrderQuantity("AvailableQuantity")]
        public double Quantity { get; set; }

        public decimal AvgPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrdererId { get; set; }

        public string Orderer { get; set; }

        public int StatusId { get; set; }

        public string DeliveryAddress { get; set; }

        public string SwitchValue => this.StatusId == 1 ?
         "v-switcher v-switcher-mode--tri v-switcher-state--active"
         : this.StatusId == -1
         ? "v-switcher v-switcher-mode--tri v-switcher-state--passive"
         : "v-switcher v-switcher-mode--tri v-switcher-state--neutral";

        public OrderStatus OrderStatus { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ReceiptId { get; set; }

        public HashSet<PictureViewModel> Pictures { get; set; }

        public double AvailableQuantity { get; set; }
    }
}
