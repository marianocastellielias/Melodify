using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AddPurchaseDto
    {
        [Required]
        [Range(0, 2)]
        public int PaymentMethod { get; set; }
    }
}
