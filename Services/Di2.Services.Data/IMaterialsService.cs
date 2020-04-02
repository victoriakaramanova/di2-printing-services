namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Web.ViewModels.Materials.InputModels;
    using Di2.Web.ViewModels.Materials.ViewModels;

    public interface IMaterialsService
    {
        Task<int> AddAsync(string name, string description, string extraInfo, string subCategoryName, string imageUrl, string userId);

        IEnumerable<T> GetAllMaterials<T>();

        MaterialsViewModel GetById(int id);

        MaterialsViewModel GetByName(string name);
    }
}
