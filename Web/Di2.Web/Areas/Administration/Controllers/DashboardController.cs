namespace Di2.Web.Areas.Administration.Controllers
{
    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Administration.Dashboard;
    using Di2.Web.ViewModels.SubCategories.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISubCategoriesService subCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            ISubCategoriesService subCategoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.subCategoriesService = subCategoriesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CreateSubCategoryPath = "SubCategories/Add",
                SubCategoriesCount = this.subCategoriesService.GetCount(),
                ViewAllSubCategories = "SubCategories/All",
            };
            return this.View(viewModel);
        }
    }
}
