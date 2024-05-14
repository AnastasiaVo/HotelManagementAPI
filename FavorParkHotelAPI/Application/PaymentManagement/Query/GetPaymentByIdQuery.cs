using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using FavorParkHotelAPI.Application.PaymentManagement.Dto;

namespace FavorParkHotelAPI.Application.PaymentManagement.Query
{
    public class GetPaymentByIdQuery : IRequest<Response<PaymentDto>>
    {
        public GetPaymentByIdQuery(int paymentId)
        {
            PaymentId = paymentId;
        }

        public int PaymentId { get; }
    }

    public class GetPaymentByIdQueryHandler : BaseHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public override async Task<Response<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentEntity = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (paymentEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment not found.");
            }


            var paymentDto = new PaymentDto
            {
                Id = paymentEntity.Id,
                BookingId = paymentEntity.BookingId,
                PaymentTypeId = paymentEntity.PaymentTypeId, // Using initialized PaymentType Id
                PaymentAmount = paymentEntity.PaymentAmount,
                IsActive = paymentEntity.IsActive,
                IsPaid = paymentEntity.IsPaid,
            };

            return Success(paymentDto);
        }
    }
}
