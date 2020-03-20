using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexMaterialViewModel> Materials { get; set; }
    }
}
