using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAlbumsService
    {
        ICollection<AlbumDto> GetAlbums();

        Task AddAlbumAsync(AddAlbumDto albumDto, int userId);

        Task<List<AlbumDto>> GetMyAlbums(int userId);
    }
}
