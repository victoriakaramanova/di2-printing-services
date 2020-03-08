namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Materials.InputModels;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class MaterialsController : BaseController
    {
        private readonly IMaterialsService materialsService;
        private readonly ISubCategoriesService subCategoriesService;

        public MaterialsController(IMaterialsService materialsService, ISubCategoriesService subCategoriesService)
        {
            this.materialsService = materialsService;
            this.subCategoriesService = subCategoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateMaterialInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.materialsService.AddAsync(input);

            return this.Redirect("/");
        }

        // Download file sample
        public IActionResult Download(string filename)
        {
            // System.IO.File.ReadAllBytesAsync(); - an alternative
            // return this.File();
            // FIND SANITIZE FILENAME ARTICLES to ignore xss etc!!!
            return this.PhysicalFile(@$"D:\{filename}", "application/pdf");
        }

        public IActionResult All()
        {
            var allMaterials = this.materialsService.GetAllMaterials<MaterialsViewModel>();

            return this.View(allMaterials);
        }
    }
}
