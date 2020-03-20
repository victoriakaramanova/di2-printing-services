namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using Di2.Web.ViewModels.PriceLists.InputModels;
    using Di2.Web.ViewModels.PriceLists.ViewModels;
    using Di2.Web.ViewModels.Suppliers.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Single = ViewModels.PriceLists.ViewModels.SingleViewModel;

    public class PriceListsController : BaseController
    {
        private readonly IPriceListsService priceListsService;

        public PriceListsController(IPriceListsService priceListsService)
        {
            this.priceListsService = priceListsService;
        }

        public async Task<IActionResult> Create()
        {
            var materials = await this.priceListsService.GetAllMaterials<MaterialsViewModel>();
            var suppliers = await this.priceListsService.GetAllSuppliers<SuppliersViewModel>();
            //var viewModel = new CreatePriceListInputModel
            //{
            //    MaterialId = materialId,
            //    SupplierId = supplierId,
            //};

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePriceListInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.priceListsService.CreateAsync(input);
            return this.Redirect("/");
        }

        public async Task<IActionResult> MaterialsPerSupplier(string supplier)
        {
            var viewModel = await this.priceListsService.GetMaterialsPerSupplier<MaterialsPerSupplierViewModel>(supplier);
            return this.View(viewModel);
        }

        public async Task<IActionResult> SuppliersPerMaterial(string material)
        {
            var viewModel = await this.priceListsService.GetSupplierstPerMaterial<SuppliersPerMaterialViewModel>(material);
            return this.View(viewModel);
        }
    }
}
