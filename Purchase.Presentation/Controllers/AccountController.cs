using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Contracts;
using Purchase.Domain.DTOs.UserDTOs;
using Purchase.Domain.IEmail;
using Purchase.Domain.Managers;
using Purchase.Domain.Models;
using Purchase.Domain.Models.Templates;
using Purchase.Domain.Models.UserEntities;
using Purchase.Domain.Paging;
using Purchase.Domain.Security;
using Purchase.Domain.Utilities;
using Purchase.Domain.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public AccountController(
            IRepositoryManager repository,
            IMapper mapper,
            IUserProvider userProvider
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userProvider = userProvider;
        }
        [HttpGet("pagedlist/{accountType}")]
        public async Task<IActionResult> GetPagedList(AccountType accountType, [FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Account.GetPagedListAsync(s => s.AccountType == accountType, pagingParameters, true);
            var data = new PagedList<AccountRefDTO>(
                paged.Data.Select(s => _mapper.Map<AccountRefDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entity = await _repository.Account.GetByIdAsync(id);
                return Ok(_mapper.Map<AccountRefDTO>(entity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("accountLookuplist")]
        public async Task<IActionResult> GetLookupList()
        {
            //var user = _userProvider.User;
            var predicate = PredicateBuilder.New<Account>(true);
            var accounts = _userProvider.User.Accounts;
            var items = new List<LookupItem<Guid>>();
            if (accounts.Any())
            {
                predicate = predicate.And(s => accounts.Contains(s.Id));
                items = await _repository.Account
                .FindByCondition(predicate, false)
                .OrderBy(s => s.Name).Select(s => new LookupItem<Guid>(s.Id, s.Name, s.Code)).ToListAsync();

            }
            return Ok(items);
        }





    }
}
