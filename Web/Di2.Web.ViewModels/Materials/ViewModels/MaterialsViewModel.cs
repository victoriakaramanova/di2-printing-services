﻿namespace Di2.Web.ViewModels.Materials.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

  
    public class MaterialsViewModel : IMapFrom<Material>, IMapTo<Material>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Image { get; set; }

        public string UserUserName { get; set; }
    }
}
