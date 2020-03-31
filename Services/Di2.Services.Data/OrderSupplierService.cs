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

        public async Task<OrderSupplier> CreateAsync(CreateOrderSupplierInputModel orderSupplierInput, PriceListViewModel priceListInput,string userId)
        {
            if (orderSupplierInput.OrderDate == null)
            {
                return null;
            }

            var priceList = new PriceList
            {
                MaterialId = priceListInput.MaterialId,
                SupplierId = priceListInput.SupplierId,
                MinimumQuantityPerOrder = priceListInput.MinimumQuantityPerOrder,
                UnitPrice = priceListInput.UnitPrice,
            };
            double number;
            double.TryParse(orderSupplierInput.Quantity.ToString(), out number);
            var orderSupplier = new OrderSupplier
            {
                PriceList = priceList,
                OrderDate = orderSupplierInput.OrderDate,
                Quantity = number,
                TotalPrice = priceListInput.UnitPrice * (decimal)orderSupplierInput.Quantity,
                UserId = userId,
            };
            orderSupplier.Status = await this.orderStatusRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == "Sent");

            await this.orderSuppliersRepository.AddAsync(orderSupplier);
            await this.orderSuppliersRepository.SaveChangesAsync();
            return orderSupplier;
        }

        public void Populate(List<CreateOrderSupplierInputModel> input)
        {
            var qty = this.priceListRepository.All().Count();
            //CreateOrderSupplierInputModel orderDefaults = input;
            //for (int i = 0; i < qty; i++)
            //{
            //    input[i].OrderDate = DateTime.UtcNow;
            //    input[i].Quantity = 0;
            //    input[i].TotalPrice = 0;
            //};
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
