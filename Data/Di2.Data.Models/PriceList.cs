namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Common.Models;

    public class PriceList : BaseDeletableModel<int>
    {
        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public double MinimumQuantityPerOrder { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
