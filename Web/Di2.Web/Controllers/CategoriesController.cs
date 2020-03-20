namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Categories.InputModels;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public CategoriesController(
            ICategoriesService categoriesService,
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryInputModel input, string userId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // CORRECT WAY TO FIND A USER - var user = await this.userManager.GetUserAsync(this.User);
            // userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; - ALSO OK!!!
            var user = await this.userManager.GetUserAsync(this.User);

            await this.categoriesService.AddAsync(input, user.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var viewModel = this.categoriesService
                .GetAllCategories<CategoryViewModel>();

            return this.View(viewModel);
        }
    }
}
