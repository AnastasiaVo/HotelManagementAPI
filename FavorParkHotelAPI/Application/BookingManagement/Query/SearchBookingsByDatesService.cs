using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using FavorParkHotelAPI.Application.BookingManagement.Dto;

namespace FavorParkHotelAPI.Application.BookingManagement.Query
{
    public class SearchBookingsByDatesService : IRequest<Response<IEnumerable<BookingDetailsDto>>>
    {
        public SearchBookingsByDatesService(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }

    public class SearchBookingsByDatesServiceHandler : BaseHandler<SearchBookingsByDatesService, IEnumerable<BookingDetailsDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public SearchBookingsByDatesServiceHandler(IBookingRepository bookingRepository, IPaymentRepository paymentRepository)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
        }

        public override async Task<Response<IEnumerable<BookingDetailsDto>>> Handle(SearchBookingsByDatesService request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.SearchBookingsByDatesAsync(request.StartDate, request.EndDate);
            if (!bookings.Any())
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Bookings not found for the specified date range.");
            }

            var bookingDetailsList = new List<BookingDetailsDto>();

            foreach (var bookingEntity in bookings)
            {
                // Get payment details for the booking, if available
                var paymentEntity = await _paymentRepository.GetPaymentByBookingIdAsync(bookingEntity.Id);
                decimal totalAmount = paymentEntity?.PaymentAmount ?? 0; // Default to 0 if payment details not found

                var bookingDetailsDto = new BookingDetailsDto
                {
                    Id = bookingEntity.Id,
                    RoomId = bookingEntity.RoomId,
                    UserId = bookingEntity.UserId,
                    NumberOfGuests = bookingEntity.NumberOfGuests,
                    NumberOfChildren = bookingEntity.NumberOfChildren,
                    CheckInDate = bookingEntity.CheckInDate.ToString("yyyy-MM-dd"),
                    CheckOutDate = bookingEntity.CheckOutDate.ToString("yyyy-MM-dd"),
                    TotalAmount = totalAmount
                };

                bookingDetailsList.Add(bookingDetailsDto);
            }

            return Success(bookingDetailsList);
        }

    }

}
