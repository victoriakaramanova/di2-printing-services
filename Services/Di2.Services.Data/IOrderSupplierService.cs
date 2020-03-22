using Di2.Web.ViewModels.OrderSupplier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface IOrderSupplierService
    {
        Task<int> CreateAsync(DateTime orderDate, int materialId, int supplierId, double quantity, decimal unitPrice, decimal totalPrice, string userId);

        IEnumerable<T> GetAllOrderSuppliers<T>();
    }
}
