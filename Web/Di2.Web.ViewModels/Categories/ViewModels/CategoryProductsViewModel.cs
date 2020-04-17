using Di2.Data.Models;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Deliveries;
using Di2.Web.ViewModels.OrderSuppliers;
using Di2.Web.ViewModels.SubCategories.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Categories.ViewModels
{
    public class CategoryProductsViewModel : IMapFrom<Delivery>, IMapFrom<Category>,IMapTo<DeliveryViewModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public double Quantity { get; set; }

        public decimal AvgPrice { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        // public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public IEnumerable<DeliveryViewModel> Deliveries { get; set; }
    }
}
