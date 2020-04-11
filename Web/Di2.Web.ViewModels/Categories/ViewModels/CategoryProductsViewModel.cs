using Di2.Data.Models;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.OrderSuppliers;
using Di2.Web.ViewModels.SubCategories.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Categories.ViewModels
{
    public class CategoryProductsViewModel : IMapFrom<OrderSupplier>, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }

        public IEnumerable<OrderSupplierViewModel> OrderSuppliers { get; set; }
    }
}
