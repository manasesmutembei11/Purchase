using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            var data = FindAll(trackChanges).OrderBy(e => e.Code);
            return PagedList<Category>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public void CreateCategory(Category category) => Create(category);

        public Category GetCategory(Guid categoryId, bool trackChanges) =>
        FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
        .SingleOrDefault();


        public void DeleteCategory(Category category) => Delete(category);


    }
}
