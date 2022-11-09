using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>,IAccountRepository
    {


        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {


        }

        public IEnumerable<Account> GetAccountByOwner(Guid ownerId)
        {
            var accounts = FindByCondition(a => a.Ownerid.Equals(ownerId));
            return accounts;
        }

        public Account GetAccountByOwner(Guid ownerId, Guid Id)
        {
            return FindByCondition(a => a.Ownerid.Equals(ownerId) && a.Id.Equals(Id)).SingleOrDefault();
        }

        public IEnumerable<Account> GetAllAccount()
        {
            return FindAll()
                .OrderBy(ow => ow.AccountType)
                .ToList();
        }

        public Account GetAccountById(Guid AccountId)
        {
            return FindByCondition(account => account.Id.Equals(AccountId))
                .FirstOrDefault();
        }
    }
    
    
}
