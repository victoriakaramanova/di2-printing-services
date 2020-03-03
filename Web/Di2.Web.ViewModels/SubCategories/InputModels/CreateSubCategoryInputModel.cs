namespace Di2.Web.ViewModels.SubCategories.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;

    public class CreateSubCategoryInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

       // public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
