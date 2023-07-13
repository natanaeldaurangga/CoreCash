using System.Security.Claims;
using CoreCashApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoreCashApi.DTOs.Records;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.Enums;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : ControllerBase
    {
        private readonly CashService _cashService;

        private readonly ILogger<CashController> _logger;

        public CashController(CashService cashService, ILogger<CashController> logger)
        {
            _cashService = cashService;
            _logger = logger;
        }

        [HttpDelete("ForceDelete/{recordId}"), Authorize("USER")]
        public async Task<IActionResult> ForceDelete([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.ForceDeleteRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("Restore/{recordId}"), Authorize("USER")]
        public async Task<IActionResult> UpdateRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.RestoreRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("{recordId}"), Authorize("USER")]
        public async Task<IActionResult> DeleteRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.SoftDeleteRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{recordId}"), Authorize("USER")]
        public async Task<IActionResult> DetailRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.GetCashRecordsDetailAsync(userId, recordId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("OnlyTrashed"), Authorize("USER")]
        public async Task<IActionResult> OnlyTrashedRecordPaged([FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.GetCashRecordsPagedAsync(userId, request, TrashFilter.ONLY_TRASHED);
                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet, Authorize("USER")]
        public async Task<IActionResult> RecordPaged([FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.GetCashRecordsPagedAsync(userId, request, TrashFilter.WITHOUT_TRASHED);
                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost, Authorize("USER")]
        public async Task<IActionResult> InsertNewRecord([FromBody] RequestCashRecord request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.InsertNewRecordAsync(userId, request);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}