﻿namespace Di2.Web.ViewModels.Deliveries
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class DeliveryViewModel : IMapFrom<Material>, IMapFrom<Delivery>,IMapTo<Delivery>
    {
        public int DeliveryId { get; set; }

        public int MaterialId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string Image { get; set; }

        public double Quantity { get; set; }

        public decimal AvgPrice { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
