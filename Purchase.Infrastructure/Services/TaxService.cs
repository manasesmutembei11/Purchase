using Purchase.Domain.Contracts;
using Purchase.Domain.DTOs;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Purchase.Infrastructure.Services
{
    internal sealed class TaxService : ITaxService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public TaxService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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

        public TaxDTO CreateTax(TaxDTO tax)
        {
            var taxEntity = _mapper.Map<Tax>(tax);
            _repository.Tax.CreateTax(taxEntity);
            _repository.Save();
            var taxToReturn = _mapper.Map<TaxDTO>(taxEntity);
            return taxToReturn;
        }

        public TaxDTO GetTax(Guid id, bool trackChanges)
        {
            var tax = _repository.Tax.GetTax(id, trackChanges);

            var taxDto = _mapper.Map<TaxDTO>(tax);
            return taxDto;
        }


        public void DeleteTax(Guid Id, bool trackChanges)
        {
            var tax = _repository.Tax.GetTax(Id, trackChanges);
            if (tax is null)
                throw null;

            _repository.Tax.DeleteTax(tax);
            _repository.Save();
        }
    }
}
