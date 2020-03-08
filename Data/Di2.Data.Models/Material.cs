﻿namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using Di2.Data.Common.Models;
    using Microsoft.AspNetCore.Http;

    public class Material : BaseDeletableModel<int>
    {
        public Material()
        {
            this.PriceLists = new HashSet<PriceList>();
            this.DeliveryBatches = new HashSet<DeliveryBatch>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        // public IFormFile Image { get; set; }

        public virtual ICollection<PriceList> PriceLists { get; set; }

        public virtual ICollection<DeliveryBatch> DeliveryBatches { get; set; }
    }
}
