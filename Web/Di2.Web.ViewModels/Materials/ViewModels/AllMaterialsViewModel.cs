namespace Di2.Web.ViewModels.Materials.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class AllMaterialsViewModel : IMapFrom<Material>
    {
        public string Name { get; set; }

        public string Color { get; set; }
    }
}
