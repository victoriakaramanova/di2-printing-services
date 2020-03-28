namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.OrderSuppliers;

    public interface IOrderSupplierService
    {
        Task<int> CreateAsync(CreateOrderSupplierInputModel input, string userId);

        // Task<int> CreateAsync(DateTime orderDate, int materialId, int supplierId, double quantity, decimal unitPrice, decimal totalPrice, string userId);
        IEnumerable<T> GetAllOrderSuppliers<T>();
    }
}
