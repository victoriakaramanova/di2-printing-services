namespace Di2.Web.ViewModels.OrderSuppliers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.PriceLists.ViewModels;

    public class CreateOrderSupplierInputModel : IMapFrom<OrderSupplier>, IMapFrom<PriceList>, IMapTo<OrderSupplier>
    {
        public int PriceListId { get; set; }

        public PriceListViewModel PriceList { get; set; }

        // public int MaterialId { get; set; }

        // public Material Material { get; set; }

        // public int SupplierId { get; set; }

        // public Supplier Supplier { get; set; }

        // public double MinimumQuantityPerOrder { get; set; }

        // public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public double Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
