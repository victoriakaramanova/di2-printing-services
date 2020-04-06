using Di2.Web.ViewModels.OrderSuppliers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface IProductsService
    {
        Task<int> Create(OrderSupplierViewModel input);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId);
    }
}
