using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface ITotalsService
    {
        Task<int> ChangeOrderStatus(int orderId, bool isCompleted);
    }
}
