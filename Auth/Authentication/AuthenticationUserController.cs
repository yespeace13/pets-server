﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLibrary.Model.Authentication;
using PetsServer.Auth.Authorization.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetsServer.Auth.Authentication;

[Route("login")]
[ApiController]
public class AuthenticationUserController : ControllerBase
{
    private AuthenticationUserService _authenticationService;

    private IMapper _mapper;
    private IConfiguration _configuration;
    public AuthenticationUserController(IMapper mapper, IConfiguration configuration)
    {
        _authenticationService = new AuthenticationUserService();
        _mapper = mapper;
        _configuration = configuration;
    }


    [HttpPost(Name = "CreteUser")]
    public IActionResult Create([FromBody] UserEdit view)
    {
        var user = _mapper.Map<UserModel>(view);
        if (ModelState.IsValid)
        {
            _authenticationService.CreateUser(user);
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult Login(string login, string password)
    {
        UserModel? person = new AuthenticationUserService().GetUser(login);

        if (person is null) return Unauthorized();
        var result = new PasswordHasher<UserModel>().VerifyHashedPassword(person, person.Password, password);
        if (result != PasswordVerificationResult.Success) return Unauthorized();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Login) };
        var jwt = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtSettings").GetSection("Issuer").Value,
                audience: _configuration.GetSection("JwtSettings").GetSection("Audience").Value,
                claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromHours(12)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings").GetSection("Key").Value)),
                    SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        var response = new
        {
            access_token = encodedJwt,
            username = person.Login
        };
        return Ok(response);
    }
}
