using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.RequestModels
{
    public class DepositRequest
    {
        [Required(ErrorMessage = "AccountNumber is required.")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public decimal Amount { get; set; }
    }
}
