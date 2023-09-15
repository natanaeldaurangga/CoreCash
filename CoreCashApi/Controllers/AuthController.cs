using System.Security.Claims;
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

        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, EmailService emailService, IConfiguration config, ILogger<AuthController> logger)
        {
            _authService = authService;
            _emailService = emailService;
            _config = config;
            _logger = logger;
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

                return Ok(result);
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }

        #region RESET PASSWORD SERVICES

        [HttpPost("RequestResetPassword")]
        public async Task<IActionResult> RequestResetPassword([FromBody] RequestResetPassword request)
        {
            var resetPasswordForm = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/api/Auth/ResetPassword/";

            const string logoUrl = "constants%5C%5Cmain-logo.png";

            try
            {
                var token = await _authService.ResetPasswordTokenAsync(request);
                if (string.IsNullOrEmpty(token))
                    return StatusCode(StatusCodes.Status503ServiceUnavailable);

                var resetPassword = new EmailResetPasswordModel()
                {
                    EmailAddress = request.Email,
                    ResetPasswordToken = token,
                    Url = resetPasswordForm + token,
                    LogoUrl = logoUrl
                };

                var emailAddresses = new List<string>
                {
                    request.Email!
                };

                var model = new EmailModel(emailAddresses, "Verifikasi Email",
                _emailService.GetEmailTemplate("EmailVerification", resetPassword));
                bool sended = await _emailService.SendAsync(model, new CancellationToken());
                return Ok("Silahkan cek email anda.");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("ResetPassword/{token}")]
        public IActionResult RedirectToResetPasswordForm([FromRoute] string token)
        {
            // Mengambil react route dari front end untuk formulir new password
            string baseUrl = _config.GetValue<string>("CORs:AllowedOrigin");
            string resetPasswordForm = _config.GetValue<string>("CORs:ResetPassword");
            return Redirect(baseUrl + resetPasswordForm + token);
        }

        [HttpPut("ResetPassword/{token}")]
        public async Task<IActionResult> ResetPassword([FromRoute] string token, [FromBody] RequestNewPassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.ResetPasswordAsync(token, request);
                return Ok("Password anda berhasil dirubah.");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        #endregion

        [HttpGet("CheckSession"), Authorize]
        public IActionResult CheckSession()
        {
            try
            {
                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("TestUser"), Authorize("USER")]
        public IActionResult GetDateTime()
        {
            return Ok(_authService.TestYield());
        }
    }
}