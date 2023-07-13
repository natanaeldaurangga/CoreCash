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

        [HttpDelete("{recordId}"), Authorize("USER")]
        public async Task<IActionResult> DeleteRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _cashService.SoftDeleteRecords(userId, recordId);
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

        [HttpPost, Authorize]
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