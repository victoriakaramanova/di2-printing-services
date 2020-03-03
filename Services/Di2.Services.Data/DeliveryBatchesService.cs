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
    using Di2.Web.ViewModels.DeliveryBatches.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class DeliveryBatchesService : IDeliveryBatchesService
    {
        private readonly IDeletableEntityRepository<DeliveryBatch> deliveryBatchesRepository;

        public DeliveryBatchesService(IDeletableEntityRepository<DeliveryBatch> deliveryBatchesRepository)
        {
            this.deliveryBatchesRepository = deliveryBatchesRepository;
        }

        public async Task<IEnumerable<T>> GetAllDeliveryBatches<T>()
        {
            return await this.deliveryBatchesRepository
            .AllAsNoTracking()
            .To<T>()
            .ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAllDeliveryBatchesPerSupplier<T>(int supplierId)
        {
            return await this.deliveryBatchesRepository
            .AllAsNoTracking()
            .Select(x => x.SupplierId == supplierId)
            .To<T>()
            .ToArrayAsync();
        }

        public async Task CreateBatch(CreateBatchInputModel input)
        {
            DeliveryBatch material = (DeliveryBatch)this.deliveryBatchesRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.MaterialId == input.MaterialId);
            DeliveryBatch supplier = this.deliveryBatchesRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.SupplierId == input.SupplierId) as DeliveryBatch;

            var deliveryBatch = new DeliveryBatch
            {
                MaterialId = input.MaterialId,
                SupplierId = input.SupplierId,
                Quantity = input.Quantity,
            };

            await this.deliveryBatchesRepository.AddAsync(deliveryBatch);
            await this.deliveryBatchesRepository.SaveChangesAsync();
        }
    }
}
