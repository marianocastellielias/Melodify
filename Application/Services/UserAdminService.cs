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
    public class UserAdminService : IUserAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAlbumRepository _albumRepository;

        public UserAdminService(IUserRepository userRepository, IAlbumRepository albumRepository)
        {
            _userRepository = userRepository;
            _albumRepository = albumRepository;
        }


        public ICollection<UserDto> GetUsers()
        {
            // Usa await para esperar el resultado
            var users = _userRepository.ListAsync().Result;
            return UserDto.CreateList(users);
        }

        public UpdateUserDto UsersUpdate(int id, UpdateUserDto updateUserDto)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Address = updateUserDto.Address;
            user.Phone = updateUserDto.Phone;

            _userRepository.UpdateAsync(user).Wait();

            return updateUserDto;

        }

        public AddUserDto AddUser(AddUserDto addUserDto)
        {
            var user = new User
            {
                Name = addUserDto.Name,
                Role = "Client",//asigna el rol por defecto
                Email = addUserDto.Email,
                Address = addUserDto.Address,
                Phone = addUserDto.Phone,
                Password = addUserDto.Password,
            };

            _userRepository.AddAsync(user).Wait();
            return addUserDto;

        }

        public UserRoleUpdateDTO UpdateRole(int id, UserRoleUpdateDTO userRoleUpdateDTO)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Role = userRoleUpdateDTO.Role;//Aca el Adm cambia el rol del usuario.
            

            _userRepository.UpdateAsync(user);
            return userRoleUpdateDTO;
        }

        public void UpdateAlbumState(int idAlbum, UpdateAlbumStateDto updateAlbumStateDto)
        {
            var album = _albumRepository.GetByIdAsync(idAlbum).Result;
            album.State = updateAlbumStateDto.State ? AlbumState.Accepted : AlbumState.Rejected;
            _albumRepository.UpdateAsync(album).Wait();
        }

        public User DeleteUser(int id)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            if (user == null)
            {
                throw new Exception("User not found");
            }
            _userRepository.DeleteAsync(user).Wait();

            return user;
        }
    }
}
