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
    public interface IUserAdminService
    {
        Task<ICollection<UserDto>> GetUsers();

        Task<UpdateUserDto> UsersUpdate(int id, UpdateUserDto updateUserDto);

        Task<AddUserDto> AddUser(AddUserDto addUserDto);

        Task<UserRoleUpdateDTO> UpdateRole(int id, UserRoleUpdateDTO userRoleUpdateDTO);

        Task<UpdateUserDto> UserUpdate(int id, UpdateUserDto updateUser);

        Task<User> DeleteUser(int id);
    }
}
