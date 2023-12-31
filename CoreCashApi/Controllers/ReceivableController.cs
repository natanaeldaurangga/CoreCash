using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.DTOs.Records;
using CoreCashApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceivableController : ControllerBase
    {
        private readonly ReceivableService _receivableService;

        private readonly ILogger<ReceivableController> _logger;

        public ReceivableController(ReceivableService receivableService, ILogger<ReceivableController> logger)
        {
            _receivableService = receivableService;
            _logger = logger;
        }

        [HttpPost("Payment"), Authorize("USER")]
        public async Task<IActionResult> PaymentRecord([FromBody] RequestReceivablePayment request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _receivableService.PaymentRecordAsync(userId, request);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{debtorId}"), Authorize("USER")]
        public async Task<IActionResult> GetReceivableDetail([FromRoute] Guid debtorId, [FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _receivableService.GetReceivableDetailAsync(userId, debtorId, request);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet, Authorize("USER")]
        public async Task<IActionResult> GetRecordPaged([FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _receivableService.GetRecordPagedAsync(userId, request);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, Authorize("USER")]
        public async Task<IActionResult> InsertNewRecord([FromBody] RequestReceivableRecord request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _receivableService.NewReceivableAsync(userId, request);
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