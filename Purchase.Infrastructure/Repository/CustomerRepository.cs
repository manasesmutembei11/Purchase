﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Purchase.Domain.Models;
using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;

namespace Purchase.Infrastructure.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }


        public Task<PagedList<Customer>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindAll(trackChanges).OrderBy(e => e.FirstName);
            return PagedList<Customer>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public void CreateCustomer(Customer customer) => Create(customer);

        public Customer GetCustomer(Guid Id, bool trackChanges) =>
        FindByCondition(c => c.CustomerId.Equals(Id), trackChanges)
        .SingleOrDefault();


        public void DeleteCustomer(Customer customer) => Delete(customer);

    }

}