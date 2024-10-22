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
    public class UserAlbumService : IUserAlbumService
    {
        private readonly IUserRepository _userRepository;

        public UserAlbumService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<AlbumDto>> GetMyAlbums(int userId)
        {

            var albums = await _userRepository.GetMyAlbumsAsync(userId);

            return AlbumDto.CreateList(albums);
        }

    }
}
