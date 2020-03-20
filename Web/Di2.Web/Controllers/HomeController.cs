namespace Di2.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Di2.Services;
    using Di2.Services.Data;
    using Di2.Services.Messaging;
    using Di2.Web.ViewModels;
    using Di2.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sandbox;

    public class HomeController : BaseController
    {
        private readonly IMaterialsService materialsService;
        private readonly Cloudinary cloudinary;
        private readonly IEmailSender sender;

        public HomeController(
            IMaterialsService materialsService,
            Cloudinary cloudinary,
            IEmailSender sender)
        {
            this.materialsService = materialsService;
            this.cloudinary = cloudinary;
            this.sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                Materials = this.materialsService
                    .GetAllMaterials<IndexMaterialViewModel>(),
            };

            // NOTA BENE!
            await this.sender.SendEmailAsync("Stoyan@softuni.bg", "Stoyan", "nkik@abv.bg", "Title", "html");
            return this.View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var result = await CloudinaryExtension.Upload(this.cloudinary, files);

            this.ViewBag.Links = result;
            return this.Redirect("/");
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
