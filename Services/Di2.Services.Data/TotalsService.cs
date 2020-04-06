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
    public class TotalsService : ITotalsService
    {
        private readonly IRepository<OrderSupplier> orderSupplierRepository;

        public TotalsService(IDeletableEntityRepository<OrderSupplier> orderSupplierRepository)
        {
            this.orderSupplierRepository = orderSupplierRepository;
        }

        public async Task<int> ChangeOrderStatus(int orderId, bool isCompleted)
        {
            var order = this.orderSupplierRepository.All()
                .FirstOrDefault(x => x.Id == orderId);
            if (isCompleted)
            {
                order.Status = OrderStatus.Completed;
            }
            else if (!isCompleted)
            {
                order.Status = OrderStatus.Canceled;
            }
            else
            {
                order.Status = OrderStatus.Sent;
            }

            this.orderSupplierRepository.Update(order);
            await this.orderSupplierRepository.SaveChangesAsync();
            return (int)order.Status;
        }
    }
}
