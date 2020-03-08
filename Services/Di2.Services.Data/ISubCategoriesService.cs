namespace Di2.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Di2.Data.Models;
    using Di2.Web.ViewModels.SubCategories.InputModels;
    using Di2.Web.ViewModels.SubCategories.ViewModels;

    public interface ISubCategoriesService
    {
        Task AddAsync(CreateSubCategoryInputModel input);

        Task<IEnumerable<T>> GetAllSubCategories<T>();

        IEnumerable<T> GetCategories<T>();
    }
}
