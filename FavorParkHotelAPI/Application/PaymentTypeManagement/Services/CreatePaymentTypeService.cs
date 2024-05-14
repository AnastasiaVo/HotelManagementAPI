using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using FPH.Data.Entities;
using FavorParkHotelAPI.Application.PaymentTypeManagement.Dto;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement.Services
{
    public class CreatePaymentTypeService : IRequest<Response<PaymentTypeDto>>
    {
        public CreatePaymentTypeService(PaymentTypeDto dto)
        {
            Dto = dto;
        }

        public PaymentTypeDto Dto { get; }
    }

    public class CreatePaymentTypeServiceHandler : BaseHandler<CreatePaymentTypeService, PaymentTypeDto>
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public CreatePaymentTypeServiceHandler(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public override async Task<Response<PaymentTypeDto>> Handle(CreatePaymentTypeService request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var paymentTypeEntity = new PaymentTypeEntity
            {
                //Id = dto.Id,
                Type = dto.Type,
            };

            await _paymentTypeRepository.AddPaymentTypeAsync(paymentTypeEntity);

            return Success(new PaymentTypeDto
            {
                Id = paymentTypeEntity.Id,
                Type = paymentTypeEntity.Type,
            });
        }
    }
}
