namespace Di2.Web.ViewModels.Suppliers.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;

    public class SupplierViewModel : IMapFrom<Supplier>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
