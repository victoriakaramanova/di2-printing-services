namespace Di2.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Data.Models.Enums;
    using Di2.Services.Data;
    using Di2.Web.ViewModels.Categories.ViewModels;
    using Di2.Web.ViewModels.Orders.ViewModels;
    using Di2.Web.ViewModels.Pictures;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeliveriesService deliveriesService;
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IPictureService pictureService;

        public OrdersController(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IDeliveriesService deliveriesService,
            IDeletableEntityRepository<Order> ordersRepository,
            IPictureService pictureService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.deliveriesService = deliveriesService;
            this.ordersRepository = ordersRepository;
            this.pictureService = pictureService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            double availableQty; 
            var viewModel = new OrdersViewModel
            {
                DeliveryAddress = user.Address,
                Orders = this.ordersRepository.All()
                    .Where(x => x.StatusId == 2 && x.OrdererId == userId)
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
                        Description = x.Description,
                        OrdererId = userId,
                        Orderer = user,
                        Pictures = this.pictureService.GetAllPictures<PictureViewModel>(x.Id),
                    })
                    .ToList(),
                //this.orderService.
                //GetAllCreated<OrderViewModel>(userId),
            };
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
                /*this.orderService
                .GetAll<OrderViewModel>().Distinct()
                .Where(x => x.StatusId == (int)OrderStatus.Created).ToList(),
            };
            viewModel.Orders = viewModel.Orders
                .Where(x => x.OrdererId == userId).ToList();
            foreach (var order in viewModel.Orders)
            {
                order.AvailableQuantity = this.orderService.GetAvlQtyPerProduct(order.MaterialId);
            }*/

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(OrdersViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Order dbOrder;
            /*foreach (var order in input.Orders)
            {
                dbOrder = this.ordersRepository.All()
                    .Where(x => x.Id == order.Id).FirstOrDefault();
                if (dbOrder.Quantity != order.Quantity)
                {
                    if (dbOrder.Quantity < order.Quantity)
                    {
                        bool enough = this.deliveriesService
                        .GetDeliveredQuantityPerProduct(dbOrder.MaterialId, order.Quantity);
                        if (enough == false)
                            {
                            this.ModelState.AddModelError("MessageError", "Въведете по-малко количество");
                        }
                    }
                    if (this.ModelState.IsValid)
                    { */
            await this.orderService.UpdateOrder(input);
            /*        }
                }
            }*/
            if (!this.ModelState.IsValid)
            {
                return this.View("All", new OrdersViewModel
                {
                    DeliveryAddress = user.Address,

                    //Orders = this.orderService.GetAllCreated<OrderViewModel>(userId),
                    Orders = this.ordersRepository.All()
                    .Where(x => x.StatusId == 2 && x.OrdererId == userId)
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
                        Description = x.Description,
                        OrdererId = userId,
                        Orderer = user,
                        Pictures = this.pictureService.GetAllPictures<PictureViewModel>(x.Id),
                    })
                    .ToList(),
                });
            }

            /*var updateQty = await this.orderService.UpdateOrder(input);
            if (updateQty == 0)
            {
                this.ModelState.AddModelError(string.Empty, "Въведете по-малко количество или се свържете с нас, за да уточним подробностите!");
                return this.View("All");
            }*/

            await this.orderService.CompleteOrder(input);

            var deliveryAddress = input.DeliveryAddress;
            //var user = await this.userManager.GetUserAsync(this.User);
            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var receiptId = await this.orderService.CreateReceipt(userId, deliveryAddress);
            await this.orderService.AssignReceiptToOrders(receiptId);
            await this.orderService.SendOrderReceiptMailCustomer(userId, receiptId);
            return this.RedirectToAction(nameof(this.Details), "Orders", new { id = receiptId });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            IEnumerable<FinalOrderViewModel> orders = this.orderService.GetReceiptOrders<FinalOrderViewModel>(id);
            var aggregateOrder = this.orderService.GetById<OrdersViewModel>(id);
            var deliveryAddress = aggregateOrder.DeliveryAddress;
            var recipientName = this.orderService.GetRecipientName(id);
            var viewModel = new ReceiptViewModel
            {
                Id = id,
                IssuedOn = DateTime.UtcNow,
                RecipientName = recipientName,
                Orders = orders,
                DeliveryAddress = deliveryAddress,
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
            //return this.RedirectToAction(nameof(this.Details), new { id = receiptId });
        }

    }
}
