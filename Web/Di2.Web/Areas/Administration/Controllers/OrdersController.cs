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

namespace Di2.Web.Areas.Administration.Controllers
{
    public class OrdersController : AdministrationController
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
            var viewModel = new ListCompleteViewModel
            {
                Orders = this.orderService
                .GetAll<CompleteViewModel>()
                .Where(x => x.StatusId == (int)OrderStatus.Sent)
                .OrderBy(x=>x.IssuedOn)
                .ToList(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllCreated()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new OrdersViewModel
            {
                Orders = this.orderService
                .GetAll<OrderViewModel>()
                .Where(x => x.StatusId == (int)OrderStatus.Created)
                .OrderBy(x=>x.IssuedOn).ToList(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Complete(OrdersViewModel input)
        {
            await this.orderService.UpdateOrder(input);
            await this.orderService.CompleteOrder(input);

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var receiptId = await this.orderService.CreateReceipt(userId);
            await this.orderService.AssignReceiptToOrders(receiptId);
            await this.orderService.SendOrderReceiptMailCustomer(userId, receiptId);
            return this.RedirectToAction(nameof(this.Details),"Orders", new { id = receiptId } );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminComplete(OrdersViewModel input)
        {
            await this.orderService.AdminCompleteOrder(input);

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var orders = this.orderService.GetReceiptOrders<OrderViewModel>(id);

            var recipientName = this.orderService.GetRecipientName(id);
            var viewModel = new ReceiptViewModel
            {
                Id = id,
                IssuedOn = DateTime.UtcNow,
                RecipientName = recipientName,
                Orders = orders.Where(x=>x.ReceiptId==id),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.orderService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
