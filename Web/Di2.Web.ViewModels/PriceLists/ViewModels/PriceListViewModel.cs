using Di2.Data.Models;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Materials.ViewModels;
using Di2.Web.ViewModels.Suppliers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.PriceLists.ViewModels
{
    public class PriceListViewModel : IMapFrom<PriceList>
    {
        public IEnumerable<MaterialsViewModel> Materials { get; set; }

        public IEnumerable<SuppliersViewModel> Suppliers { get; set; }

        public double MinimumQuantityPerOrder { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
