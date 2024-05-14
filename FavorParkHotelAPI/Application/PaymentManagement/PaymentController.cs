
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.PaymentManagement.Dto;
using FavorParkHotelAPI.Application.PaymentManagement.Services;
using FavorParkHotelAPI.Application.PaymentManagement.Query;
using FavorParkHotelAPI.Application.PaymentManagement.Service;

namespace FavorParkHotelAPI.Application.PaymentManagement
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto paymentDto)
        {
            var response = await _mediator.Send(new CreatePaymentService(paymentDto));
            return Result(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdatePayment(int paymentId, [FromBody] PaymentDto paymentDto)
        {
            var response = await _mediator.Send(new UpdatePaymentService(paymentId, paymentDto));
            return Result(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeletePayment([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeletePaymentService(id));
            return Result(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPayments()
        {
            var response = await _mediator.Send(new GetAllPaymentsQuery());
            return Result(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetPaymentByIdQuery(id));
            return Result(response);
        }
    }
}

