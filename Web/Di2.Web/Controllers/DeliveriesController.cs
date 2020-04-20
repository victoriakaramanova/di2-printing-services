namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Di2.Data.Models;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Di2.Web.ViewModels.Deliveries;
    using Di2.Web.ViewModels.Orders.InputModels;
    using Di2.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Di2.Services.Mapping;
    //using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]/[action]")]
    public class DeliveriesController : BaseController
    {
        private readonly IDeliveriesService deliveriesService;
        private readonly IMaterialsService materialsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderService orderService;
        private readonly ICategoriesService categoriesService;

        public DeliveriesController(
            IDeliveriesService deliveriesService, 
            IMaterialsService materialsService, 
            UserManager<ApplicationUser> userManager,
            IOrderService orderService,
            ICategoriesService categoriesService)
        {
            this.deliveriesService = deliveriesService;
            this.materialsService = materialsService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.categoriesService = categoriesService;
        }

        [HttpGet("{materialId}")]
        //[Route("Deliveries/ById/{materialId}")]
        public IActionResult ById(int materialId)
        {
            var viewModel = this.deliveriesService
                .GetByMaterialId<CategoryProductsViewModel>(materialId);
            if (viewModel == null)
            {
                return this.NotFound();
            }
            //return this.RedirectToAction(nameof(this.ById), new { id = materialId});
            return this.View(viewModel);
        }

        [HttpPost] //(Name = "Order")]
        [Authorize]
        public async Task<IActionResult> Order(OrderInputModel input)
        {
            //OrderViewModel viewModel = input.To<OrderViewModel>();
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var categoryName = this.materialsService.GetById(input.MaterialId).Category.NameEng;
            var user = await this.userManager.GetUserAsync(this.User);
            await this.orderService.CreateOrder(input, user.Id);

            return this.RedirectToAction("ByName","Categories", new { name = categoryName});
        }
    }
}
