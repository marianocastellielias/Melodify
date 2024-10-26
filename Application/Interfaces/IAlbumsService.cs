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

        void AddAlbumAsync(AddAlbumDto albumDto, int userId);

        List<AlbumDto> GetMyAlbums(int userId);

        void UpdateAlbumAsync(UpdateAlbumDto albumDto, int userId, int id);

        Album DeleteAlbumAsync(int id, int userId);
    }
}
