using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entites.ResponseModels
{
    public class AccountInfoByUserResponse : AccountInfoResponse
    {

    }
    public class AccountInfoByAccountNumberResponse : AccountInfoResponse
    {

    }
    public class AccountInfoResponse
    {
        public string AccountNumber { get; set; }
        public string AccountTitle { get; set; }
        public decimal CurrentBalance { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus AccountStatus { get; set; }
        public string UserImageUrl { get; set; }
    }
}
