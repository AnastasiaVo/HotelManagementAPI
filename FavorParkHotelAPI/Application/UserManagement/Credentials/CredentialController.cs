using System.Net;
using System.Net.Mail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Service;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Query;
using SendGrid.Helpers.Mail;
using SendGrid;
namespace FavorParkHotelAPI.Application.UserManagement.Credentials;


[Route("api/[controller]")]
[ApiController]
public class CredentialController : BaseController
{
    public CredentialController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut]
    [Route("ResetPassword")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new ResetPasswordCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }

        return BadRequest("Invalid data");

    }

    [HttpPut]
    [Route("RecoverByEmail")]
    public async Task<IActionResult> ResetPasswordByEmail(ResetPasswordModelByEmail model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new ResetPasswordByEmailCommand(model), HttpContext.RequestAborted);
            return Result(result);
        }

        return BadRequest("Invalid data");
    }

    [HttpPut]
    [Route("DropByEmail")]
    public async Task<IActionResult> DropPassword(string email)
    {
        try
        {
            var result = await Mediator.Send(new DropPasswordCommand(email), HttpContext.RequestAborted);
            var temporaryPassword = result.Result.ToString();

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("anastasiavoronichenko7@gmail.com", "Anastasia Voronichenko");
            var to = new EmailAddress(email);
            var subject = "Recreation password";
            var plainTextContent = $"Here is your temporary password: {temporaryPassword}";
            var htmlContent = $"<strong>Here is your temporary password:</strong> {temporaryPassword}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            return Ok("Temporary password has been sent to the provided email address.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while sending the email: {ex.Message}");
        }
    }


    [HttpPut]
    [Route("addAdmin/{Id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddAdmin(string id)
    {
        var result = await Mediator.Send(new CreateAdminCommand(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("removeAdmin/{Id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RemoveAdmin(string id)
    {
        var result = await Mediator.Send(new RemoveAdminCommand(id), HttpContext.RequestAborted);
        return Result(result);
    }

    [HttpPut]
    [Route("updateUserLogin")]
    public async Task<IActionResult> UpdateUserLogin(LoginUserModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new UpdateUserLoginCommand(User.Identity.Name, model), HttpContext.RequestAborted);
            return Result(result);
        }
        return BadRequest("Invalid data");
    }

    [HttpGet]
    [Route("loginUser")]
    public async Task<IActionResult> GetLoginUser()
    {
        var result = await Mediator.Send(new GetLoginUserQuery(User.Identity.Name), HttpContext.RequestAborted);
        return Result(result);
    }
}
