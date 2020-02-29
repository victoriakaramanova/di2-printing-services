namespace Di2.Web.ViewModels.PriceLists.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateInputModel
    {
        public int MaterialId { get; set; }

        public int SupplierId { get; set; }

        public double MinimumQuantityPerOrder { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
