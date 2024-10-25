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
    public class MusicRepository : EfRepository<Music>, IMusicRepository
    {
        public MusicRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<Music>> GetAllMusicWithAlbum()
        {
            return await _context.Musics.Include(m => m.Album).ToListAsync();  
        }

        public async Task<Music?> GetMusic(int id)
        {
            return await _context.Musics
                .Include(m => m.Album)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
