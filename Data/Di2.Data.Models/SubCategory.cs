namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
