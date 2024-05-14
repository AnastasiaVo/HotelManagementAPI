using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FavorParkHotelAPI.Application.BookingManagement.Dto;
using FavorParkHotelAPI.Application.BookingManagement.Services;
using FavorParkHotelAPI.Application.PaymentManagement.Query;
using FavorParkHotelAPI.Application.BookingManagement.Query;
using FavorParkHotelAPI.Application.RoomManagement.Services;
using FavorParkHotelAPI.Application.RoomManagement.Query;
using FavorParkHotelAPI.Application.RoomManagement.Dto;

namespace FavorParkHotelAPI.Application.BookingManagement
{
    [ApiController]
    [Route("booking")]
    [Authorize] // Add authorization if required
    public class BookingController : BaseController
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto bookingDto)
        {
            var response = await _mediator.Send(new CreateBookingService(bookingDto));
            return Result(response);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteBookingService(id));
            return Result(response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] CreateBookingDto bookingDto)
        {
            var response = await _mediator.Send(new UpdateBookingService(bookingId, bookingDto));
            return Result(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookingDetaildById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetBookingDetailsByIdService(id));
            return Result(response);
        }


        [HttpGet]
        [Route("search by user name/{guestName}")]
        public async Task<IActionResult> SearchBookingsByGuestName([FromRoute] string guestName)
        {
            var response = await _mediator.Send(new SearchBookingsByGuestLastNameService(guestName));
            return Result(response);
        }


        [HttpGet]
        [Route("search by dates")]
        public async Task<IActionResult> SearchBookingsByDates([FromRoute] DateTime startDate, DateTime endDate)
        {
            var response = await _mediator.Send(new SearchBookingsByDatesService(startDate, endDate));
            return Result(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBookings()
        {
            var response = await _mediator.Send(new GetAllBookingsService());
            return Result(response);
        }
    }
}
