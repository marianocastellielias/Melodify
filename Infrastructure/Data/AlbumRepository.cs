using Domain.Entities;
using Domain.Enums;
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

        public async Task<List<Album>> GetAlbumsWithMusicAsync()
        {
            return await _context.Albums
                    .Include(m => m.Songs)
                    .ToListAsync();
        }

        public async Task<List<Album>> GetAlbumsAcceptedAsync()
        {
            return await _context.Albums
                    .Include(m => m.Songs)
                    .Where(album => album.State == AlbumState.Accepted)
                    .ToListAsync();
        }

        public async Task<Album?> GetByIdAndUserAsync(int id)
        {
            return await _context.Albums
                .Include((a) => (a.User))
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Album>> GetMyAlbumsAsync(int userId)
        {
            return await _context.Albums
                .Where(album => album.User.Id == userId)
                .ToListAsync();
        }
    }
}
