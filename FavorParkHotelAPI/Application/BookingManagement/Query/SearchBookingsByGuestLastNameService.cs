using FPH.Common;
using FavorParkHotelAPI.Application.BookingManagement.Dto;
using FPH.DataBase.Abstractions;
using MediatR;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.BookingManagement.Query
{
    public class SearchBookingsByGuestLastNameService : IRequest<Response<IEnumerable<BookingDetailsDto>>>
    {
        public SearchBookingsByGuestLastNameService(string lastName) // Updated parameter name
        {
            LastName = lastName; // Updated property name
        }

        public string LastName { get; } // Updated property name
    }

    public class SearchBookingsByGuestNameServiceHandler : BaseHandler<SearchBookingsByGuestLastNameService, IEnumerable<BookingDetailsDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public SearchBookingsByGuestNameServiceHandler(IBookingRepository bookingRepository, IPaymentRepository paymentRepository)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
        }

        public override async Task<Response<IEnumerable<BookingDetailsDto>>> Handle(SearchBookingsByGuestLastNameService request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.LastName))
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "The lastName field is required."); // Updated error message
            }

            var bookings = await _bookingRepository.SearchBookingsByGuestNameAsync(request.LastName); // Updated method call
            if (!bookings.Any())
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Bookings not found for the guest name.");
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

            return new Response<IEnumerable<BookingDetailsDto>>(bookingDetailsList);
        }

    }
}
