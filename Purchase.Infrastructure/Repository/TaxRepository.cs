using LinqKit;
using Purchase.Domain.Contracts;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure.Repository
{
    public class TaxRepository : RepositoryBase<Tax, Guid>, ITaxRepository
    {
        public TaxRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Tax>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var predicate = PredicateBuilder.New<Tax>(true);
            if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
            {
                predicate = predicate.And(s => s.Name.Contains(pagingParameters.Search) || s.Code.Contains(pagingParameters.Search));
            }
            var data = FindByCondition(predicate, trackChanges).OrderBy(s => s.Name);
            return PagedList<Tax>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }


        public void DeleteTax(Tax tax) => Delete(tax);

    }
}
