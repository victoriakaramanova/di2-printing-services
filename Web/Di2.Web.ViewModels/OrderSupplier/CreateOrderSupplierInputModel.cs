namespace Di2.Web.ViewModels.OrderSupplier
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class CreateOrderSupplierInputModel : IMapFrom<OrderSupplier>, IMapTo<OrderSupplier>
    {
        public DateTime OrderDate { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public double MinimumQuantityPerOrder { get; set; }

        public double Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
