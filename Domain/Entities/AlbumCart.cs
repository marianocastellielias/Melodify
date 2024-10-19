using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AlbumCart
    {
        public int Id { get; set; }
        public Cart Cart { get; set; }
        public Album Album { get; set; }
    }
}
