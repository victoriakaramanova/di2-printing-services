namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.Categories.InputModels;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.AddAsync(input);

            return this.Redirect("/");
        }

        public IActionResult All()
        {
            var viewModel = this.categoriesService.GetAllCategories<CategoryViewModel>();

            return this.View(viewModel);
        }
    }
}
