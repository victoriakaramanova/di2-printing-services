namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.OrderSuppliers;
    using Di2.Web.ViewModels.PriceLists.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

        // TODO: ASK!!!
        [Authorize]
        public IActionResult Create()
        {
            // var orderSuppliers = this.orderSupplierService.GetAllOrderSuppliers<OrderSupplierViewModel>();
            var priceLists = this.priceListsService.GetAllPriceLists<PriceListViewModel>();
            var viewModel = new OrderSuppliersListViewModel
            {
                Pricelists = priceLists,
                Orderpart = new CreateOrderSupplierInputModel(),
            };

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
            var orderSupplier = await this.orderSupplierService.CreateAsync(input, user.Id);
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var viewModel = this.orderSupplierService
                .GetAllOrderSuppliers<OrderSupplierViewModel>();

            return this.View(viewModel);
        }
    }
}
