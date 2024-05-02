using FPH.Common;
using MediatR;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using FPH.DataBase.Abstractions;
using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Services
{
    public class CreateAccomodationTypeService : IRequest<Response<AccomodationTypeDto>>
    {
        public CreateAccomodationTypeService(CreateAccomodationTypeDto dto)
        {
            Dto = dto;
        }

        public CreateAccomodationTypeDto Dto { get; }
    }

    public class CreateAccomodationTypeServiceHandler : BaseHandler<CreateAccomodationTypeService, AccomodationTypeDto>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public CreateAccomodationTypeServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<AccomodationTypeDto>> Handle(CreateAccomodationTypeService request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var accomodationTypeEntity = new AccomodationTypeEntity
            {
                Name = dto.Name,
                NumberOfBeds = dto.NumberOfBeds
            };

            await _accomodationTypeRepository.AddAccomodationTypeAsync(accomodationTypeEntity);

            return Success(new AccomodationTypeDto
            {
                Id = accomodationTypeEntity.Id,
                Name = accomodationTypeEntity.Name,
                NumberOfBeds = accomodationTypeEntity.NumberOfBeds
            });
        }
    }
}
