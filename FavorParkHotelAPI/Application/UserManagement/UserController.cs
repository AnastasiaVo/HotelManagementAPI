using MediatR;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.Common;
using FavorParkHotelAPI;
using FavorParkHotelAPI.Application.UserManagement.Query;
using FavorParkHotelAPI.Application.UserManagement.Services;

namespace VideoHosting.Api.Application.Users;
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("exist")]
    public async Task<IActionResult> IsExist(UserLoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }
        var result = await Mediator.Send(new IsExistQuery(model), HttpContext.RequestAborted);
        return Result(result);
    }
    
    [HttpPut]
    [Route("updateUser")]
    public async Task<IActionResult> UpdateUser(UpdateUserDto model)
    {
        var result = await Mediator.Send(new UpdateUserService(User.Identity?.Name ?? string.Empty, model), HttpContext.RequestAborted);
        return Result(result);
    }


    [HttpGet]
    [Route("profileUser/{Id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var result = await Mediator.Send(new GetUserQuery(id, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("profileUser/my")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await Mediator.Send(new GetUserQuery(User.Identity?.Name ?? string.Empty, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpGet]
    [Route("findByName/{userName}")]
    public async Task<IActionResult> GetUserByName(string userName)
    {
        var result = await Mediator.Send(new GetUserByNameQuery(userName, User.Identity?.Name ?? string.Empty), HttpContext.RequestAborted);
        return Result(result);
    }
}
