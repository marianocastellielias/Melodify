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
        ICollection<UserDto> GetUsers();

        UpdateUserDto UsersUpdate(int id, UpdateUserDto updateUserDto);

        AddUserDto AddUser(AddUserDto addUserDto);

        UserRoleUpdateDTO UpdateRole(int id, UserRoleUpdateDTO userRoleUpdateDTO);


        User DeleteUser(int id);
        void UpdateAlbumState(int idAlbum, UpdateAlbumStateDto updateAlbumStateDto);
    }
}
