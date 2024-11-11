using Application.DTOs;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        CartDto AddAlbumCart(int idAlbum, int quantity, int idUser);
        List<PurchaseDto> GetAllPurchases(int idUser);
        CartDto GetCart(int idUser);
        Task MakePurchase(int idUser, int paymentMethod);
        void RemoveAlbumCart(int id, int idUser);
    }
}
