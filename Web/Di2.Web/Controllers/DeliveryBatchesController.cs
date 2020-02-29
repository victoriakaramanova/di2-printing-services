namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.DeliveryBatches;
    using Di2.Web.ViewModels.DeliveryBatches.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class DeliveryBatchesController : BaseController
    {
        private readonly IDeliveryBatchesService deliveryBatchesService;

        public DeliveryBatchesController(IDeliveryBatchesService deliveryBatchesService)
        {
            this.deliveryBatchesService = deliveryBatchesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBatchInputModel input)
        {
            await this.deliveryBatchesService.CreateBatch(input);

            return this.Redirect("/DeliveryBatches/All");
        }

        public async Task<IActionResult> All()
        {
            var allDeliveryBatches = await this.deliveryBatchesService.GetAllDeliveryBatches<AllDeliveryBatchesViewModel>();

            return this.View(allDeliveryBatches);
        }
    }
}
