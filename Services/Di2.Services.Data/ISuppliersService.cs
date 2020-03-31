namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISuppliersService
    {
        Task<int> AddAsync(string name, string address, string email, string phone, string userId);

        IEnumerable<T> GetAllSuppliers<T>();

        T GetById<T>(int id);
    }
}
