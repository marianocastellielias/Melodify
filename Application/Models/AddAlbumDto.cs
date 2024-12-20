﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddAlbumDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [Url]
        public string Cover { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
