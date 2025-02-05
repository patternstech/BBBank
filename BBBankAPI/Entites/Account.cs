using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entites
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string AccountTitle { get; set; }
        public decimal CurrentBalance { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus AccountStatus { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
    public enum AccountStatus
    {
        Active = 0,
        InActive = 1
    }
}
