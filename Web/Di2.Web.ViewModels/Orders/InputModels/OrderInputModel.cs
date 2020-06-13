using Di2.Data.Common.Repositories;
using Di2.Data.Models;
using Di2.Services.Mapping;
using Di2.Web.Infrastructure.Attributes;
using Di2.Web.ViewModels.Orders.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Di2.Web.ViewModels.Orders.InputModels
{
    public class OrderInputModel: IMapTo<OrderViewModel>,IMapFrom<Order>, IMapFrom<Delivery>
    {
        [Required]
        public int MaterialId { get; set; }

        // [Remote(action: "CheckDeliveredQty", controller: "Deliveries", AdditionalFields ="materialId", ErrorMessage ="Order less qty", HttpMethod ="post")]

        [OrderQuantity("AvailableQuantity")]
        [Range(1,int.MaxValue, ErrorMessage = "Въведете положително число!")]
        [Display(Name = "Количество")]
        public double Quantity { get; set; }

        public string MaterialName { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public string Image { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal AvgPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrdererId { get; set; }

        public int StatusId { get; set; }

        public string DeliveryAddress { get; set; }

        //public OrderStatus OrderStatus { get; set; }

        public List<IFormFile> PicturesFormFiles { get; set; }

        public double AvailableQuantity { get; set; }
    }
}
