using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using Purchase.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.IService;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public TaxController(IRepositoryManager repository, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Tax.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<TaxDTO>(
                paged.Data.Select(s => _mapper.Map<TaxDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateTax([FromBody] TaxDTO tax)
        {
            if (tax is null)
                return BadRequest("OrderDTO object is null");
            var createdTax = _service.TaxService.CreateTax(tax);
            return Ok(createdTax);
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetTax(Guid id)
        {
            var tax = _service.TaxService.GetTax(id, trackChanges: false);
            return Ok(tax);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTax(Guid id)
        {
            _service.TaxService.DeleteTax(id, trackChanges: false);
            return NoContent();
        }
    }
}
