using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Contracts;
using AutoMapper;
using Purchase.Domain.Paging;
using Purchase.Presentation.DTOs;
using Purchase.Domain.Models;
namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CustomerController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Customer.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<CustomerDTO>(
                paged.Data.Select(s => _mapper.Map<CustomerDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid CustomerId)
        {
            try
            {
                var entity = await _repository.Customer.(CustomerId);
                return Ok(_mapper.Map<TaxDTO>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] CustomerDTO dto)
        {
            var response = new BasicResponse();
            try
            {
                response.Message = "Customer";
                if (dto == null || !ModelState.IsValid)
                {
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var exist = await _repository.Customer.ExistAsync(dto.CustomerId);
                var entity = _mapper.Map<Customer>(dto);
                if (!exist)
                {
                    _repository.Customer.Create(entity);
                }
                else
                {
                    _repository.Customer.Update(entity);
                }
                await _repository.Customer.SaveAsync();
                response.Message = "OK";
                return StatusCode(201, response);

            }
            catch (DomainValidationException ex)
            {
                ex.ValidationResult.Results.ForEach(s => response.AddError(0, s.ErrorMessage));
                response.Message = "Domain Errors";
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                response.AddError(0, ex.Message);
                response.Message = "OK";
                return StatusCode(500, response);
            }



        }

        [HttpGet("lookuplist")]
        public async Task<IActionResult> GetLookupList()
        {
            //var user = _userProvider.User;
            var predicate = PredicateBuilder.New<Tax>(true);



            var data = await _repository.Tax
                .FindByCondition(predicate, false)
                .OrderBy(s => s.Code).ToListAsync();
            var items = data.Select(_mapper.Map<TaxDTO>).ToList(); ;

            items.ForEach(item => item.Rate = item.Rate / 100);
            return Ok(items);
        }
    }


        
}
