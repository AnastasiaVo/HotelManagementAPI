using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using FPH.DataBase.Abstractions;
using FPH.Common;
using FPH.DataBase.Repositories;
using MediatR;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Query
{
    public class GetAllAccomodationTypesService : IRequest<Response<IEnumerable<AccomodationTypeDto>>>
    {
    }

    public class GetAllAccomodationTypesServiceHandler : BaseHandler<GetAllAccomodationTypesService, IEnumerable<AccomodationTypeDto>>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public GetAllAccomodationTypesServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<IEnumerable<AccomodationTypeDto>>> Handle(GetAllAccomodationTypesService request, CancellationToken cancellationToken)
        {
            var accomodationTypes = await _accomodationTypeRepository.GetAllAccomodationTypesAsync();
            var dtoList = accomodationTypes.Select(at => new AccomodationTypeDto
            {
                Id = at.Id,
                Name = at.Name,
                NumberOfBeds = at.NumberOfBeds
            }).ToList(); // Convert to list here

            return Success(dtoList);
        }
    }
}


