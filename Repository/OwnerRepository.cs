using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        private ISortHelper<Owner> _sortHelper;
        private IDataShaper<Owner> _dataShaper;

        public OwnerRepository(RepositoryContext repositoryContext,ISortHelper<Owner> sortHelper,IDataShaper<Owner> dataShaper)
            :base(repositoryContext)
            { 
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        
        }

        public PagedList<ExpandoObject> GetOwners(OwnerParameters ownerParameters)
        {
            var owners = FindByCondition(o => o.DOB.Year >= ownerParameters.MinYearOfBirth && o.DOB.Year <= ownerParameters.MaxYearOfBirth);
                

            SearchByName(ref owners, ownerParameters.Name);

            //_sortHelper.ApplySort(owners, ownerParameters.OrderBy);

            var shapedOwners = _dataShaper.ShapeData(owners, ownerParameters.Fields);
            return PagedList<ExpandoObject>.ToPagedList(shapedOwners, ownerParameters.PageNumber, ownerParameters.PageSize);



           /* var sortedOWners = _sortHelper.ApplySort(owners, ownerParameters.OrderBy);

            return PagedList<Owner>.ToPagedList(sortedOWners,ownerParameters.PageNumber,ownerParameters.PageSize);*/



            //ApplySort(ref owners, ownerParameters.OrderBy);


            //return PagedList<Owner>.ToPagedList(FindAll().OrderBy(on => on.Name),
            //ownerParameters.PageNumber, ownerParameters.PageSize);

            //return PagedList<Owner>.ToPagedList(owners,
            //    ownerParameters.PageNumber,
            //        ownerParameters.PageSize);





            //return FindAll()
            //    .OrderBy(ow => ow.Name)
            //    .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
            //    .Take(ownerParameters.PageSize)
            //    .ToList();
        }

        //private void SearchByName(ref IOrderedQueryable<Owner> owners, string ownerName)
        //{
        //    if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
        //        return;

        //    owners = (IQueryable<Owner>)owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        //}

        private void SearchByName(ref IQueryable<Owner> owners, string ownerName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
                return;

            owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        }


        public Owner GetOwnerById(Guid Ownerid)
        {
            return FindByCondition(owner => owner.id.Equals(Ownerid))
            .FirstOrDefault();
        }

        public Owner GetOwnerWithDetails(Guid Ownerid)
        {
            return FindByCondition(owner => owner.id.Equals(Ownerid))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }
        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }


        //Add a new private method Applysort for sorting purpose
        private void ApplySort(ref IQueryable<Owner> owners, string orderByQueryString)
        {
            if (!owners.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos=typeof(Owner).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach(var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}");
           }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                owners = owners.OrderBy(x => x.Name);
                return;
            }
            owners = owners.OrderBy(orderQuery);
        }

        //public SerializableExpando GetOwnerById(Guid Ownerid, string fields)
        //{
        //    var owner = FindByCondition(owner => owner.id.Equals(Ownerid))
        //        .DefaultIfEmpty(new Owner())
        //        .FirstOrDefault();

        //    return _dataShaper.ShapeData(owner, fields);
        //}



        public ExpandoObject GetOwnerById(Guid Ownerid, string fields)
        {
            var owner = FindByCondition(owner => owner.id.Equals(Ownerid))
               .DefaultIfEmpty(new Owner())
               .FirstOrDefault();

            return _dataShaper.ShapeData(owner, fields);
        }
    }
}
