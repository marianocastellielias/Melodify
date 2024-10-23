using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class CartService : ICartService
	{
		private readonly ICartRepository _cartRepository;
		private readonly IAlbumRepository _albumRepository;
		private readonly IAlbumCartRepository _albumCartRepository;
		public CartService(ICartRepository cartRepository, IAlbumRepository albumRepository, IAlbumCartRepository albumCartRepository)
		{
			_cartRepository = cartRepository;
			_albumRepository = albumRepository;
			_albumCartRepository = albumCartRepository;
		}

		public CartDto GetCart(int idUser)
		{
			var cart = _cartRepository.GetMyCartAsync(idUser).Result;
			return CartDto.Create(cart);
		}

		public CartDto AddAlbumCart(int idAlbum, int idUser)
		{
			var album = _albumRepository.GetByIdAsync(idAlbum).Result
				?? throw new Exception("El album ingresado no existe");
			var cart = _cartRepository.GetMyCartAsync(idUser).Result;
			if (cart == null)
			{
				var newCart = new Cart();
				newCart.UserId = idUser;
				_cartRepository.AddAsync(newCart).Wait();
				cart = _cartRepository.GetMyCartAsync(idUser).Result;
			}
			var albumCart = new AlbumCart()
			{
				AlbumId = album.Id,
				CartId = cart.Id,
				Album = album
			};
			_albumCartRepository.AddAsync(albumCart).Wait();
			return CartDto.Create(cart);
		}
	}
}
