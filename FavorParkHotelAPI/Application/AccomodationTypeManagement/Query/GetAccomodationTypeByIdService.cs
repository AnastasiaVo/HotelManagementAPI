using System.Threading;
using System.Threading.Tasks;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Query
{
    public class GetAccomodationTypeByIdService : IRequest<Response<AccomodationTypeDto>>
    {
        public GetAccomodationTypeByIdService(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetAccomodationTypeByIdServiceHandler : BaseHandler<GetAccomodationTypeByIdService, AccomodationTypeDto>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public GetAccomodationTypeByIdServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<AccomodationTypeDto>> Handle(GetAccomodationTypeByIdService request, CancellationToken cancellationToken)
        {
            var accomodationType = await _accomodationTypeRepository.GetAccomodationTypeByIdAsync(request.Id);
            if (accomodationType == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Accomodation type not found.");
            }

            var dto = new AccomodationTypeDto
            {
                Id = accomodationType.Id,
                Name = accomodationType.Name,
                NumberOfBeds = accomodationType.NumberOfBeds
            };

            return Success(dto);
        }
    }
}

