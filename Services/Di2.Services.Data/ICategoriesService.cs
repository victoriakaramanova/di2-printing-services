namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Web.ViewModels.Categories.InputModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        Task AddAsync(CreateCategoryInputModel input, string userId);

        IEnumerable<T> GetAllCategories<T>();
    }
}
