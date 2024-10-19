using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AlbumService : IAlbumsService
    {
        private readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        
        public ICollection<AlbumDto> GetAlbums()
        {
            var albums = _albumRepository.ListAsync().Result;
            return AlbumDto.CreateList(albums);
        }
    }
}
