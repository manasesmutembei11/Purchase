using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Purchase.Domain.Contracts;
using Purchase.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Purchase.Domain.Models.Configs;
using Purchase.Domain.Utilities;
using Purchase.Domain.DTOs.Configs;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ConfigurationController(
            IRepositoryManager repositoryManager,
            IMapper mapper
            )
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        private string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        }
        [HttpGet()]
        public IActionResult GetAppAssemblyVersion()
        {
            try
            {
                return Ok(new { version = GetAssemblyVersion() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ConfigTypes")]
        public IActionResult GetConfigTypes()
        {
            try
            {
                var items = XpaEnumExtensions.GetEnumList<ConfigType>();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveConfigTypeAsync()
        {
            try
            {
                using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
                string body = await reader.ReadToEndAsync();
                var jsonObj = JsonDocument.Parse(body);
                int configTypeId = jsonObj.RootElement.GetProperty("configType").GetInt32();
                ConfigType type = (ConfigType)configTypeId;



                ConfigBase config;
                //ConfigBase? p;
                switch (type)
                {
                    case ConfigType.ReportServer:
                        config = PrepareReportServer(body);
                        break;
                    case ConfigType.Storage:
                        config = PrepareStorage(body);
                        break;
                    case ConfigType.EmailServer:
                        config = PrepareEmailServer(body);
                        break;
                    case ConfigType.ThirdPartyClaims:
                        config = PrepareThirdPartyClaims(body);
                        break;
                    case ConfigType.ExpaqMateServer:
                        config = PrepareExpaqMateServerConfig(body);
                        break;
                    default:
                        throw new Exception("Invalid Configuration Type");

                }
                if (config is not null)
                {
                    _repositoryManager.Config.Save(config);
                    await _repositoryManager.SaveAsync();
                }

                var response = new BasicResponse() { Message = "Okay" };
                return Ok(response);
            }
            catch (DomainValidationException ex)
            {

                var response = new BasicResponse();
                ex.ValidationResult.Results.ForEach(s => response.AddError(0, s.ErrorMessage));
                response.Message = "Domain Errors";
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                var response = new BasicResponse() { Message = "Error" };
                response.AddError(0, ex.Message);
                return BadRequest(response);
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private ConfigBase PrepareExpaqMateServerConfig(string body)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Deserialize<ExpaqMateServerConfig>(body, options)!;
        }
        private ConfigBase PrepareThirdPartyClaims(string body)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Deserialize<ThirdPartyClaimConfig>(body, options)!;
        }

        private static ConfigBase PrepareEmailServer(string body)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            ConfigBase config;
            EmailConfiguration emailConfiguration = JsonSerializer.Deserialize<EmailConfiguration>(body, options)!;
            emailConfiguration.SmtpPassword = EncryptDecryptManager.Encrypt(emailConfiguration.SmtpPassword);
            config = emailConfiguration;
            return config;
        }

        private static ConfigBase PrepareStorage(string body)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Deserialize<StorageConfig>(body, options)!;
        }

        private static ConfigBase PrepareReportServer(string body)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            ConfigBase config;
            ReportServerConfig reportServerConfig = JsonSerializer.Deserialize<ReportServerConfig>(body, options)!;
            reportServerConfig.ReportServerPassword = EncryptDecryptManager.Encrypt(reportServerConfig.ReportServerPassword);
            config = reportServerConfig;
            return config;
        }



        [HttpGet("get/{configTypeId}")]
        public async Task<IActionResult> GetOperationUploadTypes(int configTypeId)
        {
            try
            {
                ConfigBaseDTO config = null;
                var configType = (ConfigType)configTypeId;
                // switch (configType) { }
                switch (configType)
                {
                    case ConfigType.ReportServer:
                        config = await ReportServerMap();
                        break;
                    case ConfigType.Storage:
                        config = await StorageConfigMap();
                        break;
                    case ConfigType.EmailServer:
                        config = await EmailServerConfigMap();
                        break;
                    case ConfigType.ThirdPartyClaims:
                        config = await ThirdPartyClaimsConfigMap();
                        break;
                    case ConfigType.ExpaqMateServer:
                        config = await ExpaqMateServerConfigMap();
                        break;

                    default:
                        throw new Exception("Invalid config type");
                }
                return Ok(config);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private async Task<ConfigBaseDTO> ExpaqMateServerConfigMap()
        {
            var config = await _repositoryManager.Config.Load<ExpaqMateServerConfig>();

            var dto = _mapper.Map<ExpaqMateServerConfigDTO>(config);
            if (dto == null) { dto = new ExpaqMateServerConfigDTO() { ConfigType = (int)ConfigType.ExpaqMateServer }; }
            return dto;
        }
        private async Task<ConfigBaseDTO> ThirdPartyClaimsConfigMap()
        {
            var config = await _repositoryManager.Config.Load<ThirdPartyClaimConfig>();

            var dto = _mapper.Map<ThirdPartyClaimConfigDTO>(config);
            if (dto == null) { dto = new ThirdPartyClaimConfigDTO() { ConfigType = (int)ConfigType.ThirdPartyClaims }; }
            return dto;
        }

        private async Task<ReportServerConfigDTO> ReportServerMap()
        {
            var config = await _repositoryManager.Config.Load<ReportServerConfig>();

            var dto = _mapper.Map<ReportServerConfigDTO>(config);
            if (dto == null) { dto = new ReportServerConfigDTO() { ConfigType = (int)ConfigType.ReportServer }; }

            dto.ReportServerPassword = "";

            return dto;
        }
        private async Task<StorageConfigDTO> StorageConfigMap()
        {
            var config = await _repositoryManager.Config.Load<StorageConfig>();
            var dto = _mapper.Map<StorageConfigDTO>(config);
            if (dto == null) { dto = new StorageConfigDTO() { ConfigType = (int)ConfigType.Storage }; }
            return dto;
        }
        private async Task<EmailConfigurationDTO> EmailServerConfigMap()
        {
            var config = await _repositoryManager.Config.Load<EmailConfiguration>();
            var dto = _mapper.Map<EmailConfigurationDTO>(config);
            if (dto == null) { dto = new EmailConfigurationDTO() { ConfigType = (int)ConfigType.EmailServer }; }
            dto.SmtpPassword = "";
            return dto;
        }




    }
}
