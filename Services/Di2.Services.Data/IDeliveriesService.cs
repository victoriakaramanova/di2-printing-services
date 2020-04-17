using Di2.Web.ViewModels.Deliveries;
using Di2.Web.ViewModels.OrderSuppliers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface IDeliveriesService
    {
        Task<int> Create(OrderSupplierViewModel input);

        T GetById<T>(int id);

        IEnumerable<T> GetAllProducts<T>(int? categoryId);
    }
}
