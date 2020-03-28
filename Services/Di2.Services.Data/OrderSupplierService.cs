namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.OrderSuppliers;
    using Di2.Web.ViewModels.PriceLists.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class OrderSupplierService : IOrderSupplierService
    {
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;
        private readonly IDeletableEntityRepository<OrderStatus> orderStatusRepository;
        private readonly IDeletableEntityRepository<PriceList> priceListRepository;

        public OrderSupplierService(
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository,
            IDeletableEntityRepository<OrderStatus> orderStatusRepository,
            IDeletableEntityRepository<PriceList> priceListRepository)
        {
            this.orderSuppliersRepository = orderSuppliersRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.priceListRepository = priceListRepository;
        }

        public async Task<int> CreateAsync(CreateOrderSupplierInputModel input, string userId)
        {
            // var priceLists = this.priceListRepository.All().To<PriceListViewModel>().ToList();
            /*var priceList = this.priceListRepository.All().FirstOrDefault(x => x.Id == input.PriceListId);
            var materialId = priceList.Material.Id;
            var supplierId = priceList.Supplier.Id;
            var unitPrice = priceList.UnitPrice;
            var minimumQty = priceList.MinimumQuantityPerOrder;*/

            /*var orderSupplier = new OrderSupplier
            {
                PriceListId = input.PriceListId,
                OrderDate = DateTime.UtcNow,
                // MaterialId = materialId,
                // SupplierId = supplierId,
                Quantity = minimumQty,
                UnitPrice = unitPrice,
                TotalPrice = unitPrice * (decimal)minimumQty,
                UserId = userId,
            };
            orderSupplier.Status = await this.orderStatusRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == "Sent");

            await this.orderSuppliersRepository.AddAsync(orderSupplier);
            await this.orderSuppliersRepository.SaveChangesAsync();*/
            return 0; //orderSupplier.Id;
        }

        public IEnumerable<T> GetAllOrderSuppliers<T>()
        {
            return this.orderSuppliersRepository
           .All().OrderByDescending(x => x.Quantity)
           .To<T>()
           .ToList();
        }
    }
}
