using Di2.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Data.Models
{
    public class Total : BaseModel<int>
    {
        public decimal UnitPrice { get; set; }

        public double Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
