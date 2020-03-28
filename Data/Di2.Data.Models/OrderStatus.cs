namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Di2.Data.Common.Models;

    public class OrderStatus : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
