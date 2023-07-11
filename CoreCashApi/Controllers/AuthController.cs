using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.DTOs.Auth;
using CoreCashApi.Email;
using CoreCashApi.Email.TemplateModel;
using CoreCashApi.Enums;
using CoreCashApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        private readonly EmailService _emailService;

        private readonly IConfiguration _config;

        public AuthController(AuthService authService, EmailService emailService, IConfiguration config)
        {
            _authService = authService;
            _emailService = emailService;
            _config = config;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] RequestLogin request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.LoginAsync(request);

                if (result!.Error == AuthError.NOT_FOUND)
                    return Unauthorized("Username atau Password salah");

                if (result!.Error == AuthError.INACTIVE)
                    return Forbid("Akun anda tidak aktif.");

                if (result!.Error == AuthError.UNVERIFIED)
                    return Unauthorized("Akun anda belum diverifikasi, silahkan cek email.");

                // TODO: Lanjut bikin verifikasi
                return Ok(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost("Registration")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] RequestRegistration request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/";

                const string verificationUrl = "Auth/VerifyEmail/";
                const string logoUrl = "constants%5C%5Cmain-logo.png";

                // const string imageUrl = "";

                var token = await _authService.RegisterAsync(request);
                if (string.IsNullOrEmpty(token))
                    return StatusCode(StatusCodes.Status503ServiceUnavailable);

                var emailVerification = new EmailVerificationModel()
                {
                    LogoUrl = baseUrl + logoUrl,
                    EmailAddress = request.Email,
                    Url = baseUrl + verificationUrl + token,
                    VerificationToken = token
                };

                var emailAddresses = new List<string>
                {
                    request.Email!
                };
                var model = new EmailModel(emailAddresses, "Verifikasi Email",
                _emailService.GetEmailTemplate("EmailVerification", emailVerification));

                bool sended = await _emailService.SendAsync(model, new CancellationToken());

                return Ok("Akun anda sudah terdaftar, Silahkan cek email anda untuk melakukan verifikasi.");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("VerifyEmail/{token}")]
        public async Task<IActionResult> VerifyUser([FromRoute] string token)
        {
            try
            {
                bool result = await _authService.VerifyUserAsync(token);
                if (!result) return BadRequest("Token sudah expire.");
                string baseUrl = _config.GetValue<string>("CORs:AllowedOrigin");
                string emailVerified = _config.GetValue<string>("CORs:EmailVerified");
                return Redirect(baseUrl + emailVerified);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("TestUser"), Authorize("USER")]
        public IActionResult GetDateTime()
        {
            // return Ok(DateTime.Now);
            return Ok(_authService.TestYield());
        }
    }
}