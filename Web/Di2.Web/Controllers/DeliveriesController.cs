namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Di2.Web.ViewModels.Deliveries;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]/[action]")]
    public class DeliveriesController : BaseController
    {
        private readonly IDeliveriesService deliveriesService;
        private readonly IMaterialsService materialsService;

        public DeliveriesController(IDeliveriesService deliveriesService, IMaterialsService materialsService)
        {
            this.deliveriesService = deliveriesService;
            this.materialsService = materialsService;
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

    }
}
