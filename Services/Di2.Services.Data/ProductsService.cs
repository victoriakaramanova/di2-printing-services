using Di2.Common;
using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Web.ViewModels.OrderSuppliers;
using Di2.Web.ViewModels.PriceLists.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository)
        {
            this.productsRepository = productsRepository;
            this.orderSuppliersRepository = orderSuppliersRepository;
        }

        public async Task<int> Create(OrderSupplierViewModel input)
        {
            // int statusId = Enum.TryParse<OrderStatus>(input.StatusId, out _);
            double stockQuantity = this.orderSuppliersRepository.All()
                .Where(x => x.Status == 0 && x.MaterialId == input.MaterialId)
                .Sum(x => x.Quantity);

            var product = new Product
            {
                Name = input.Material.Name,
                Description = input.Material.Description,
                ExtraInfo = input.Material.ExtraInfo,
                Image = input.Material.Image,
                CategoryId = input.Material.CategoryId,
                SubCategoryId = input.Material.SubCategoryId,
                QuantityOnStock = stockQuantity,
                UnitPrice = input.UnitPrice * (decimal)GlobalConstants.StandardMarkup,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
            return product.Id;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
