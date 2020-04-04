using Di2.Services.Data;
using Di2.Web.ViewModels.Totals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Di2.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TotalsController : ControllerBase
    {
        private readonly ITotalsService totalsService;

        public TotalsController(ITotalsService totalsService)
        {
            this.totalsService = totalsService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<TotalsResponseModel> Post(TotalsInputModel input)
        {
            var totalPrice = this.totalsService.CalculateTotalPrice(input.Quantity, input.UnitPrice);
            return new TotalsResponseModel { TotalPrice = totalPrice };
        }
    }
}
