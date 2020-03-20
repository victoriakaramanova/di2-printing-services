namespace Di2.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class IndexMaterialViewModel : IMapFrom<Material>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public string UserUserName { get; set; }
    }
}
