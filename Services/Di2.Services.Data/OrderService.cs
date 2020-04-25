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
        private readonly IDeletableEntityRepository<Receipt> receiptsRepository;

        public OrderService(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<Delivery> deliveriesRepository,
            IDeletableEntityRepository<Material> materialsRepository,
            IDeletableEntityRepository<SubCategory> subCategoriesRepository,
            IDeletableEntityRepository<Receipt> receiptsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.deliveriesRepository = deliveriesRepository;
            this.materialsRepository = materialsRepository;
            this.subCategoriesRepository = subCategoriesRepository;
            this.receiptsRepository = receiptsRepository;
        }

        public async Task<int> CreateOrder(OrderInputModel input, string userId)
        {
            var image = this.materialsRepository.All()
                .Where(x => x.Id == input.MaterialId)
                .Select(x => x.Image)
                .FirstOrDefault();
           // var subCategoryId = this.materialsRepository.All()
                //.Select(x => x.SubCategoryId).FirstOrDefault();
           // var subCategoryName = this.subCategoriesRepository.All()
            //    .Where(x => x.Name == input.SubCategoryName).FirstOrDefault();
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                MaterialId = input.MaterialId,
                MaterialName = input.MaterialName,
                Description = input.Description,
                ExtraInfo = input.ExtraInfo,
                Image = image,
                SubCategoryName = input.SubCategoryName,
                Quantity = input.Quantity,
                AvgPrice = input.AvgPrice,
                StatusId = (int)OrderStatus.Created,
                IssuedOn = DateTime.UtcNow,
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
                    
                }
                await this.ordersRepository.SaveChangesAsync();
            }
        }

        public async Task CompleteOrder(OrdersViewModel input)
        {
            Order dbOrder;
            foreach (var order in input.Orders)
            {
                dbOrder = this.ordersRepository.All()
                    .Where(x => x.Id == order.Id).FirstOrDefault();
                if (dbOrder == null || dbOrder.StatusId != (int)OrderStatus.Created)
                {
                    throw new ArgumentException(nameof(dbOrder));
                }

                dbOrder.StatusId = (int)OrderStatus.Sent;
                this.ordersRepository.Update(dbOrder);
                await this.ordersRepository.SaveChangesAsync();
            }
        }

        public async Task<string> CreateReceipt(string recipientId)
        {
            var receipt = new Receipt
            {
                Id = Guid.NewGuid().ToString(),
                IssuedOn = DateTime.UtcNow,
                RecipientId = recipientId,
            };
            await this.receiptsRepository.AddAsync(receipt);
            await this.receiptsRepository.SaveChangesAsync();
            return receipt.Id;
        }

        public IEnumerable<T> GetReceiptOrders<T>(string receiptId)
        {
            Receipt receipt = this.receiptsRepository.All()
                .Where(x => x.Id == receiptId).FirstOrDefault();

            IQueryable<Order> query = this.ordersRepository.All()
                .Where(x => x.OrdererId == receipt.RecipientId);
            query = query.Where(x => x.StatusId == (int)OrderStatus.Sent);
            // var defaultDuration = TimeSpan.FromMinutes(1);
            // query = query.Where(x => x.CreatedOn - DateTime.UtcNow < defaultDuration);
            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var receipt = this.ordersRepository.All()
                .Where(x => x.ReceiptId == id)
                .To<T>().FirstOrDefault();
            return receipt;
        }

        public string GetRecipientName(string receiptId)
        {
            var recipient = this.receiptsRepository.All()
                .Where(x => x.Id == receiptId).FirstOrDefault().Recipient;
            return recipient.UserName;
        }

        public int GetCount()
        {
            return this.ordersRepository.All()
                .Where(x=>x.StatusId==(int)OrderStatus.Sent)
                .Count();
        }

        public async Task<string> DeleteAsync(string id)
        {
            var order = this.ordersRepository.All()
                    .FirstOrDefault(x => x.Id == id);
            this.ordersRepository.Delete(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }
    }
}
