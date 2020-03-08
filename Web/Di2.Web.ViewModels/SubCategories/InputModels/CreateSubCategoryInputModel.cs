namespace Di2.Web.ViewModels.SubCategories.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Di2.Web.ViewModels.SubCategories.ViewModels;

    public class CreateSubCategoryInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }
    }
}
