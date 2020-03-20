namespace Di2.Web.ViewModels.PriceLists.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.Materials.ViewModels;
    using Di2.Web.ViewModels.Suppliers.ViewModels;

    public class CreatePriceListInputModel : IMapFrom<MaterialViewModel>, IMapFrom<SupplierViewModel>
    {
        //public IList<MaterialsViewModel> MaterialsViewModelList { get; set; }

        //public IList<SuppliersViewModel> SuppliersViewModelList { get; set; }
         public Material Material { get; set; }

         public int MaterialId { get; set; }

         public Supplier Supplier { get; set; }

         public int SupplierId { get; set; }

         public double MinimumQuantityPerOrder { get; set; }

         public decimal UnitPrice { get; set; }
    }
}
