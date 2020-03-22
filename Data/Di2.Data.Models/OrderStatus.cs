using Di2.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Data.Models
{
    public class OrderStatus : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
