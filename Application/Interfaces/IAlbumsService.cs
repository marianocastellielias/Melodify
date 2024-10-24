using Application.DTOs;
using Application.Models;
using Domain.Entities;
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

        Task UpdateAlbumAsync(UpdateAlbumDto albumDto, int userId, int id);

        Task<Album> DeleteAlbumAsync(int id);
    }
}
