using Di2.Data.Models;
using Di2.Data.Models.Enums;
using Di2.Services.Data;
using Di2.Web.ViewModels.Orders.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Di2.Web.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new OrdersViewModel
            {
                Orders = this.orderService
                .GetAll<OrderViewModel>()
                .Where(x => x.StatusId == (int)OrderStatus.Created).ToList(),

            };
            viewModel.Orders = viewModel.Orders.Where(x => x.OrdererId == userId).ToList();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(OrdersViewModel input)
        {
            await this.orderService.UpdateOrder(input);
            await this.orderService.CompleteOrder(input);

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var receiptId = await this.orderService.CreateReceipt(userId);
            return this.RedirectToAction(nameof(this.Details),"Orders", new { id = receiptId } );
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var orders = this.orderService.GetReceiptOrders<OrderViewModel>(id);
            var recipientName = this.orderService.GetRecipientName(id);
            var viewModel = new ReceiptViewModel
            {
                Id = id,
                IssuedOn = DateTime.UtcNow,
                RecipientName = recipientName,
                Orders = orders,
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
