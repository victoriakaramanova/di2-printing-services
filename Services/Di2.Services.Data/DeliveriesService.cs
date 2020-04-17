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
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Di2.Web.ViewModels.Deliveries;
    using Di2.Web.ViewModels.OrderSuppliers;
    using Di2.Web.ViewModels.PriceLists.InputModels;

    public class DeliveriesService : IDeliveriesService
    {
        private readonly IDeletableEntityRepository<Delivery> deliveriesRepository;
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;
        private readonly IDeletableEntityRepository<Material> materialsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;

        public DeliveriesService(
            IDeletableEntityRepository<Delivery> deliveriesRepository,
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository,
            IDeletableEntityRepository<Material> materialsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<SubCategory> subCategoriesRepository)
        {
            this.deliveriesRepository = deliveriesRepository;
            this.orderSuppliersRepository = orderSuppliersRepository;
            this.materialsRepository = materialsRepository;
            this.categoriesRepository = categoriesRepository;
            this.subCategoriesRepository = subCategoriesRepository;
        }

        public async Task<int> Create(OrderSupplierViewModel input)
        {
            /*double stockQuantity = this.orderSuppliersRepository.All()
                .Where(x => x.Status == 0 && x.MaterialId == input.MaterialId)
                .Sum(x => x.Quantity);*/

            var materialCategoryId = this.materialsRepository.All()
                .Where(x => x.Id == input.MaterialId)
                .Select(x => x.CategoryId).FirstOrDefault();
            var material = this.materialsRepository.All()
                .Where(x => x.Id == input.MaterialId).FirstOrDefault();
            var delivery = new Delivery
            {
                OrderId = input.Id,
                MaterialId = input.MaterialId,
                Material = material,
                // Description = input.Material.Description,
                // ExtraInfo = input.Material.ExtraInfo,
                // Image = input.Material.Image,
                CategoryId = materialCategoryId,
                // SubCategoryId = input.Material.SubCategoryId,
                Quantity = input.Quantity,
                Cost = input.TotalPrice,
                //QuantityOnStock = stockQuantity,
                UnitPrice = input.UnitPrice * (1 + (decimal)GlobalConstants.StandardMarkup),
            };

            await this.deliveriesRepository.AddAsync(delivery);
            await this.deliveriesRepository.SaveChangesAsync();

            return delivery.Id;
        }

        public IEnumerable<T> GetAllProducts<T>(int? categoryId = null)
        {
            var categoryName = this.categoriesRepository.All()
                .FirstOrDefault(x => x.Id == categoryId).Name;

            var query = from d in this.deliveriesRepository.All()
                        group d by new
                        {
                            d.MaterialId,
                            d.Material.Name,
                            d.Material.Description,
                            d.Material.ExtraInfo,
                            d.Material.Image,
                            d.CategoryId,
                            d.Material.SubCategoryId,
                        }
                        into m 
                        select new CategoryProductsViewModel
                        {
                            Id = m.Key.MaterialId,
                            Name = m.Key.Name,
                            Description = m.Key.Description,
                            ExtraInfo = m.Key.ExtraInfo,
                            Image = m.Key.Image,
                            CategoryId = m.Key.CategoryId,
                            // SubCategoryId = m.Key.SubCategoryId,
                            SubCategoryName = this.subCategoriesRepository.All().FirstOrDefault(x => x.Id == m.Key.SubCategoryId).Name,
                            Quantity = m.Sum(x => x.Quantity),
                            //AvgPrice = m.Sum(x => x.UnitPrice) / (decimal)m.Count(),
                            AvgPrice = m.Sum(x => x.Cost) * (decimal)(1 + GlobalConstants.StandardMarkup) / (decimal)m.Sum(x=>x.Quantity),
                        };
                //.Select(x => x.MaterialId)
                //.FirstOrDefault(x => x.CategoryId == categoryId)

            if (categoryId.HasValue)
            {
                query = query
                    .Where(x => x.CategoryId == categoryId.Value);
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
