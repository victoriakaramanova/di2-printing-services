using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Web.ViewModels.OrderSuppliers;
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
        private readonly IDeliveriesService deliveriesService;

        public TotalsService(
            IDeletableEntityRepository<OrderSupplier> orderSupplierRepository,
            IDeliveriesService deliveriesService)
        {
            this.orderSupplierRepository = orderSupplierRepository;
            this.deliveriesService = deliveriesService;
        }

        public async Task<int> ChangeOrderStatus(int orderId, bool isCompleted)
        {
            var order = this.orderSupplierRepository.All()
                .FirstOrDefault(x => x.Id == orderId);
            if (isCompleted)
            {
                order.Status = OrderStatus.Completed;
                this.orderSupplierRepository.Update(order);
                await this.orderSupplierRepository.SaveChangesAsync();

                var viewModel = new OrderSupplierViewModel
                {
                    Id = order.Id,
                    MaterialId = order.MaterialId,
                    SupplierId = order.SupplierId,
                    OrderDate = order.OrderDate,
                    Quantity = order.Quantity,
                    UnitPrice = order.UnitPrice,
                    TotalPrice = order.TotalPrice,
                };
                var deliveredProductId = await this.deliveriesService.Create(viewModel);

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
