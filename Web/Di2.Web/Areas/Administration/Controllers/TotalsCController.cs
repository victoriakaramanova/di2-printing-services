namespace Di2.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.Totals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[area]/[controller]")]

    public class TotalsCController : AdministrationController
    {
        private readonly ITotalsCustomerService totalsCustomerService;

        public TotalsCController(ITotalsCustomerService totalsCustomerService)
        {
            this.totalsCustomerService = totalsCustomerService;
        }

       // [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Post(TotalsCInputModel input)
        {
            var statusId = await this.totalsCustomerService.ChangeOrderStatus(input.OrderId, input.IsCompleted);
            return statusId;
        }
    }
}
