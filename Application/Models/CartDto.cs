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
    }
}
