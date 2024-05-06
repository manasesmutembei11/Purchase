using Purchase.Domain.Contracts;
using Purchase.Domain.DTOs;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Services
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ProductService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<Product> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            try
            {
                var products =
                _repository.Product.GetPagedListAsync(pagingParameters, trackChanges);
                return (IEnumerable<Product>) products;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedListAsync)} service method {ex}");
                throw;
            }
        }

        public ProductDTO CreateProduct(ProductDTO product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _repository.Product.CreateProduct(productEntity);
            _repository.Save();
            var productToReturn = _mapper.Map<ProductDTO>(productEntity);
            return productToReturn;
        }
    }
}
