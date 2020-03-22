namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.PriceLists.InputModels;

    public interface IPriceListsService
    {
        Task CreateAsync(CreatePriceListInputModel input, string userId);

        IEnumerable<T> GetAllPriceLists<T>();

        Task<IEnumerable<Material>> GetAllMaterials<Material>();

        Task<IEnumerable<Supplier>> GetAllSuppliers<Supplier>();
    }
}
