using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Di2.Services.Data
{
    public interface ITotalsCustomerService
    {
        int GetStatus(string orderId);

        Task<int> ChangeOrderStatus(string orderId, int isCompleted);
    }
}
