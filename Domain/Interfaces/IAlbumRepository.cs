using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAlbumRepository : IRepositoryBase<Album>
    {
        Task<List<Album>> GetAlbumsAcceptedAsync();
        Task<List<Album>> GetAlbumsWithMusicAsync();
        Task<Album?> GetByIdAndUserAsync(int id);

        Task<List<Album>> GetMyAlbumsAsync(int userId);
    }
}
