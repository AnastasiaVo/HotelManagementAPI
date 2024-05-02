using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Services;
using FPH.DataBase.Repositories;
using FPH.Common;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Query;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccomodationTypeController : BaseController
    {

        public AccomodationTypeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}/number-of-beds")]
        public async Task<IActionResult> GetNumberOfBedsInAccomodationTypeAsync(int id)
        {
            var result = await Mediator.Send(new GetNumberOfBedsInAccomodationTypeService(id));
            return Result(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccomodationTypesAsync()
        {
            var result = await Mediator.Send(new GetAllAccomodationTypesService());
            return Result(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccomodationTypeByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetAccomodationTypeByIdService(id));
            return Result(result);
        }

        [HttpPost]
        [Route("accomodation type")]
        public async Task<IActionResult> CreateAccomodationType([FromBody] CreateAccomodationTypeDto request)
        {
            var result = await Mediator.Send(new CreateAccomodationTypeService(request));
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccomodationType(int id, [FromBody] UpdateAccomodationTypeDto request)
        {
            request.Id = id;
            var result = await Mediator.Send(new UpdateAccomodationTypeService(request));
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccomodationType(int id)
        {
            var result = await Mediator.Send(new DeleteAccomodationTypeService(id));
            return Result(result);
        }
    }
}
