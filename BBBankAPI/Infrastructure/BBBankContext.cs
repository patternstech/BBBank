﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites;

namespace Infrastructure
{
    public class BBBankContext
    {
        public BBBankContext()
        {
            // creating the collection for user list
            this.Users = new List<User>();

            // initializing a new user 
            this.Users.Add(new User
            {
                Id = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3",
                FirstName = "Raas",
                LastName = "Masood",
                Email = "rassmasood@hotmail.com",
                ProfilePicUrl = "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg"
            });

            // creating the collection for account list
            this.Accounts = new List<Account>();

            // initializing a new account 
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                AccountNumber = "0001-1001",
                AccountTitle = "Raas Masood",
                CurrentBalance = 3500M,
                AccountStatus = AccountStatus.Active,
                User = this.Users[0]
            });

            // creating the collection for transaction list
            this.Transactions = new List<Transaction>();

            // initializing with some transactions 
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 1000M,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.Deposit,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -100M,
                TransactionDate = DateTime.Now.AddMonths(-1),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -45M,
                TransactionDate = DateTime.Now.AddMonths(-2),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 500M,
                TransactionDate = DateTime.Now.AddMonths(-3),
                TransactionType = TransactionType.Deposit,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {


                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -200M,
                TransactionDate = DateTime.Now.AddMonths(-4),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 500M,
                TransactionDate = DateTime.Now.AddMonths(-5),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 200M,
                TransactionDate = DateTime.Now.AddMonths(-6),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -300M,
                TransactionDate = DateTime.Now.AddMonths(-7),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -100M,
                TransactionDate = DateTime.Now.AddMonths(-8),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 200M,
                TransactionDate = DateTime.Now.AddMonths(-9),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -500M,
                TransactionDate = DateTime.Now.AddMonths(-10),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 900M,
                TransactionDate = DateTime.Now.AddMonths(-11),
                TransactionType = TransactionType.Deposit

            });

        }

        public List<Transaction> Transactions { get; set; }
        public List<Account> Accounts { get; set; }
        public List<User> Users { get; set; }
    }
}
