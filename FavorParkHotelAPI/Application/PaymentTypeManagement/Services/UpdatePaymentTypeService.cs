using FPH.Common;
using MediatR;
using FavorParkHotelAPI.Application.PaymentTypeManagement.Dto;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement.Services
{
    public class UpdatePaymentTypeService : IRequest<Response<PaymentTypeDto>>
    {
        public UpdatePaymentTypeService(PaymentTypeDto dto)
        {
            Dto = dto;
        }

        public PaymentTypeDto Dto { get; }
    }

    public class UpdateAccomodationTypeServiceHandler : BaseHandler<UpdatePaymentTypeService, PaymentTypeDto>
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public UpdateAccomodationTypeServiceHandler(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public override async Task<Response<PaymentTypeDto>> Handle(UpdatePaymentTypeService request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var paymentTypeEntity = await _paymentTypeRepository.GetPaymentTypeByIdAsync(dto.Id);
            if (paymentTypeEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment type not found.");

            }
            // Update the entity with the new data
            paymentTypeEntity.Id = dto.Id;
            paymentTypeEntity.Type = dto.Type;

            await _paymentTypeRepository.UpdatePaymentTypeAsync(paymentTypeEntity);

            return Success(new PaymentTypeDto
            {
                Id = paymentTypeEntity.Id,
                Type = paymentTypeEntity.Type,
            });
        }
    }
}
