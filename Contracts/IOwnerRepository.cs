using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public  interface IOwnerRepository : IRepositoryBase<Owner>
    {
        PagedList<ExpandoObject> GetOwners(OwnerParameters ownerParameters);
        ExpandoObject GetOwnerById(Guid Ownerid, string fields);

        Owner GetOwnerById(Guid Ownerid);

        Owner GetOwnerWithDetails(Guid Ownerid);

        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);

    }
}
