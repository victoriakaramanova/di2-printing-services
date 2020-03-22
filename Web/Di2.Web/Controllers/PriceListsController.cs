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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Single = ViewModels.PriceLists.ViewModels.SingleViewModel;

    public class PriceListsController : BaseController
    {
        private readonly IPriceListsService priceListsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PriceListsController(IPriceListsService priceListsService, UserManager<ApplicationUser> userManager)
        {
            this.priceListsService = priceListsService;
            this.userManager = userManager;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePriceListInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.priceListsService.CreateAsync(input, user.Id);

            return this.RedirectToAction(nameof(this.ById));
        }

        public IActionResult All()
        {
            var allPriceLists = this.priceListsService.GetAllPriceLists<PriceListViewModel>();

            return this.View(allPriceLists);
        }

        public IActionResult ById(int id)
        {
            var priceListViewModel = this.priceListsService.GetById<PriceListViewModel>(id);
            if (priceListViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(priceListViewModel);
        }
    }
}
