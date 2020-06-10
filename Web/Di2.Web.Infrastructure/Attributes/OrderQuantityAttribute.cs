using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace Di2.Web.Infrastructure.Attributes
{
    public class OrderQuantityAttribute : ValidationAttribute
    {
        private const string ValidationOrderQuantityErrorMessage = "Въведете по-малко количество или се свържете с НАС!";

        private readonly string availableQuantity;

        public OrderQuantityAttribute(string availableQuantity)
        {
            this.availableQuantity = availableQuantity;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var quantity = value.ToString();
            var property = validationContext.ObjectType.GetProperty(availableQuantity);
           
            var avaiableQtyAsString = (property.GetValue(validationContext.ObjectInstance)).ToString();

            if (double.Parse(quantity) > double.Parse(avaiableQtyAsString))
            {
                return new ValidationResult(ValidationOrderQuantityErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
