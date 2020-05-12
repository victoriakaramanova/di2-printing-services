using Di2.Common;
using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Services.Messaging;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailSender sender;
        //private readonly UserManager<ApplicationUser> userManager;

        public TotalsCustomerService(
            IDeletableEntityRepository<Order> ordersRepository,
            IOrderService orderService,
            IEmailSender sender
            //UserManager<ApplicationUser> userManager
            )
        {
            this.ordersRepository = ordersRepository;
            this.orderService = orderService;
            this.sender = sender;
            //this.userManager = userManager;
        }

        public async Task<int> ChangeOrderStatus(string orderId, int isCompleted, ApplicationUser orderer)
        {
            var order = this.ordersRepository.All()
                .FirstOrDefault(x => x.Id == orderId);
            //StringBuilder sb = new StringBuilder();
           // var user = this.ordersRepository.All().FirstOrDefault(x=>x.Orderer.Email==ordererEmail)

            if (isCompleted == 0)
            {
                order.StatusId = (int)OrderStatus.Completed;
                await this.sender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, orderer.Email, $"Ваша поръчка по разписка " + order.ReceiptId, $"Здравейте, {orderer.UserName}, Поръчката Ви " + order.Id + $" на {order.MaterialName}, {order.Quantity} за {order.TotalPrice.ToString("f2")} лв е готова. Поздрави - {GlobalConstants.SystemName}");
            }
            else
                if (isCompleted == 1)
            {
                order.StatusId = (int)OrderStatus.Sent;
            }
            else
            {
                order.StatusId = (int)OrderStatus.Canceled;
                await this.sender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, orderer.Email, $"Отказ на Ваша поръчка по разписка " + order.ReceiptId, $"Здравейте, {orderer.UserName}, Поръчката Ви " + order.Id + $" на {order.MaterialName}, {order.Quantity} за {order.TotalPrice.ToString("f2")} лв е отказана. Моля потърсете ни за подробности. Поздрави - {GlobalConstants.SystemName}");
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
