namespace Di2.Web.Controllers
{
    using System.Diagnostics;
    using Di2.Services.Data;
    using Di2.Web.ViewModels;
    using Di2.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IMaterialsService materialsService;

        public HomeController(IMaterialsService materialsService)
        {
            this.materialsService = materialsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Materials = this.materialsService
                    .GetAllMaterials<IndexMaterialViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
