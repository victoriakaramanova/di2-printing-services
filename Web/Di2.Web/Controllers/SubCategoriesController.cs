namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.SubCategories.InputModels;
    using Di2.Web.ViewModels.SubCategories.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class SubCategoriesController : BaseController
    {
        private readonly ISubCategoriesService subCategoriesService;

        public SubCategoriesController(ISubCategoriesService subCategoriesService)
        {
            this.subCategoriesService = subCategoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateSubCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.subCategoriesService.AddAsync(input);

            return this.Redirect("/");
        }

        public async Task<IActionResult> All()
        {
            var allSubCategories = await this.subCategoriesService.GetAllSubCategories<SubCategoriesViewModel>();

            return this.View(allSubCategories);
        }
    }
}
