using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AlbumCartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public AlbumDto Album { get; set; }
    }
}
