namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Di2.Common;
    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Services.Messaging;
    using Di2.Web.ViewModels.OrderSuppliers;
    using Di2.Web.ViewModels.PriceLists.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class OrderSupplierService : IOrderSupplierService
    {
        private readonly IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository;
        private readonly IDeletableEntityRepository<OrderStatus> orderStatusRepository;
        private readonly IDeletableEntityRepository<PriceList> priceListRepository;
        private readonly IEmailSender sender;

        public OrderSupplierService(
            IDeletableEntityRepository<OrderSupplier> orderSuppliersRepository,
            IDeletableEntityRepository<OrderStatus> orderStatusRepository,
            IDeletableEntityRepository<PriceList> priceListRepository,
            IEmailSender sender)
        {
            this.orderSuppliersRepository = orderSuppliersRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.priceListRepository = priceListRepository;
            this.sender = sender;
        }

        public async Task<OrderSupplier> CreateAsync(CreateOrderSupplierInputModel orderSupplierInput, PriceListViewModel priceListInput, string userId)
        {
            if (orderSupplierInput.OrderDate == null)
            {
                return null;
            }

            double number;
            double.TryParse(orderSupplierInput.Quantity.ToString(), out number);
            var orderSupplier = new OrderSupplier
            {
                MaterialId = priceListInput.MaterialId,
                SupplierId = priceListInput.SupplierId,
                OrderDate = orderSupplierInput.OrderDate,
                Quantity = number,
                UnitPrice = priceListInput.UnitPrice,
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

        public async Task SendMailSupplier(List<OrderSupplier> orderSuppliers)
        {
            foreach (var supplier in orderSuppliers.GroupBy(x => x.SupplierId))
            {
                // var materials = supplier.Select(x => x.SupplierId)
                // .ToList();

                StringBuilder sb = new StringBuilder();
                sb.Append("Дата за изпълнение;Име на материал;Описание на материал;Количество;Единична цена;Крайна цена" + Environment.NewLine);

                foreach (var material in supplier)
                {
                    sb.AppendFormat(
                            "{0};{1};{2};{3};{4};{5};{6}",
                            material.OrderDate,
                            material.Material.Name,
                            material.Material.Description,
                            material.Quantity,
                            material.UnitPrice,
                            material.TotalPrice,
                            Environment.NewLine
                    );
                }

                var data = Encoding.UTF8.GetBytes(sb.ToString());
                var res = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
                var attachmentFileName = $"{GlobalConstants.SystemName} - {DateTime.UtcNow.ToShortDateString()}.csv";
                var mimeType = "text/csv"; // charset=UTF-8

                var attch = new EmailAttachment
                {
                    MimeType = mimeType,
                    FileName = attachmentFileName,
                    Content = res,
                };
                var attchList = new List<EmailAttachment>();
                attchList.Add(attch);

                await this.sender.SendEmailAsync(GlobalConstants.Email, GlobalConstants.SystemName, supplier.Select(x => x.Supplier.Email).FirstOrDefault(), $"Поръчка за {DateTime.UtcNow.ToShortDateString()}", $"Здравейте, приложена е поръчката. Поздрави - {GlobalConstants.SystemName}", attchList);
            }
        }

        public IEnumerable<T> GetAllOrderSuppliers<T>()
        {
            // var supplier = AutoMapperConfig.MapperInstance.Map<OrderSuppliersListViewModel>();
            IQueryable<OrderSupplier> query = this.orderSuppliersRepository.All();
            return query.To<T>().ToList();
        }
    }
}
