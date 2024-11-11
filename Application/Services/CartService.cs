using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
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
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result;
            return CartDto.Create(cart);
        }
       
        public CartDto AddAlbumCart(int idAlbum, int quantity, int idUser)
        {
            var album = _albumRepository.GetByIdAsync(idAlbum, a => a.Songs).Result
                ?? throw new NullReferenceException("El album ingresado no existe");
            if (album.State == AlbumState.Rejected || album.State == AlbumState.Pending )
            {
                throw new Exception("Album no disponible.");
            }
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result;
            if (cart == null)
            {
                var newCart = new Cart();
                newCart.UserId = idUser;
                newCart.State = CartState.Pending;
                _cartRepository.AddAsync(newCart).Wait();
                cart = _cartRepository.GetMyCartPendingAsync(idUser).Result;
            }
            var albumCart = new AlbumCart()
            {
                AlbumId = album.Id,
                CartId = cart.Id,
                Quantity = quantity,
                Album = album
            };

            var IsInCart = cart.AlbumsCart.Any(c => c.AlbumId == albumCart.AlbumId);
            if (IsInCart) throw new Exception($"El album {albumCart.AlbumId} esta en el carrito");
            var isGreaterThanStock = albumCart.Quantity > albumCart.Album.Stock;
            if (isGreaterThanStock) throw new Exception($"El album {albumCart.AlbumId} tiene un stock de {albumCart.Album.Stock}");

            _albumCartRepository.AddAsync(albumCart).Wait();
            cart.Total = cart.AlbumsCart.Sum(a => a.Album.Price * a.Quantity);
            cart.State = CartState.Pending;
            _cartRepository.UpdateAsync(cart).Wait();
            return CartDto.Create(cart);
        }
        
        public void RemoveAlbumCart(int idAlbumCart, int idUser)
        {
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result
                ?? throw new NullReferenceException("El carrito no esta como pendiente o no existe");
            var IsInCart = cart.AlbumsCart.Any(c => c.Id == idAlbumCart);
            if (!IsInCart)
            {
                throw new Exception("El album no esta en en carrito ");
            }
            var albumCart = _albumCartRepository.GetByIdAsync(idAlbumCart).Result
                ?? throw new NullReferenceException("No existe ese album en un carrito");
            _albumCartRepository.DeleteAsync(albumCart).Wait();
            cart.Total = cart.AlbumsCart.Sum(a => a.Album.Price * a.Quantity);
            cart.State = CartState.Pending;
            _cartRepository.UpdateAsync(cart).Wait();
        }

        public async Task MakePurchase(int idUser, int paymentMethod)
        {
            var cart = await _cartRepository.GetMyCartPendingAsync(idUser)
                ?? throw new NullReferenceException("El carrito no esta como pendiente o no existe");

            cart.State = CartState.Purchased;
            cart.PurchaseDate = DateTime.Now;
            cart.PaymentMethod = (PaymentMethod)paymentMethod;
            await _cartRepository.UpdateAsync(cart);
        }
        
        public List<PurchaseDto> GetAllPurchases(int idUser)
        {
            var purchases = _cartRepository.GetAllCartPurchaseAsync(idUser).Result;
            if (purchases != null && purchases.Count != 0)
            {
                return purchases.Select(PurchaseDto.Create).ToList();
            }
            return [];
        }
    }
}
