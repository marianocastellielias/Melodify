using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public static UserDto Create(User user)
        {
            UserDto userDto = new UserDto();

            userDto.Id = user.Id;
            userDto.Name = user.Name;
            userDto.Role = user.Role; 
            userDto.Email = user.Email;
            userDto.Address = user.Address;
            userDto.Phone = user.Phone;

            return userDto;
        }

        
        public static List<UserDto> CreateList(IEnumerable<User> users)
        {
            var listDto = new List<UserDto>();
            foreach (var user in users)
            {
                listDto.Add(Create(user)); 
            }
            return listDto;
        }

    }
}
