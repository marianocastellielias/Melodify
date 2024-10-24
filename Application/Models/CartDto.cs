using Application.Models;
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
        public decimal Total { get; set; }
        public List<AlbumCartDto> AlbumsCart { get; set; } = new List<AlbumCartDto>();
        public static CartDto Create(Cart? cart)
        {
            var dto = new CartDto();
            if (cart != null)
            {
                dto.Id = cart.Id;
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
