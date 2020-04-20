using Di2.Web.ViewModels.Orders.InputModels;
using Di2.Web.ViewModels.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface IOrderService
    {
        Task<int> CreateOrder(OrderInputModel input, string userId);
    }
}
