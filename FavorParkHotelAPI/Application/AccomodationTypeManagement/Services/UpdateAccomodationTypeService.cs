using FPH.Common;
using MediatR;
using FavorParkHotelAPI.Application.AccomodationTypeManagement.Dto;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Services
{
    public class UpdateAccomodationTypeService : IRequest<Response<AccomodationTypeDto>>
    {
        public UpdateAccomodationTypeService(AccomodationTypeDto dto)
        {
            Dto = dto;
        }

        public AccomodationTypeDto Dto { get; }
    }

    public class UpdateAccomodationTypeServiceHandler : BaseHandler<UpdateAccomodationTypeService, AccomodationTypeDto>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public UpdateAccomodationTypeServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<AccomodationTypeDto>> Handle(UpdateAccomodationTypeService request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var accomodationTypeEntity = await _accomodationTypeRepository.GetAccomodationTypeByIdAsync(dto.Id);
            if (accomodationTypeEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Accomodation type not found.");

            }
            // Update the entity with the new data
            accomodationTypeEntity.Name = dto.Name;
            accomodationTypeEntity.NumberOfBeds = dto.NumberOfBeds;

            await _accomodationTypeRepository.UpdateAccomodationTypeAsync(accomodationTypeEntity);

            return Success(new AccomodationTypeDto
            {
                Id = accomodationTypeEntity.Id,
                Name = accomodationTypeEntity.Name,
                NumberOfBeds = accomodationTypeEntity.NumberOfBeds
            });
        }
    }
}
