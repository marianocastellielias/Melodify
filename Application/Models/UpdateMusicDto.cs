using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UpdateMusicDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(0, 59)]
        public int Minute { get; set; }
        [Required]
        [Range(0, 59)]
        public int Second { get; set; }
    }
}
