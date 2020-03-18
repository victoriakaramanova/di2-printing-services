﻿namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.Categories.InputModels;
    using Di2.Web.ViewModels.SubCategories;
    using Di2.Web.ViewModels.SubCategories.InputModels;
    using Di2.Web.ViewModels.SubCategories.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoriesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public SubCategoriesService(
            IDeletableEntityRepository<SubCategory> subCategoriesRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.subCategoriesRepository = subCategoriesRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetCategories<T>()
        {
            IQueryable<Category> query =
                this.categoriesRepository.All();
            return query.To<T>().ToList();
        }

        public async Task AddAsync(CreateSubCategoryInputModel input)
        {
            //var catName = input.Categories.Select(x => x.Name).FirstOrDefault();
            var catId = this.categoriesRepository
                .All()
                .Where(x => x.Name == input.CategoryName)
                .Select(x => x.Id)
                .FirstOrDefault();

            var subCategory = new SubCategory
            {
                Name = input.Name,
                CategoryId = catId,
                Description = input.Description,
            };

            await this.subCategoriesRepository.AddAsync(subCategory);
            await this.subCategoriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllSubCategories<T>()
        {
            return await this.subCategoriesRepository
            .AllAsNoTracking()
            .To<T>()
            .ToArrayAsync();
        }
    }
}