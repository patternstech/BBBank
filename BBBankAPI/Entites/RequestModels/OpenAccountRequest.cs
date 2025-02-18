using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entites.RequestModels
{
    public class OpenAccountRequest
    {
        [MinLength(4, ErrorMessage = "The AccountNumber value cannot be less than 4 characters.")]
        [MaxLength(9, ErrorMessage = "The AccountNumber value cannot exceed 9 characters.")]
        [Required(ErrorMessage = "AccountNumber is required.")]
        public string AccountNumber { get; set; }
        [MinLength(4, ErrorMessage = "The AccountTitle value cannot be less than 4 characters.")]
        [MaxLength(20, ErrorMessage = "The AccountTitle value cannot exceed 20 characters.")]
        public string AccountTitle { get; set; }
        [Required(ErrorMessage = "CurrentBalance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "CurrentBalance cannot be negative.")]
        public decimal CurrentBalance { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required(ErrorMessage = "AccountStatus is required.")]
        public AccountStatus AccountStatus { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
