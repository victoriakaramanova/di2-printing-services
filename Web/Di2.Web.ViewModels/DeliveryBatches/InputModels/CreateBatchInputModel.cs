namespace Di2.Web.ViewModels.DeliveryBatches.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateBatchInputModel
    {
        public int MaterialId { get; set; }

        public int SupplierId { get; set; }

        public double Quantity { get; set; }

    }
}
