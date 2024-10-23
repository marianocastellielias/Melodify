using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CartRepository : EfRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetMyCartAsync(int userId)
        {
			return await _context.Carts
				.Include(c => c.AlbumsCart)           
				.ThenInclude(ac => ac.Album)            
				.Where(c => c.UserId == userId)
				.SingleOrDefaultAsync();
		}
    }
}
