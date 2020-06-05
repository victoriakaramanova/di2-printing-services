using Di2.Services.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Di2.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantityController : ControllerBase
    {
        private readonly IDeliveriesService deliveriesService;

        public QuantityController(IDeliveriesService deliveriesService)
        {
            this.deliveriesService = deliveriesService;
        }

        
    }
}
