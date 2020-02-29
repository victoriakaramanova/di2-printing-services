namespace Di2.Web.ViewModels.DeliveryBatches.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateBatchInputModel
    {
        public string MaterialId { get; set; }

        public string SupplierId { get; set; }

        public double Quantity { get; set; }

    }
}
