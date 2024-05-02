using System.Threading;
using System.Threading.Tasks;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using FPH.DataBase.Abstractions;
using FPH.Common;
using FPH.DataBase.Repositories;
using MediatR;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Query
{
    public class GetNumberOfBedsInAccomodationTypeService : IRequest<Response<int>>
    {
        public GetNumberOfBedsInAccomodationTypeService(int accomodationTypeId)
        {
            AccomodationTypeId = accomodationTypeId;
        }

        public int AccomodationTypeId { get; }
    }

    public class GetNumberOfBedsInAccomodationTypeServiceHandler : BaseHandler<GetNumberOfBedsInAccomodationTypeService, int>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public GetNumberOfBedsInAccomodationTypeServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<int>> Handle(GetNumberOfBedsInAccomodationTypeService request, CancellationToken cancellationToken)
        {
            var numberOfBeds = await _accomodationTypeRepository.GetNumberOfBedsInAccomodationTypeAsync(request.AccomodationTypeId);
            return Success(numberOfBeds);
        }
    }
}

