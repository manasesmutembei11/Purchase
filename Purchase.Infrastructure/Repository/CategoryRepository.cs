using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LinqKit;

namespace Purchase.Infrastructure.Repository
{
    public class CategoryRepository : RepositoryBase<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Category>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var predicate = PredicateBuilder.New<Category>(true);
            if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
            {
                predicate = predicate.And(s => s.Name.Contains(pagingParameters.Search) || s.Code.Contains(pagingParameters.Search));
            }
            var data = FindByCondition(predicate, trackChanges).OrderBy(s => s.Name);
            return PagedList<Category>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }


        public void DeleteTax(Category category) => Delete(category);


    }
}
