namespace Di2.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;

    using Di2.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
