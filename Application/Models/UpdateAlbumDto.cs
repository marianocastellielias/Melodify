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

        public string Cover { get; set; }

        public int Stock { get; set; }  

        public decimal Price { get; set; }
    }
}

