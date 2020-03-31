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

        // TODO: COLLECTION INITIALIZATION!!!!!!!!!!!!
        [Authorize]
        public IActionResult Create()
        {
            var priceLists = this.priceListsService.GetAllPriceLists<PriceListViewModel>();
            var orderSub = new CreateOrderSupplierInputModel [priceLists.Count];
            for (int i = 0; i < orderSub.Length; i++)
            {
                orderSub[i].OrderDate = DateTime.UtcNow;
                orderSub[i].Quantity = 0;
                orderSub[i].TotalPrice = 0;
            }

            // var orderSuppliers = this.orderSupplierService.GetAllOrderSuppliers<OrderSupplierViewModel>();
            var viewModel = new OrderSuppliersListViewModel
            {
                Pricelists = priceLists,
                OrderSub = orderSub.ToList(),
            };


            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderSuppliersListViewModel input)
        {
            var priceLists = input.Pricelists;
            var orderSub = input.OrderSub;
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            int materialId;
            int supplierId;
            double minQty;
            decimal unitPrice;
            var priceListsDb = this.priceListsService.GetAllPriceLists<PriceListViewModel>();
            for (int i = 0; i < priceListsDb.Count; i++)
            {
                if (input.OrderSub.Select(x => x.Quantity).ToList()[i] > 0)
                {
                    materialId = input.Pricelists.Select(x => x.MaterialId).ToList()[i];
                    supplierId = input.Pricelists.Select(x => x.SupplierId).ToList()[i];
                    minQty = input.Pricelists.Select(x => x.MinimumQuantityPerOrder).ToList()[i];
                    unitPrice = input.Pricelists.Select(x => x.UnitPrice).ToList()[i];

                    var priceList = this.priceListsService.GetByElements<PriceListViewModel>(materialId, supplierId, minQty, unitPrice);
                    var user = await this.userManager.GetUserAsync(this.User);
                    OrderSupplier orderSupplier = await this.orderSupplierService.CreateAsync(input.OrderSub.ToList()[i], priceList, user.Id);
                }
            }

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
