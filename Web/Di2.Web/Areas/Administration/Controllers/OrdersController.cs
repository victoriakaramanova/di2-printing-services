namespace Di2.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Data.Models.Enums;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Orders.ViewModels;
    using Di2.Web.ViewModels.Pictures;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministrationController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<Receipt> receiptsRepository;
        private readonly IDeliveriesService deliveriesService;
        private readonly IPictureService pictureService;

        public OrdersController(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<Receipt> receiptsRepository,
            IDeliveriesService deliveriesService,
            IPictureService pictureService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.ordersRepository = ordersRepository;
            this.receiptsRepository = receiptsRepository;
            this.deliveriesService = deliveriesService;
            this.pictureService = pictureService;
        }

        //[Authorize]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new ListCompleteViewModel
            {
                Orders = this.ordersRepository.All()
                .Where(x => x.StatusId != (int)OrderStatus.Sent && x.StatusId != (int)OrderStatus.Created)
                .AsEnumerable()
                .Select(x => new CompleteViewModel
                {
                    Id = x.Id,
                    IssuedOn = x.IssuedOn,
                    MaterialId = x.MaterialId,
                    MaterialName = x.MaterialName,
                    Quantity = x.Quantity,
                    AvgPrice = x.AvgPrice,
                    TotalPrice = x.TotalPrice,
                    AvailableQuantity = this.deliveriesService.GetDeliveredQuantityPerProduct(x.MaterialId),
                    OrdererId = x.OrdererId,
                    Orderer = this.userManager.FindByIdAsync(x.OrdererId).Result.UserName,
                    Pictures = this.pictureService.GetAllPictures<PictureViewModel>(x.Id),
                    DeliveryAddress = x.DeliveryAddress,
                    ReceiptId = x.ReceiptId,
                    StatusId = x.StatusId,
                })
                //.GetAll<CompleteViewModel>().Distinct()
                .OrderBy(x => x.IssuedOn)
                .ToList(),
                /*Orders = this.orderService
                .GetAll<CompleteViewModel>().Distinct()
                .Where(x => x.StatusId != (int)OrderStatus.Sent && x.StatusId != (int)OrderStatus.Created)
                .OrderBy(x => x.IssuedOn)
                .ToList(),*/
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllSent()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new ListCompleteViewModel
            {
                Orders = this.ordersRepository.All()
                .Where(x => x.StatusId == (int)OrderStatus.Sent)
                .AsEnumerable()
                .Select(x => new CompleteViewModel
                {
                    Id = x.Id,
                    IssuedOn = x.IssuedOn,
                    MaterialId = x.MaterialId,
                    MaterialName = x.MaterialName,
                    Quantity = x.Quantity,
                    AvgPrice = x.AvgPrice,
                    TotalPrice = x.TotalPrice,
                    AvailableQuantity = this.deliveriesService.GetDeliveredQuantityPerProduct(x.MaterialId),
                    OrdererId = x.OrdererId,
                    Orderer = this.userManager.FindByIdAsync(x.OrdererId).Result.UserName,
                    Pictures = this.pictureService.GetAllPictures<PictureViewModel>(x.Id),
                    DeliveryAddress = x.DeliveryAddress,
                    ReceiptId = x.ReceiptId,
                    StatusId = x.StatusId, 
                })
                //.GetAll<CompleteViewModel>().Distinct()
                .OrderBy(x => x.IssuedOn)
                .ToList(),
            };

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllCreated()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new OrdersViewModel
            {
                Orders = this.ordersRepository.All()
                    .Where(x => x.StatusId == 2)
                    .AsEnumerable()
                    .Select(x => new OrderViewModel
                    {
                        Id = x.Id,
                        MaterialId = x.MaterialId,
                        MaterialName = x.MaterialName,
                        Quantity = x.Quantity,
                        AvgPrice = x.AvgPrice,
                        TotalPrice = x.TotalPrice,
                        AvailableQuantity = this.deliveriesService.GetDeliveredQuantityPerProduct(x.MaterialId),
                        Image = x.Image,
                        IssuedOn = x.IssuedOn,
                        OrdererId = x.OrdererId,
                        Orderer = x.Orderer,
                        Pictures = this.pictureService.GetAllPictures<PictureViewModel>(x.Id),
                    }).OrderBy(x => x.IssuedOn)
                    .ToList(),
                //.GetAll<OrderViewModel>()
                //.Where(x => x.StatusId == (int)OrderStatus.Created)
                //.OrderBy(x => x.IssuedOn).ToList(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Complete(OrdersViewModel input)
        {
            await this.orderService.UpdateOrder(input);
            await this.orderService.CompleteOrder(input);

            var deliveryAddress = input.DeliveryAddress;
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var receiptId = await this.orderService.CreateReceipt(userId,deliveryAddress);
            //await this.orderService.AssignReceiptToOrders(receiptId);
            //var receipt = this.receiptsRepository.All()
            //    .Where(x => x.Id == receiptId).FirstOrDefault();
            var orders = this.ordersRepository.All()
                //.Where(x => x.Receipt.RecipientId == userId)
                .Where(x => x.StatusId == (int)OrderStatus.Sent)
                .Where(x => x.ReceiptId == null) //NB!!!
                .ToList();
            foreach (var order in orders)
            {
                order.ReceiptId = receiptId;
                this.ordersRepository.Update(order);
                await this.ordersRepository.SaveChangesAsync();
            }

            await this.orderService.SendOrderReceiptMailCustomer(userId, receiptId);
            return this.RedirectToAction(nameof(this.Details), "Orders", new { id = receiptId } );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminComplete(OrdersViewModel input)
        {
            await this.orderService.AdminCompleteOrder(input);
            var deliveryAddress = input.DeliveryAddress;
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           // var receiptId = await this.orderService.CreateReceipt(userId, deliveryAddress);
           // await this.orderService.AssignReceiptToOrders(receiptId);
           // await this.orderService.SendOrderReceiptMailCustomer(userId, receiptId);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {
            var user = this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var orders = this.orderService.GetReceiptOrdersByReceiptId<FinalOrderViewModel>(id,userId);
            Receipt receipt = this.receiptsRepository.All()
                .Where(x => x.Id == id).FirstOrDefault();
            /*IQueryable<Order> query = this.ordersRepository.All()
                .Where(x => x.OrdererId == receipt.RecipientId);
            query = query.Where(x => x.StatusId == (int)OrderStatus.Sent);
            return query.To<T>().ToList();*/

            // var aggregateOrder = this.orderService.GetById<OrdersViewModel>(id);

            IEnumerable<FinalOrderViewModel> orders = this.orderService.GetReceiptOrders<FinalOrderViewModel>(id, (int)OrderStatus.Sent);
            
            var deliveryAddress = receipt.DeliveryAddress;//aggregateOrder.DeliveryAddress;
            var recipientName = this.orderService.GetRecipientName(id);
            var viewModel = new ReceiptViewModel
            {
                Id = id,
                IssuedOn = DateTime.UtcNow,
                //RecipientName = recipientName,
            Orders = orders,
                //orders.Where(x => x.ReceiptId == id),
                DeliveryAddress = deliveryAddress,
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        //[HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.orderService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
