using FavorParkHotelAPI.Application.BookingManagement.Query;
using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FavorParkHotelAPI.Application.BookingManagement.Services
{
    public class DeleteBookingService : IRequest<Response<bool>>
    {
        public DeleteBookingService(int bookingId)
        {
            BookingId = bookingId;
        }

        public int BookingId { get; }
    }

    public class DeleteBookingServiceHandler : BaseHandler<DeleteBookingService, bool>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingServiceHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public override async Task<Response<bool>> Handle(DeleteBookingService request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingId = request.BookingId;

                // Retrieve the booking entity by ID
                var bookingEntity = await _bookingRepository.GetBookingByIdAsync(bookingId);
                if (bookingEntity == null)
                {
                    throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Booking not found.");
                }

                // Delete the booking entity
                await _bookingRepository.DeleteBookingAsync(bookingId);

                // Return success response
                return Success(true);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return error response
                throw new ProblemDetailsException(StatusCodes.Status500InternalServerError, "An error occurred while deleting the booking.", ex);
            }
        }
    }
}

