using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using FavorParkHotelAPI.Application.BookingManagement.Dto;

namespace FavorParkHotelAPI.Application.BookingManagement.Query
{
    public class GetBookingDetailsByIdService : IRequest<Response<BookingDetailsDto>>
    {
        public GetBookingDetailsByIdService(int bookingId)
        {
            BookingId = bookingId;
        }

        public int BookingId { get; }
    }

    public class GetBookingDetailsByIdServiceHandler : BaseHandler<GetBookingDetailsByIdService, BookingDetailsDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public GetBookingDetailsByIdServiceHandler(IBookingRepository bookingRepository, IPaymentRepository paymentRepository)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
        }

        public override async Task<Response<BookingDetailsDto>> Handle(GetBookingDetailsByIdService request, CancellationToken cancellationToken)
        {
            var bookingEntity = await _bookingRepository.GetBookingByIdAsync(request.BookingId);
            if (bookingEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Booking not found.");
            }

            // Get payment details for the booking
            var paymentEntity = await _paymentRepository.GetPaymentByBookingIdAsync(request.BookingId);
            if (paymentEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment details not found for the booking.");
            }



            // Calculate total amount based on payment amount
            decimal totalAmount = paymentEntity.PaymentAmount;

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

            return Success(bookingDetailsDto);
        }
    }
}
