using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
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
        private readonly IUserRepository _userRepository;

        public AlbumService(IAlbumRepository albumRepository, IUserRepository userRepository)
        {
            _albumRepository = albumRepository;
            _userRepository = userRepository;
        }
        
        public ICollection<AlbumDto> GetAlbums()
        {
            var albums = _albumRepository.ListAsync().Result;
            return AlbumDto.CreateList(albums);
        }

        public async Task AddAlbumAsync(AddAlbumDto albumDto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var album = new Album
            {
                Title = albumDto.Title,
                Artist = albumDto.Artist,
                Genre = albumDto.Genre,
                Cover = albumDto.Cover,
                Stock = albumDto.Stock,
                ReleaseDate = DateTime.Now,
                User = user 
            };

            await _albumRepository.AddAsync(album);
        }
    }
}
}
