using Di2.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Di2.Web.ViewModels.Materials.InputModels
{
    public class CreateMaterialInputModel
    {
        [Required]
        public string Name { get; set; }

        // [DataType(DataType.Date)] - for short date format
        [Required]
        public string Description { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string ExtraInfo { get; set; }

        [Display(Name = "Подкатегория")]
        [Required]
        public SubCategory SubCategory { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
