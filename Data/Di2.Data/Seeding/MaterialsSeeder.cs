using Di2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Data.Seeding
{
    public class MaterialsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Suppliers.Any())
            {
                return;
            }

            var materials = new[]
            {
                new Material
                {
                    Name = "100x50 полугланц, силнолепнещ",
                    Description = "f76 x 1 x 3 000",
                    SubCategoryId = 1,
                },
                new Material
                {
                    Name = "80x300 m OUT AWX FH черна",
                    Description = "Wax Premium FH",
                    SubCategoryId = 2,
                },
            };

            await dbContext.AddRangeAsync(materials);
        }
    }
}
