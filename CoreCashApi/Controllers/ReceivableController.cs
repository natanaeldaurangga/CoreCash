using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        [HttpPost, Authorize("USER")]
        public async Task<IActionResult> InsertNewRecord([FromBody] RequestReceivableRecord request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _receivableService.InsertNewRecordAsync(userId, request);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}