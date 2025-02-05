using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Account> AccountRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<User> UserRepository { get; }
        Task<int> CommitAsync();
    }
}
