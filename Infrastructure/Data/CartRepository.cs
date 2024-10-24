using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Infrastructure.Data
{
    public class CartRepository : EfRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetMyCartPendingAsync(int userId)
        {
			return await _context.Carts
				.Include(c => c.AlbumsCart)           
				.ThenInclude(ac => ac.Album)            
				.Where(c => c.UserId == userId && c.State == CartState.Pending)
				.SingleOrDefaultAsync();
		}
        public async Task<List<Cart>> GetAllCartPurchaseAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.AlbumsCart)
                .ThenInclude(ac => ac.Album)
                .Where(c => c.UserId == userId && c.State == CartState.Purchased)
                .ToListAsync();
        }
    }
}
