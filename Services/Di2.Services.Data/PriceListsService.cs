namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels;
    using Di2.Web.ViewModels.PriceLists.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class PriceListsService : IPriceListsService
    {
        private readonly IDeletableEntityRepository<PriceList> priceListsRepository;

        public PriceListsService(IDeletableEntityRepository<PriceList> priceListsRepository)
        {
            this.priceListsRepository = priceListsRepository;
        }

        public async Task<IEnumerable<T>> GetAllPriceLists<T>()
        {
            return await this.priceListsRepository
            .AllAsNoTracking()
            .To<T>()
            .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAPriceListPerMaterial<T>(int materialId)
        {
            return await this.priceListsRepository
            .AllAsNoTracking()
            .Select(x => x.MaterialId == materialId)
            .To<T>()
            .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAPriceListPerSupplier<T>(int supplierId)
        {
            return await this.priceListsRepository
           .AllAsNoTracking()
           .Select(x => x.SupplierId == supplierId)
           .To<T>()
           .ToArrayAsync();
        }

        public async Task CreateAsync(CreateInputModel input)
        {
            var priceList = new PriceList
            {
                MaterialId = input.MaterialId,
                SupplierId = input.SupplierId,
                MinimumQuantityPerOrder = input.MinimumQuantityPerOrder,
                UnitPrice = input.UnitPrice,
            };

            await this.priceListsRepository.AddAsync(priceList);
            await this.priceListsRepository.SaveChangesAsync();
        }
    }
}
