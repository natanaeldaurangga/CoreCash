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
    public class PayableController : ControllerBase
    {
        private readonly PayableService _payableService;

        private readonly ILogger<PayableController> _logger;

        public PayableController(PayableService payableService, ILogger<PayableController> logger)
        {
            _payableService = payableService;
            _logger = logger;
        }

        [HttpPost("Payment"), Authorize("USER")]
        public async Task<IActionResult> PaymentRecord([FromBody] RequestPayablePayment request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _payableService.PaymentRecordAsync(userId, request);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{creditorId}"), Authorize("USER")]
        public async Task<IActionResult> GetPayableDetail([FromRoute] Guid creditorId, [FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _payableService.GetPayableDetailAsync(userId, creditorId, request);
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
                var result = await _payableService.GetRecordPagedAsync(userId, request);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, Authorize("USER")]
        public async Task<IActionResult> InsertNewRecord([FromBody] RequestPayableRecord request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _payableService.NewPayableAsync(userId, request);
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