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

        public async Task<Album> AddAlbumAsync(Album album)
        {
            return await AddAsync(album);
        }
    }
}
