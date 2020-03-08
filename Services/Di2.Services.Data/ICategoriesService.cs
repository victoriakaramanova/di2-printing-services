namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.Categories.InputModels;

    public interface ICategoriesService
    {
        Task AddAsync(CreateCategoryInputModel input);

        IEnumerable<T> GetAllCategories<T>();
    }
}
