namespace Di2.Services.Data
{
    using System;
    using System.Collections.Generic;
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

        public MaterialsService(IDeletableEntityRepository<Material> materialRepository)
        {
            this.materialRepository = materialRepository;
        }

        public async Task AddAsync(CreateMaterialInputModel input)
        {
            var subCat = (SubCategory)System.Convert.ChangeType(input.SubCategory, typeof(SubCategory));

            var material = new Material
            {
                Name = input.Name,
                Description = input.Description,
                ExtraInfo = input.ExtraInfo,
                SubCategory = subCat,
            };

            await this.materialRepository.AddAsync(material);
            await this.materialRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllMaterials<T>()
        {
            return await this.materialRepository
            .AllAsNoTracking()
            .To<T>()
            .ToArrayAsync();
        }
    }
}
