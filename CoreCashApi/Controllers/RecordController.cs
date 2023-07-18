using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCashApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly RecordService _recordService;

        private readonly ILogger<RecordController> _logger;

        // TODO: buat controller teripisah untuk melakukan crud khusus untuk tiap record
        public RecordController(RecordService recordService, ILogger<RecordController> logger)
        {
            _recordService = recordService;
            _logger = logger;
        }

        [HttpGet("{recordId}"), Authorize("USER")]
        public async Task<IActionResult> GetRecordDetail([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _recordService.GetRecordDetailAsync(userId, recordId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("Force/{recordId}"), Authorize("USER")]
        public async Task<IActionResult> ForceDeleteRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _recordService.ForceDeleteRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("Restore/{recordId}"), Authorize("USER")]
        public async Task<IActionResult> RestoreRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _recordService.RestoreRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{recordId}"), Authorize("USER")]
        public async Task<IActionResult> SoftDeleteRecord([FromRoute] Guid recordId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _recordService.SoftDeleteRecordAsync(userId, recordId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}