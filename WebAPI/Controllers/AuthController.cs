using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(MemberForLoginDto memberForLoginDto)
        {
            var memberToLogin = _authService.Login(memberForLoginDto);
            if (!memberToLogin.Success)
            {
                return BadRequest(memberToLogin.Message);
            }

            var result = _authService.CreateAccessToken(memberToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(MemberForRegisterDto memberForRegisterDto)
        {
            var memberExists = _authService.MemberExists(memberForRegisterDto.Email);
            if (!memberExists.Success)
            {
                return BadRequest(memberExists.Message);
            }

            var registerResult = _authService.Register(memberForRegisterDto, memberForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
