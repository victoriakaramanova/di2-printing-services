namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.DeliveryBatches.InputModels;

    public interface IDeliveryBatchesService
    {
        Task CreateBatch(CreateBatchInputModel input);

        Task<IEnumerable<T>> GetAllDeliveryBatches<T>();

        Task<IEnumerable<T>> GetAllDeliveryBatchesPerSupplier<T>(string supplierId);
    }
}
