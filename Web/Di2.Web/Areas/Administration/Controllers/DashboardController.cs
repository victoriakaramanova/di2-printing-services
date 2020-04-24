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
        private readonly ISuppliersService suppliersService;
        private readonly IMaterialsService materialsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPriceListsService priceListsService;

        public DashboardController(
            ISubCategoriesService subCategoriesService,
            ISuppliersService suppliersService,
            IMaterialsService materialsService,
            UserManager<ApplicationUser> userManager,
            IPriceListsService priceListsService)
        {
            this.subCategoriesService = subCategoriesService;
            this.suppliersService = suppliersService;
            this.materialsService = materialsService;
            this.userManager = userManager;
            this.priceListsService = priceListsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CreateSubCategoryPath = "SubCategories/Add",
                SubCategoriesCount = this.subCategoriesService.GetCount(),
                ViewAllSubCategories = "SubCategories/All",

                CreateSupplierPath = "Suppliers/Add",
                SuppliersCount = this.suppliersService.GetCount(),
                ViewAllSuppliersPath = "Suppliers/All",

                CreateMaterialPath = "Materials/Add",
                MaterialsCount = this.materialsService.GetCount(),
                ViewAllMaterialsPath = "Materials/All",

                CreatePriceListPath = "PriceLists/Create",
                PriceListsCount = this.priceListsService.GetCount(),
                ViewAllPriceListsPath = "PriceLists/All",
            };
            return this.View(viewModel);
        }
    }
}
