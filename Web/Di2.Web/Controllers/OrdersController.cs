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
    using Di2.Web.ViewModels.Orders.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeliveriesService deliveriesService;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersController(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IDeliveriesService deliveriesService,
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.deliveriesService = deliveriesService;
            this.ordersRepository = ordersRepository;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new OrdersViewModel
            {
                Orders = this.orderService
                .GetAll<OrderViewModel>().Distinct()
                .Where(x => x.StatusId == (int)OrderStatus.Created).ToList(),
            };
            viewModel.Orders = viewModel.Orders
                .Where(x => x.OrdererId == userId).ToList();
            foreach (var order in viewModel.Orders)
            {
                order.AvailableQuantity = this.orderService.GetAvlQtyPerProduct(order.MaterialId);
            }

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
                    Orders = this.orderService
                                .GetAll<OrderViewModel>().Distinct()
                                .Where(x => x.StatusId == (int)OrderStatus.Created).ToList()
                                .Where(x => x.OrdererId == userId).ToList(),
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
            return this.RedirectToAction(nameof(this.Details), "Orders", new { id = receiptId } );
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var orders = this.orderService.GetReceiptOrders<OrderViewModel>(id);
            var aggregateOrder = this.orderService.GetById<OrdersViewModel>(id);
            var deliveryAddress = aggregateOrder.DeliveryAddress;
            var recipientName = this.orderService.GetRecipientName(id);
            var viewModel = new ReceiptViewModel
            {
                Id = id,
                IssuedOn = DateTime.UtcNow,
                RecipientName = recipientName,
                Orders = orders.Where(x => x.ReceiptId == id),
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
