namespace Di2.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Di2.Data.Common.Models;

    public class DeliveryBatch : BaseDeletableModel<string>
    {
        [Key]
        public string MaterialId { get; set; }

        [Required]
        public Material Material { get; set; }

        [Key]
        public string SupplierId { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public double Quantity { get; set; }

    }
}
