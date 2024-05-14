
using FavorParkHotelAPI.Application.PaymentTypeManagement.Dto;
using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement.Query
{
    public class GetPaymentTypeByIdService : IRequest<Response<PaymentTypeDto>>
    {
        public GetPaymentTypeByIdService(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class Handler : BaseHandler<GetPaymentTypeByIdService, PaymentTypeDto>
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public Handler(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public override async Task<Response<PaymentTypeDto>> Handle(GetPaymentTypeByIdService request, CancellationToken cancellationToken)
        {
            var paymentType = await _paymentTypeRepository.GetPaymentTypeByIdAsync(request.Id);
            if (paymentType == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment type not found.");
            }

            var dto = new PaymentTypeDto
            {
                Id = paymentType.Id,
                Type = paymentType.Type,
            };

            return Success(dto);
        }
    }
}

