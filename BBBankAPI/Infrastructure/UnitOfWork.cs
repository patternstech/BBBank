using Entites;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Account> AccountRepository { get; }
        public IRepository<Transaction> TransactionRepository { get; }
        public IRepository<User> UserRepository { get; }
        protected readonly DbContext _context;
        public UnitOfWork(DbContext context, IRepository<Account> accountRepository,
                 IRepository<Transaction> transactionRepositoy,
                 IRepository<User> userRepositoy)
        {
            this.AccountRepository = accountRepository;
            this.TransactionRepository = transactionRepositoy;
            this.UserRepository = userRepositoy;
            this._context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
