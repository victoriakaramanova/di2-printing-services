using Di2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Data.Seeding
{
    public class OrderStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Sent",
            });

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Received",
            });

            await dbContext.OrderStatuses.AddAsync(new OrderStatus
            {
                Name = "Canceled",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
