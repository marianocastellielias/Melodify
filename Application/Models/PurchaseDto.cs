using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CartState State { get; set; }
        public decimal Total { get; set; }
        public List<AlbumCartDto> AlbumsCart { get; set; } = new List<AlbumCartDto>();
        public static PurchaseDto Create(Cart? cart)
        {
            var dto = new PurchaseDto();
            if (cart != null)
            {
                dto.Id = cart.Id;
                dto.PurchaseDate = cart.PurchaseDate;
                dto.PaymentMethod = cart.PaymentMethod;
                dto.State = cart.State;
                dto.Total = cart.Total;

                foreach (var albumCart in cart.AlbumsCart)
                {
                    var albumCartDto = new AlbumCartDto();
                    albumCartDto.Id = albumCart.Id;
                    albumCartDto.Quantity = albumCart.Quantity;
                    albumCartDto.Album = AlbumDto.Create(albumCart.Album);
                    dto.AlbumsCart.Add(albumCartDto);
                }
            }

            return dto;
        }
    }
}
