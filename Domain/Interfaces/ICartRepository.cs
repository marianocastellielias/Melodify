using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Task<List<Cart>> GetAllCartPurchaseAsync(int userId);
        Task<Cart?> GetMyCartPendingAsync(int userId);
    }
}
