namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISuppliersService
    {
        Task AddAsync(string name, string address, string email, string phone);

        Task<IEnumerable<T>> GetAllSuppliers<T>();
    }
}
