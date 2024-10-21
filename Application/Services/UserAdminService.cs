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
    }
}
