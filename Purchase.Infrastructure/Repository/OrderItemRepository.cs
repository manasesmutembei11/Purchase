using Purchase.Domain.Models;
using Purchase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Purchase.Domain.Paging;
using LinqKit;

namespace Purchase.Infrastructure.Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<OrderItem>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var predicate = PredicateBuilder.New<OrderItem>(true);
            if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
            {
                predicate = predicate.And(s => s.Product.Name.Contains(pagingParameters.Search) || s.Order.Customer.FirstName.Contains(pagingParameters.Search));
            }
            var data = FindByCondition(predicate, trackChanges).OrderBy(s => s.CreatedOn);
            return PagedList<OrderItem>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }


        public void DeleteTax(OrderItem orderItem) => Delete(orderItem);

    }

}
