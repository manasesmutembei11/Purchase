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
    public class TaxRepository : RepositoryBase<Tax>, ITaxRepository
    {
        public TaxRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Tax>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindAll(trackChanges).OrderBy(e => e.Code);
            return PagedList<Tax>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public void CreateTax(Tax tax) => Create(tax);
        public Tax GetTax(Guid id, bool trackChanges) =>
        FindByCondition(c => c.TaxId.Equals(id), trackChanges)
        .SingleOrDefault();


        public void DeleteTax(Tax tax) => Delete(tax);

    }
}
