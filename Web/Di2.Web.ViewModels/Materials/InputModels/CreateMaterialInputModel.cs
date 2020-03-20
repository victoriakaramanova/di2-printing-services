namespace Di2.Web.ViewModels.Materials.InputModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using Di2.Web.ViewModels.SubCategories.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    [Authorize]
    public class CreateMaterialInputModel : IMapFrom<Material>, IMapTo<Material>
    {
        // public IList<SubCategoriesViewModel> SubCategoriesViewModelList { get; set; }

        // public SubCategoriesViewModel SubCategoriesViewModel { get; set; }

        [Required]
        public string Name { get; set; }

        // [DataType(DataType.Date)] - for short date format
        [Required]
        public string Description { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string ExtraInfo { get; set; }

        public int SubCategoryId { get; set; }

        [Display(Name = "Подкатегория")]
        [Required]
        public string SubCategoryName { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
