using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Cover { get; set; }
        public AlbumState State { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
