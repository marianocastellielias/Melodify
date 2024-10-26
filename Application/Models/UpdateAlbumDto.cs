using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UpdateAlbumDto
    {

        public string Title { get; set; }  

        public string Artist { get; set; }

        public string Genre { get; set; }
        [Url]
        public string Cover { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
    }
}

