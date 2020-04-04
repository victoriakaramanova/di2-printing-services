using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Services.Data
{
    public class TotalsService : ITotalsService
    {
        public decimal CalculateTotalPrice(double qty, decimal unitPrice)
        {
            var totalPrice = unitPrice * (decimal)qty;
            return totalPrice;
        }
    }
}
