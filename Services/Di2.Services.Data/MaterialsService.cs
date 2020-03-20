namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Di2.Data.Common.Repositories;
    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.Materials.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class MaterialsService : IMaterialsService
    {
        private readonly IDeletableEntityRepository<Material> materialRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public MaterialsService(IDeletableEntityRepository<Material> materialRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.materialRepository = materialRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<int> AddAsync(string name, string description, string extraInfo, string subCategoryName, string imageUrl, string userId)
        {
            // using (var fileStream = new FileStream(@"D:\img.jpg", FileMode.Create))
            // {
            /*var expectedFileExt = new[] { ".pdf", ".doc", ".docx", ".jpg", ".png" };
            if (expectedFileExt.Any(x => input.Image.First().FileName.EndsWith(x)))
            {
            }*/
            // await input.Image.CopyToAsync(fileStream);
            // }
            var subCategoryId = this.subCategoryRepository
                .AllAsNoTracking()
                .Where(y => y.Name == subCategoryName)
                .Select(x => x.Id).FirstOrDefault();

            var categoryId = this.subCategoryRepository
                .AllAsNoTracking()
                .Where(y => y.Id == subCategoryId)
                .Select(x => x.CategoryId)
                .FirstOrDefault();

            var material = new Material
            {
                Name = name,
                Description = description,
                ExtraInfo = extraInfo,
                SubCategoryId = subCategoryId,
                Image = imageUrl,
                UserId = userId,
            };

            await this.materialRepository.AddAsync(material);
            await this.materialRepository.SaveChangesAsync();
            return material.Id;
        }

        public IEnumerable<T> GetAllMaterials<T>()
        {
            return this.materialRepository
            .All().OrderBy(x => x.SubCategory)
            .To<T>()
            .ToList();
        }
    }
}
