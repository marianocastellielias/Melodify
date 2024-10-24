using Application.DTOs;
using Application.Interfaces;
using Application.Models;
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
                Price = albumDto.Price,
                User = user 
            };

            await _albumRepository.AddAsync(album);
  
        }

        public async Task<List<AlbumDto>> GetMyAlbums(int userId)
        {

            var albums = await _userRepository.GetMyAlbumsAsync(userId);

            return AlbumDto.CreateList(albums);
        }

        public async Task UpdateAlbumAsync(UpdateAlbumDto albumDto, int userId, int id)
        {
            var album = await _albumRepository.GetByIdAndUserAsync(id);

            if (album.User.Id != userId)
            {
                throw new UnauthorizedAccessException("No tienes permiso para modificar este álbum.");
            }

            if (album == null)
            {
                throw new Exception("User or Album not found");
            }


            album.Title = albumDto.Title;
            album.Artist = albumDto.Artist;
            album.Genre = albumDto.Genre;
            album.Cover = albumDto.Cover;
            album.Stock = albumDto.Stock;
            album.Price = albumDto.Price;

            
            await _albumRepository.UpdateAsync(album);
        }

        public async Task<Album> DeleteAlbumAsync(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            if (album == null)
            {
                throw new Exception("Album no encontrado");
            }
            await _albumRepository.DeleteAsync(album);

            return album;
        }
    }
}

