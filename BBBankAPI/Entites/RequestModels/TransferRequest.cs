using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.RequestModels
{
    public class TransferRequest
    {
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
