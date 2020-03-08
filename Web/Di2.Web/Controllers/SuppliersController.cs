namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Services.Data;
    using Di2.Web.ViewModels.Suppliers.InputModels;
    using Di2.Web.ViewModels.Suppliers.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : BaseController
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateSupplierInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.suppliersService.AddAsync(input.Name, input.Address, input.Email, input.Phone);

            return this.Redirect("/");
        }

        public async Task<IActionResult> All()
        {
            var allSuppliers = await this.suppliersService.GetAllSuppliers<SuppliersViewModel>();

            return this.View(allSuppliers);
        }
    }
}
