namespace Di2.Web.ViewModels.Materials.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateMaterialInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public string SubCategory { get; set; }
    }
}
