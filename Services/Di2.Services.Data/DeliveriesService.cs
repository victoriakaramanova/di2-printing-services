namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Common;
    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Data.Models.Enums;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.OrderSuppliers;
    using Di2.Web.ViewModels.PriceLists.InputModels;

    public class DeliveriesService : IDeliveriesService
    {
        private readonly IDeletableEntityRepository<Delivery> deliveriesRepository;
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;

        public DeliveriesService(
            IDeletableEntityRepository<Delivery> deliveriesRepository,
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository)
        {
            this.deliveriesRepository = deliveriesRepository;
            this.orderSuppliersRepository = orderSuppliersRepository;
        }

        public async Task<int> Create(OrderSupplierViewModel input)
        {
            /*double stockQuantity = this.orderSuppliersRepository.All()
                .Where(x => x.Status == 0 && x.MaterialId == input.MaterialId)
                .Sum(x => x.Quantity);*/

            var deliveredProduct = new Delivery
            {
                Name = input.Material.Name,
                Description = input.Material.Description,
                ExtraInfo = input.Material.ExtraInfo,
                Image = input.Material.Image,
                CategoryId = input.Material.CategoryId,
                SubCategoryId = input.Material.SubCategoryId,
                Quantity = input.Quantity,
                //QuantityOnStock = stockQuantity,
                UnitPrice = input.UnitPrice * (1 + (decimal)GlobalConstants.StandardMarkup),
            };

            await this.deliveriesRepository.AddAsync(deliveredProduct);
            await this.deliveriesRepository.SaveChangesAsync();
            return deliveredProduct.Id;
        }

        public IEnumerable<T> GetAll<T>(int? categoryId = null)
        {
            var query = this.deliveriesRepository.All();
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var product = this.deliveriesRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return product;
        }
    }
}
