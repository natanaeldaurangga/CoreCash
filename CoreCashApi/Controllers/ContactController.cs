using System.Net;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.DTOs.Contacts;
using CoreCashApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreCashApi.DTOs.Pagination;
using CoreCashApi.Enums;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        private readonly ILogger<ContactController> _logger;

        public ContactController(ContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }


        [HttpDelete("ForceDelete/{contactId}"), Authorize("USER")]
        public async Task<IActionResult> ForceDeleteContact([FromRoute] Guid contactId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.ForceDeleteContactAsync(userId, contactId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("Restore/{contactId}"), Authorize("USER")]
        public async Task<IActionResult> RestoreContact([FromRoute] Guid contactId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.RestoreContactAsync(userId, contactId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{contactId}"), Authorize("USER")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid contactId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.SoftDeleteContactAsync(userId, contactId);
                if (result == 0) return NotFound();
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{contactId}"), Authorize("USER")]
        public async Task<IActionResult> ContactDetail([FromRoute] Guid contactId)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.GetContactDetailAsync(userId, contactId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("OnlyTrashed"), Authorize("USER")]
        public async Task<IActionResult> OnlyTrashedContacts([FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.GetContactResponsePagedAsync(userId, request, TrashFilter.ONLY_TRASHED);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet, Authorize("USER")]
        public async Task<IActionResult> ContactsPaged([FromQuery] RequestPagination request)
        {
            try
            {
                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.GetContactResponsePagedAsync(userId, request);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, Authorize("USER")]
        public async Task<IActionResult> InsertNewContact([FromBody] RequestContactCreate request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ClaimsPrincipal user = HttpContext.User;
                Guid userId = Guid.Parse(user.FindFirstValue("id"));
                var result = await _contactService.InsertNewContactAsync(userId, request);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}