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
    using Di2.Web.ViewModels.OrderSupplier;
    using Microsoft.EntityFrameworkCore;

    public class OrderSupplierService : IOrderSupplierService
    {
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;
        private readonly IDeletableEntityRepository<OrderStatus> orderStatusRepository;

        public OrderSupplierService(
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository,
            IDeletableEntityRepository<OrderStatus> orderStatusRepository)
        {
            this.orderSuppliersRepository = orderSuppliersRepository;
            this.orderStatusRepository = orderStatusRepository;
        }

        public async Task<int> CreateAsync(DateTime orderDate, int materialId, int supplierId, double quantity, decimal unitPrice, decimal totalPrice, string userId)
        {
            var orderSupplier = new OrderSupplier
            {
                OrderDate = orderDate,
                MaterialId = materialId,
                SupplierId = supplierId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice,
                UserId = userId,
            };
            orderSupplier.Status = await this.orderStatusRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == "Sent");

            await this.orderSuppliersRepository.AddAsync(orderSupplier);
            await this.orderSuppliersRepository.SaveChangesAsync();
            return orderSupplier.Id;
        }

        public IEnumerable<T> GetAllOrderSuppliers<T>()
        {
            return this.orderSuppliersRepository
           .All().OrderBy(x => x.Supplier.Name)
           .To<T>()
           .ToArray();
        }
    }
}
