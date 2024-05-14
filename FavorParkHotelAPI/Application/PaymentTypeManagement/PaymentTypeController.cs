using MediatR;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.PaymentTypeManagement.Query;
using FavorParkHotelAPI.Application.PaymentTypeManagement.Dto;
using FavorParkHotelAPI.Application.PaymentTypeManagement.Services;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTypeController : BaseController
    {

        public PaymentTypeController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPaymentTypesAsync()
        {
            var result = await Mediator.Send(new GetAllPaymentTypesService());
            return Result(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentTypeByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetPaymentTypeByIdService(id));
            return Result(result);
        }

        [HttpPost]
        [Route("create payment type")]
        public async Task<IActionResult> CreatePaymentType([FromBody] PaymentTypeDto request)
        {
            var result = await Mediator.Send(new CreatePaymentTypeService(request));
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentType(int id, [FromBody] PaymentTypeDto request)
        {
            request.Id = id;
            var result = await Mediator.Send(new UpdatePaymentTypeService(request));
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            var result = await Mediator.Send(new DeletePaymentTypeService(id));
            return Result(result);
        }
    }
}
