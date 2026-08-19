using Core.Commands.Partner;
using Core.Dtos.Account;
using Core.Dtos.Company;
using Core.Dtos.Partner;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController(IMediator _mediator, IConfiguration _config) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] PartnerRegisterDto model)
        {
            var result = await _mediator.Send(new PartnerRegisterCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, Value = true });
        }
        
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> Verify([FromBody] VerifyCodeDto model)
        {
            var result = await _mediator.Send(new VerifyPartnerCodeCommand(model));

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
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var result = await _mediator.Send(new PartnerLoginCommand(model.Email, model.Password));

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

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            return Ok(new
            {
                IsSuccess = true,
                Value = true
            });
        }

        [Authorize(AuthenticationSchemes = "PartnerAccessScheme")]
        [HttpPost("send-request-company")]
        public async Task<IActionResult> SendRequestCompany([FromBody] AddRequestCompanyDto dto)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new SendRequestCompanyCommand(res ? id : Guid.Empty, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result = true });
        }
    }
}