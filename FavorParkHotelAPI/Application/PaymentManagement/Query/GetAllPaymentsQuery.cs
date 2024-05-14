using FPH.Common;
using FPH.DataBase.Abstractions;
using MediatR;
using FavorParkHotelAPI.Application.PaymentManagement.Dto;

namespace FavorParkHotelAPI.Application.PaymentManagement.Query
{
    public class GetAllPaymentsQuery : IRequest<Response<IEnumerable<PaymentDto>>>
    {
    }

    public class GetAllPaymentsQueryHandler : BaseHandler<GetAllPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public override async Task<Response<IEnumerable<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllAsync();
            var paymentDtos = payments.Select(payment =>
            {

                return new PaymentDto
                {
                    Id = payment.Id,
                    BookingId = payment.BookingId,
                    PaymentTypeId = payment.PaymentTypeId, // Using initialized PaymentType Id
                    PaymentAmount = payment.PaymentAmount,
                    IsActive = payment.IsActive,
                    IsPaid = payment.IsPaid
                };
            }).ToList();

            return Success(paymentDtos);
        }
    }
}

