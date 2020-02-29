namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPriceListsService
    {
        Task<IEnumerable<T>> GetAPriceListPerSupplier<T>(int supplierId);

        Task<IEnumerable<T>> GetAPriceListPerMaterial<T>(int materialId);

        Task<IEnumerable<T>> GetAllPriceLists<T>();
    }
}
