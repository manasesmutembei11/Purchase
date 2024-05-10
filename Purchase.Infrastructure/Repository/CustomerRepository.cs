using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Purchase.Domain.Models;
using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using LinqKit;

namespace Purchase.Infrastructure.Repository
{
    public class CustomerRepository : RepositoryBase<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        public Task<PagedList<Customer>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var predicate = PredicateBuilder.New<Customer>(true);
            if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
            {
                predicate = predicate.And(s => s.FirstName.Contains(pagingParameters.Search) || s.LastName.Contains(pagingParameters.Search));
            }
            var data = FindByCondition(predicate, trackChanges).OrderBy(s => s.CreatedOn);
            return PagedList<Customer>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }


        public void DeleteTax(Customer customer) => Delete(customer);

    }

}
