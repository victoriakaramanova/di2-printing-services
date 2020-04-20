using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Orders.InputModels;
using Di2.Web.ViewModels.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrderService(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }
        public async Task<int> CreateOrder(OrderInputModel input, string userId)
        {
            var material = this.ordersRepository.All()
                .Select(x=>x.Material)
                .Where(x=>x.Id==input.MaterialId)
                .FirstOrDefault();
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                MaterialId = input.MaterialId,
                Material = material,
                Quantity = input.Quantity,
                IssuedOn = DateTime.UtcNow,
                OrdererId = userId,
            };
            order.OrderStatus = OrderStatus.Sent;
            await this.ordersRepository.AddAsync(order);
            int result = 
                await this.ordersRepository.SaveChangesAsync();
            return result;
        }

        
    }
}
