﻿namespace Di2.Web.ViewModels.OrderSuppliers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Models;
    using Di2.Services.Mapping;
    using Di2.Web.ViewModels.PriceLists.ViewModels;

    public class OrderSuppliersListViewModel : IMapFrom<OrderSupplier>, IMapFrom<PriceList>, IMapTo<OrderSupplier>,IMapTo<PriceList>
    {
        // public IEnumerable<PriceList> Pricelist { get; set; }

        public IEnumerable<OrderSupplierViewModel> OrderSuppliers { get; set; }
    }
}
