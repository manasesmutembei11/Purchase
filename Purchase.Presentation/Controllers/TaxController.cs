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
using Microsoft.AspNetCore.Authorization;
using Purchase.Domain.Validations;
using Purchase.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public TaxController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
          
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

       /* [NonAction]
        public IActionResult CreateTax([FromBody] TaxDTO tax)
        {
            if (tax is null)
                return BadRequest("OrderDTO object is null");
            var createdTax = _service.TaxService.CreateTax(tax);
            return Ok(createdTax);
        } */


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaxDTO), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entity = await _repository.Tax.GetByIdAsync(id);
                return Ok(_mapper.Map<TaxDTO>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       /* [HttpDelete("{id:guid}")]
        public IActionResult DeleteTax(Guid id)
        {
            _service.TaxService.DeleteTax(id, trackChanges: false);
            return NoContent();
        } */

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] TaxDTO dto)
        {
            var response = new BasicResponse();
            try
            {
                response.Message = "Tax";
                if (dto == null || !ModelState.IsValid)
                {
                    response.AddError(0, "Invalid model state");
                    return BadRequest(response);
                }
                var exist = await _repository.Tax.ExistAsync(dto.Id);
                var entity = _mapper.Map<Tax>(dto);
                if (!exist)
                {
                    _repository.Tax.Create(entity);
                }
                else
                {
                    _repository.Tax.Update(entity);
                }
                await _repository.SaveAsync();
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
    }
}
