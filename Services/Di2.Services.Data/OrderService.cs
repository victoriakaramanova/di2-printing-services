using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Services.Mapping;
using Di2.Web.ViewModels.Orders.InputModels;
using Di2.Web.ViewModels.Orders.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDeletableEntityRepository<Delivery> deliveriesRepository;
        private readonly IDeletableEntityRepository<Material> materialsRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;

        public OrderService(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<Delivery> deliveriesRepository,
            IDeletableEntityRepository<Material> materialsRepository,
            IDeletableEntityRepository<SubCategory> subCategoriesRepository)
        {
            this.ordersRepository = ordersRepository;
            this.deliveriesRepository = deliveriesRepository;
            this.materialsRepository = materialsRepository;
            this.subCategoriesRepository = subCategoriesRepository;
        }

        public async Task<int> CreateOrder(OrderInputModel input, string userId)
        {
            var image = this.materialsRepository.All()
                .Where(x => x.Id == input.MaterialId)
                .Select(x => x.Image)
                .FirstOrDefault();
            var subCategoryId = this.materialsRepository.All()
                .Select(x => x.SubCategoryId).FirstOrDefault();
            var subCategoryName = this.subCategoriesRepository.All()
                .Where(x => x.Id == subCategoryId).Select(x => x.Name).FirstOrDefault();
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                MaterialId = input.MaterialId,
                MaterialName = input.MaterialName,
                Description = input.Description,
                ExtraInfo = input.ExtraInfo,
                Image = image,
                SubCategoryName = subCategoryName,
                Quantity = input.Quantity,
                AvgPrice = input.AvgPrice,
                //IssuedOn = DateTime.UtcNow,
                TotalPrice = input.AvgPrice * (decimal)input.Quantity,
                OrdererId = userId,
            };
            //order.OrderStatus = OrderStatus.Sent;
            await this.ordersRepository.AddAsync(order);
            int result =
                await this.ordersRepository.SaveChangesAsync();
            return result;
        }

        public List<T> GetAll<T>()
        {
            IQueryable<Order> query = this.ordersRepository.All();
            return query.To<T>().ToList();

        }

        public async Task UpdateOrder(OrdersViewModel input)
        {
            Order dbOrder;
            foreach (var order in input.Orders)
            {
                dbOrder = this.ordersRepository.All()
                    .Where(x => x.Id == order.Id).FirstOrDefault();
                if (dbOrder.Quantity != order.Quantity)
                {
                    dbOrder.Quantity = order.Quantity;
                    dbOrder.TotalPrice = (decimal)order.Quantity * dbOrder.AvgPrice;
                    this.ordersRepository.Update(dbOrder);
                    await this.ordersRepository.SaveChangesAsync();
                }
            }
        }

        public async Task CompleteOrder(OrdersViewModel input)
        {
            Order dbOrder;
            foreach (var order in input.Orders)
            {
                dbOrder = this.ordersRepository.All()
                    .Where(x => x.Id == order.Id).FirstOrDefault();
                if (dbOrder == null || dbOrder.OrderStatus != OrderStatus.Sent)
                {
                    throw new ArgumentException(nameof(dbOrder));
                }

                dbOrder.OrderStatus = OrderStatus.Sent;
                this.ordersRepository.Update(dbOrder);
                await this.ordersRepository.SaveChangesAsync();
            }
        }

        public async Task AssignOrdersToReceipt(Receipt receipt)
        {
            List<Order> dbOrders = await this.ordersRepository.All()
                .Where(x => x.OrdererId == receipt.RecipientId && x.OrderStatus == OrderStatus.Sent).ToListAsync();
            receipt.Orders = dbOrders;
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task<string> CreateReceipt(string recipientId)
        {
            var receipt = new Receipt
            {
                Id = Guid.NewGuid().ToString(),
                IssuedOn = DateTime.UtcNow,
                RecipientId = recipientId,
            };
            await this.ordersRepository.SaveChangesAsync();
            await this.AssignOrdersToReceipt(receipt);
            return receipt.Id;
        }

        public T GetById<T>(string id)
        {
            var receipt = this.ordersRepository.All()
                .Where(x => x.ReceiptId == id)
                .To<T>().FirstOrDefault();
            return receipt;
        }
    }
}
