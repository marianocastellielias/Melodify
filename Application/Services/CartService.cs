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
        /// <summary>
        /// Muestra todos los albums que tiene agregado al carrito
        /// </summary>
        /// <param name="idUser">Recibe el id del usuario logueado</param>
        /// <returns>Retorna el dto del carrito</returns>
        public CartDto GetCart(int idUser)
        {
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result;
            return CartDto.Create(cart);
        }
        /// <summary>
        /// Agrega un album al carrito. Si no hay un carrito como pendiente, agrega uno nuevo y si hay agrega al carrito en estado pendiente.
        /// </summary>
        /// <param name="idAlbum">Recibe el id del album que se desea agregar</param>
        /// <param name="idUser">Recibe el id del usuario logueado</param>
        /// <param name="quantity">Cantidad de albumes que se agregan del mismo tipo</param>
        /// <returns>Retorna el dto del carrito</returns>
        /// <exception cref="Exception"></exception>
        public CartDto AddAlbumCart(int idAlbum, int quantity, int idUser)
        {
            var album = _albumRepository.GetByIdAsync(idAlbum).Result
                ?? throw new Exception("El album ingresado no existe");
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
            if (IsInCart)
            {
                throw new Exception($"El album {albumCart.AlbumId} esta en el carrito");
            }
            _albumCartRepository.AddAsync(albumCart).Wait();
            cart.Total = cart.AlbumsCart.Sum(a => a.Album.Price * a.Quantity);
            cart.State = CartState.Pending;
            _cartRepository.UpdateAsync(cart).Wait();
            return CartDto.Create(cart);
        }
        /// <summary>
        /// Elimina el album del carrito. Esta vez solo busca por el id propio del AlbumCart que recibe desde el front
        /// </summary>
        /// <param name="idAlbumCart">Recibe el id del AlbumCart (no del album)</param>
        /// <param name="idUser"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveAlbumCart(int idAlbumCart, int idUser)
        {
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result
                ?? throw new Exception("El carrito no esta como pendiente o no existe");
            var IsInCart = cart.AlbumsCart.Any(c => c.Id == idAlbumCart);
            if (!IsInCart)
            {
                throw new Exception("El album no esta en en carrito ");
            }
            var albumCart = _albumCartRepository.GetByIdAsync(idAlbumCart).Result
                ?? throw new Exception("No existe ese album en un carrito");
            _albumCartRepository.DeleteAsync(albumCart).Wait();
            cart.Total = cart.AlbumsCart.Sum(a => a.Album.Price * a.Quantity);
            cart.State = CartState.Pending;
            _cartRepository.UpdateAsync(cart).Wait();
        }
        /// <summary>
        /// Realiza la compra con los items agregados (cambia el estado del carrito a Purchased)
        /// </summary>
        /// <param name="idUser">Recibe el id del usuario</param>
        /// <param name="paymentMethod">Recibe el método de pago</param>
        /// <exception cref="Exception"></exception>
        public void MakePurchase(int idUser, int paymentMethod)
        {
            var cart = _cartRepository.GetMyCartPendingAsync(idUser).Result
                ?? throw new Exception("El carrito no esta como pendiente o no existe");
            cart.State = CartState.Purchased;
            cart.PurchaseDate = DateTime.Now;
            cart.PaymentMethod = (PaymentMethod)paymentMethod;
            _cartRepository.UpdateAsync(cart).Wait();
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
