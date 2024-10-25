using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
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
            var albums = _albumRepository.GetAlbumsAcceptedAsync().Result;
            return AlbumDto.CreateList(albums);
        }
        public List<AlbumDto> GetMyAlbums(int userId)
        {
            var albums = _albumRepository.GetMyAlbumsAsync(userId).Result;
            return AlbumDto.CreateList(albums);
        }
        public void AddAlbumAsync(AddAlbumDto albumDto, int userId)
        {
            var user = _userRepository.GetByIdAsync(userId).Result;
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
                State = AlbumState.Pending,
                ReleaseDate = DateTime.Now,
                Price = albumDto.Price,
                User = user 
            };

            _albumRepository.AddAsync(album).Wait();
        }

        public void UpdateAlbumAsync(UpdateAlbumDto albumDto, int userId, int id)
        {
            var album = _albumRepository.GetByIdAndUserAsync(id).Result;

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

            
            _albumRepository.UpdateAsync(album).Wait();
        }

        public Album DeleteAlbumAsync(int id)
        {
            var album = _albumRepository.GetByIdAsync(id).Result;
            if (album == null)
            {
                throw new Exception("Album no encontrado");
            }
            _albumRepository.DeleteAsync(album).Wait();

            return album;
        }
    }
}

