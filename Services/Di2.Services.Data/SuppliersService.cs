namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SuppliersService : ISuppliersService
    {
        private readonly IDeletableEntityRepository<Supplier> supplierRepository;

        public SuppliersService(IDeletableEntityRepository<Supplier> supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public async Task AddAsync(string name, string address, string email, string phone)
        {
            var supplier = new Supplier
            {
                //Id = Guid.NewGuid().ToString(),
                Name = name,
                Address = address,
                Email = email,
                Phone = phone,
            };

            await this.supplierRepository.AddAsync(supplier);
            await this.supplierRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllSuppliers<T>()
        {
            return await this.supplierRepository
            .AllAsNoTracking()
            .To<T>()
            .ToArrayAsync();
        }
    }
}
