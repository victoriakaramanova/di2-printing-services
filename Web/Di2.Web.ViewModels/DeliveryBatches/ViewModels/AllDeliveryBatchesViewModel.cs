namespace Di2.Web.ViewModels.DeliveryBatches
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllDeliveryBatchesViewModel
    {
        public string MaterialId { get; set; }

        public string SupplierId { get; set; }

        public string MaterialName { get; set; }

        public string Color { get; set; }

        public string SupplierName { get; set; }

        public string ReferenceKey { get; set; }

        public DateTime CreatedOn { get; set; }

        public long Quantity { get; set; }

        public decimal Cost { get; set; }
    }
}
