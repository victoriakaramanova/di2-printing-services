using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public class TotalsCustomerService : ITotalsCustomerService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IOrderService orderService;

        public TotalsCustomerService(
            IDeletableEntityRepository<Order> ordersRepository,
            IOrderService orderService)
        {
            this.ordersRepository = ordersRepository;
            this.orderService = orderService;
        }

        public async Task<int> ChangeOrderStatus(string orderId, int isCompleted)
        {
            var order = this.ordersRepository.All()
                .FirstOrDefault(x => x.Id == orderId);

            if (isCompleted == 0)
            {
                order.StatusId = (int)OrderStatus.Completed;
            }
            else
                if (isCompleted == 1)
            {
                order.StatusId = (int)OrderStatus.Sent;
            }
            else
            {
                order.StatusId = (int)OrderStatus.Canceled;
            }
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            return order.StatusId;
        }

        public int GetStatus(string orderId)
        {
            var status = this.ordersRepository.All()
                .FirstOrDefault(x => x.Id == orderId);
            var s = Enum.Parse(typeof(OrderStatus), status.ToString()) as Enum;
            return Convert.ToInt32(s);
        }
    }
}
