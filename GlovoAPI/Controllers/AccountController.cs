using Core.Commands.Account;
using Core.Commands.Partner;
using Core.Dtos.Account;
using Core.Queries.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator _mediator, IConfiguration _config) : ControllerBase
    {
        [Authorize(AuthenticationSchemes = "RegistrationScheme")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine($"REGISTRATION: {email}");

            var result = await _mediator.Send(new RegisterCommand(email, model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });


            var lifeTime = _config["Tokens:Jwt:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });


            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var result = await _mediator.Send(new LoginCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            var lifeTime = _config["Tokens:Jwt:LifeTime"];
            var lifeTimeRefresh = _config["Tokens:Refresh:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });

            Response.Cookies.Append(
               "refreshToken",
               result.Value.RefreshToken,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTimeRefresh, out var days) ? days : 7),
               });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var result = await _mediator.Send(new GoogleLoginCommand(request));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            var lifeTime = _config["Tokens:Jwt:LifeTime"];
            var lifeTimeRefresh = _config["Tokens:Refresh:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });

            Response.Cookies.Append(
               "refreshToken",
               result.Value.RefreshToken,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTimeRefresh, out var days) ? days : 7),
               });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendLoginCodeDto model)
        {
            var result = await _mediator.Send(new SendCodeCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDto model)
        {
            var result = await _mediator.Send(new VerifyCodeCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            var lifeTime = _config["Tokens:Jwt:LifeTime"];
            var lifeTimeRefresh = _config["Tokens:Refresh:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });

            Response.Cookies.Append(
               "refreshToken",
               result.Value.RefreshToken,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTimeRefresh, out var days) ? days : 7),
               });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue(
                    "refreshToken",
                    out var refreshToken))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(new PartnerRefreshTokenCommand(refreshToken));

            if (!result.IsSuccess)
            {
                Response.Cookies.Delete("accessToken");
                Response.Cookies.Delete("refreshToken");

                return Unauthorized(new
                {
                    result.IsSuccess,
                    result.Errors
                });
            }

            var lifeTime = _config["Tokens:Jwt:LifeTime"];
            var lifeTimeRefresh = _config["Tokens:Refresh:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });

            Response.Cookies.Append(
               "refreshToken",
               result.Value.RefreshToken,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddDays(int.TryParse(lifeTimeRefresh, out var days) ? days : 7),
               });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var idRaw = User.FindFirst("id")?.Value;
            var result = await _mediator.Send(new GetProfileQuery(idRaw));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            var result = await _mediator.Send(new ForgotPasswordCommand(request));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }

        [Authorize(AuthenticationSchemes = "ResetPasswordScheme")]
        [HttpPost("SetNewPassword")]
        public async Task<IActionResult> SetNewPassword([FromBody] SetNewPasswordDto request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine($"EMAIL: {email}");

            var result = await _mediator.Send(new SetNewPasswordCommand(email, request));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }

        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyCodeDto model)
        {
            var result = await _mediator.Send(new VerifyResetCodeCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            var lifeTime = _config["Tokens:Jwt:LifeTime"];

            Response.Cookies.Append(
                "accessToken",
                result.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(int.TryParse(lifeTime, out var minutes) ? minutes : 15),
                });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpPost("update-profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto model)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine($"EMAIL: {email}");

            var result = await _mediator.Send(new UpdateProfileCommand(email, model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }
    }
}
