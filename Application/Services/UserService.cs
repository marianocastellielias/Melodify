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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UpdateUserDto UserUpdate(int id, UpdateUserDto updateUser)
        {
            var user = _userRepository.GetByIdAsync(id).Result;
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Address = updateUser.Address;
            user.Phone = updateUser.Phone;

            _userRepository.UpdateAsync(user).Wait();

            return updateUser;
        }

    }
}
