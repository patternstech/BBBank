﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Transaction : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public Account Account { get; set; }
    }
    public enum TransactionType
    {
        Deposit = 0,
        Withdraw = 1
    }
}
