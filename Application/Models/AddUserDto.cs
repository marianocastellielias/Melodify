using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddUserDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "Supera lantidad maxima de caracteres. ")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "La direccion excede la cantidad maxima de caracteres.")]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
  
}
