﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FPH.Common;
using FavorParkHotelAPI.Application.Authentification.Services;
using FavorParkHotelAPI.Application.Authentification.Dto;

namespace FavorParkHotelAPI.Application.Authentification;

//[ApiController]
//[Route("account")]
//[AllowAnonymous]
//public class AuthorizationController : BaseController
//{
//    private readonly LoginService _loginService;
//    private readonly CreateUserService _createUserService;

//    public AuthorizationController(LoginService loginService, CreateUserService createUserService)
//    {
//        _loginService = loginService;
//        _createUserService = createUserService;
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody, Required] LogInDto request)
//    {
//        var result = await _loginService.LoginAsync(request);
//        return Result(result);
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> CreateUser([FromBody, Required] RegisterUserDto request)
//    {
//        var result = await _createUserService.CreateUserAsync(request);
//        return Result(result);
//    }
//}


[ApiController]
[Route("account")]
[AllowAnonymous]
public class AuthorizationController : BaseController
{
    public AuthorizationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody, Required] LogInDto request)
    {
        var result = await Mediator.Send(new LoginService(request), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody, Required] RegisterUserDto request)
    {
        var result = await Mediator.Send(new CreateUserService(request), HttpContext.RequestAborted);
        return Result(result);
    }
}