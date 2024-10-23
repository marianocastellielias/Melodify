using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CartDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CartState State { get; set; }
        public decimal Total { get; set; }
        public List<AlbumDto> AlbumsCart { get; set; } = new List<AlbumDto>();
        public static CartDto Create(Cart? cart)
        {
            var dto = new CartDto();
            if (cart != null)
            {
                dto.Id = cart.Id;
                dto.PurchaseDate = cart.PurchaseDate;
                dto.PaymentMethod = cart.PaymentMethod;
                dto.State = cart.State;
                dto.Total = cart.Total;

                foreach (var album in cart.AlbumsCart)
                {
                    dto.AlbumsCart.Add(AlbumDto.Create(album.Album));
                }
            }

            return dto;
        }
    }
}
