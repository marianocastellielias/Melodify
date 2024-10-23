using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CartState State { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public List<AlbumCart> AlbumsCart { get; set; } = [];
        public void AddAlbum(AlbumCart albumCart) => AlbumsCart.Add(albumCart);
    }
}
