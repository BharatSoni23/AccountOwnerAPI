using Entities.Models;
//using Entities.Models.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> GetAllAccount();

        Account GetAccountById(Guid AccountId);
        IEnumerable<Account> GetAccountByOwner(Guid ownerId);

        Account GetAccountByOwner(Guid ownerId,Guid Id);
        
    }
}
