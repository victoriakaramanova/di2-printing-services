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

        public MaterialsController(IMaterialsService materialsService)
        {
            this.materialsService = materialsService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateMaterialInputModel input)
        {
            await this.materialsService.AddAsync(input);

            return this.Redirect("/");
        }

        public async Task<IActionResult> All()
        {
            var allMaterials = await this.materialsService.GetAllMaterials<AllMaterialsViewModel>();

            return this.View(allMaterials);
        }
    }
}
