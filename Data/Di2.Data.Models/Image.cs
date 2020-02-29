using Di2.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Data.Models
{
    public class Image : BaseDeletableModel<int>
    {
        public string ImageUrl { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }
    }
}
