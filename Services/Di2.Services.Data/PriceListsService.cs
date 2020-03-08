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
        private readonly IDeletableEntityRepository<Material> materialsRepository;
        private readonly IDeletableEntityRepository<Supplier> suppliersRepository;

        public PriceListsService(
            IDeletableEntityRepository<PriceList> priceListsRepository,
            IDeletableEntityRepository<Material> materialsRepository,
            IDeletableEntityRepository<Supplier> suppliersRepository)
        {
            this.priceListsRepository = priceListsRepository;
            this.materialsRepository = materialsRepository;
            this.suppliersRepository = suppliersRepository;
        }

        public IEnumerable<T> GetAllPriceLists<T>()
        {
            return this.priceListsRepository
            .All()
            .To<T>()
            .ToArray();
        }

        public async Task<IEnumerable<T>> GetAllMaterials<T>()
        {
            return await this.materialsRepository.All()
                .To<T>().ToListAsync();
            //return await this.priceListsRepository
            //    .All()
            //    .Include(x => x.Material)
            //    .To<T>()
            //    .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllSuppliers<T>()
        {
            return await this.suppliersRepository.All()
                .To<T>().ToListAsync();
            //return await this.priceListsRepository
            //    .All()
            //    .Include(x => x.Supplier)
            //    .To<T>()
            //    .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetSupplierstPerMaterial<T>(string material)
        {
            return await this.priceListsRepository
            .All()
            .Where(x => x.Material.Name == material)
            .To<T>()
            .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetMaterialsPerSupplier<T>(string supplier)
        {
            return await this.priceListsRepository
           .All()
           .Select(x => x.Supplier.Name == supplier)
           .To<T>()
           .ToArrayAsync();
        }

        public async Task CreateAsync(CreatePriceListInputModel input)
        {
            var mId = this.materialsRepository.All()
                .Where(x => x.Name == input.Material.Name)
                .Select(x => x.Id).FirstOrDefault();
            var sId = this.suppliersRepository.All()
                .Where(x => x.Name == input.Supplier.Name)
                .Select(x => x.Id).FirstOrDefault();
            var priceList = new PriceList
            {
                MaterialId = mId,
                SupplierId = sId,
                MinimumQuantityPerOrder = input.MinimumQuantityPerOrder,
                UnitPrice = input.UnitPrice,
            };

            await this.priceListsRepository.AddAsync(priceList);
            await this.priceListsRepository.SaveChangesAsync();
        }

        public async Task GetSupplierId()
        {
            await this.priceListsRepository
           .All()
           .Select(x => x.SupplierId)
           .FirstOrDefaultAsync();
        }
    }
}
