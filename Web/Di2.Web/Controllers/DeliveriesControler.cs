namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.Deliveries;
    using Microsoft.AspNetCore.Mvc;

    public class DeliveriesControler : BaseController
    {
        private readonly IDeliveriesService deliveriesService;

        public DeliveriesControler(IDeliveriesService deliveriesService)
        {
            this.deliveriesService = deliveriesService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.deliveriesService.GetById<DeliveryViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var viewModel = this.deliveriesService
                .GetAllProducts<DeliveryViewModel>(null);

            return this.View(viewModel);
        }
    }
}
