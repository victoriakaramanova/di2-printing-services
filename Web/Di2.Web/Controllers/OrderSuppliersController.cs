using Di2.Data.Models;
using Di2.Services.Data;
using Di2.Web.ViewModels.OrderSupplier;
using Di2.Web.ViewModels.PriceLists.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Di2.Web.Controllers
{
    public class OrderSuppliersController : BaseController
    {
        private readonly IOrderSupplierService orderSupplierService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISuppliersService suppliersService;
        private readonly IMaterialsService materialsService;
        private readonly IPriceListsService priceListsService;

        public OrderSuppliersController(
            IOrderSupplierService orderSupplierService,
            UserManager<ApplicationUser> userManager,
            ISuppliersService suppliersService,
            IMaterialsService materialsService,
            IPriceListsService priceListsService)
        {
            this.orderSupplierService = orderSupplierService;
            this.userManager = userManager;
            this.suppliersService = suppliersService;
            this.materialsService = materialsService;
            this.priceListsService = priceListsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = this.priceListsService.GetAllPriceLists<PriceListViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateOrderSupplierInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var orderSupplier = await this.orderSupplierService.CreateAsync(input.OrderDate, input.MaterialId, input.SupplierId, input.Quantity, input.UnitPrice, input.TotalPrice, user.Id);
            return this.Redirect("/");
        }


    }
}
