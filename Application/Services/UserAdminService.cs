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
    public class UserAdminService : IUserAdminService
    {
        private readonly IUserRepository _userRepository;

        public UserAdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<ICollection<UserDto>> GetUsers()
        {
            // Usa await para esperar el resultado
            var users = await _userRepository.ListAsync();
            return UserDto.CreateList(users);
        }

        public async Task<UpdateUserDto> UsersUpdate(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Address = updateUserDto.Address;
            user.Phone = updateUserDto.Phone;

            await _userRepository.UpdateAsync(user);

            return updateUserDto;

        }

        public async Task<AddUserDto> AddUser(AddUserDto addUserDto)
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

            await _userRepository.AddAsync(user);
            return addUserDto;

        }

        public async Task<UserRoleUpdateDTO> UpdateRole(int id, UserRoleUpdateDTO userRoleUpdateDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Role = userRoleUpdateDTO.Role;
            

            await _userRepository.UpdateAsync(user);
            return userRoleUpdateDTO;
        }

        public async Task<UpdateUserDto> UserUpdate(int id,UpdateUserDto updateUser)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Address = updateUser.Address;
            user.Phone = updateUser.Phone;

            await _userRepository.UpdateAsync(user);

            return updateUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            await _userRepository.DeleteAsync(user);

            return user;
        }
    }
}
