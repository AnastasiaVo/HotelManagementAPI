
using FavorParkHotelAPI.Application.PaymentTypeManagement.Dto;
using FPH.DataBase.Abstractions;
using FPH.Common;
using MediatR;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement.Query
{
    public class GetAllPaymentTypesService : IRequest<Response<IEnumerable<PaymentTypeDto>>>
    {
    }

    public class GetAllPaymentTypesServiceHandler : BaseHandler<GetAllPaymentTypesService, IEnumerable<PaymentTypeDto>>
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public GetAllPaymentTypesServiceHandler(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public override async Task<Response<IEnumerable<PaymentTypeDto>>> Handle(GetAllPaymentTypesService request, CancellationToken cancellationToken)
        {
            var paymentTypes = await _paymentTypeRepository.GetAllPaymentTypesAsync();
            var dtoList = paymentTypes.Select(pt => new PaymentTypeDto
            {
                Id = pt.Id,
                Type = pt.Type,
            }).ToList(); 

            return Success(dtoList);
        }
    }
}


