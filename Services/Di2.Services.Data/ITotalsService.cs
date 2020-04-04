using System;
using System.Collections.Generic;
using System.Text;

namespace Di2.Services.Data
{
    public interface ITotalsService
    {
        decimal CalculateTotalPrice(double qty, decimal unitPrice);
    }
}
