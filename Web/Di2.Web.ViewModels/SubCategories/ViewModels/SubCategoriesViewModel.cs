namespace Di2.Web.ViewModels.SubCategories.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class SubCategoriesViewModel : IMapFrom<SubCategory>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
