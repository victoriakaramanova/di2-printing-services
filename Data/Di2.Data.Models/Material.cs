namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Common.Models;

    public class Material : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<PriceList> PriceLists { get; set; }

        public virtual ICollection<DeliveryBatch> DeliveryBatches { get; set; }
    }
}
