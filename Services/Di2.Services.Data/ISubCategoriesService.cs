namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.SubCategories.InputModels;

    public interface ISubCategoriesService
    {
        Task AddAsync(CreateSubCategoryInputModel input);

        Task<IEnumerable<T>> GetAllSubCategories<T>();
    }
}
