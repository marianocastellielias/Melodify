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
    public class AlbumRepository : EfRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(ApplicationDbContext context) : base(context)
        {
        }

        

        public async Task<Album> GetByIdAndUserAsync(int id)
        {
            return await _context.Albums
                .Include((a)=>(a.User))
                .FirstOrDefaultAsync(a => a.Id == id);
            

        }

       

    }
}
