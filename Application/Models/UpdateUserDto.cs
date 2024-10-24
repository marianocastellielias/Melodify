using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

        public static  UpdateUserDto Update(User user)
        {
            UpdateUserDto updateUserDto = new UpdateUserDto();


            updateUserDto.Name = user.Name;
            updateUserDto.Email = user.Email;
            updateUserDto.Address = user.Address;
            updateUserDto.Phone = user.Phone;

            return updateUserDto;
        }

    }
}
