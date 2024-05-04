using Purchase.Domain.Contracts;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Services
{
    internal sealed class TaxService : ITaxService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public TaxService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public IEnumerable<Tax> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            try
            {
                var taxes =
                _repository.Tax.GetPagedListAsync(pagingParameters, trackChanges);
                return (IEnumerable<Tax>) taxes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedListAsync)} service method {ex}");
                throw;
            }
        }
    }
}
