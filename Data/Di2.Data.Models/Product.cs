using Di2.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Data.Models
{
    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public string Image { get; set; }

        public double QuantityOnStock { get; set; }

        public decimal UnitPrice { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
