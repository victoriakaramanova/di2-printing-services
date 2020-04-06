using Di2.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Di2.Web.Controllers
{
    public class ProductsControler : BaseController
    {
        private readonly IOrderSupplierService orderSupplierService;

        public ProductsControler(IOrderSupplierService orderSupplierService)
        {
            this.orderSupplierService = orderSupplierService;
        }
    }
}
