namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Models;
    using Di2.Web.ViewModels.Materials.InputModels;

    public interface IMaterialsService
    {
        Task AddAsync(CreateMaterialInputModel input);

        IEnumerable<T> GetAllMaterials<T>();
    }
}
