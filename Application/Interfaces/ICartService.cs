using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        CartDto AddAlbumCart(int idAlbum, int idUser);
        CartDto GetCart(int idUser);
    }
}
