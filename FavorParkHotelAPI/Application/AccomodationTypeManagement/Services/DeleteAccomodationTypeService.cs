using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.AccomodationTypeManagement.Services
{
    public class DeleteAccomodationTypeService : IRequest<Response<bool>>
    {
        public DeleteAccomodationTypeService(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteAccomodationTypeServiceHandler : BaseHandler<DeleteAccomodationTypeService, bool>
    {
        private readonly IAccomodationTypeRepository _accomodationTypeRepository;

        public DeleteAccomodationTypeServiceHandler(IAccomodationTypeRepository accomodationTypeRepository)
        {
            _accomodationTypeRepository = accomodationTypeRepository;
        }

        public override async Task<Response<bool>> Handle(DeleteAccomodationTypeService request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var accomodationTypeEntity = await _accomodationTypeRepository.GetAccomodationTypeByIdAsync(id);
            if (accomodationTypeEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Accomodation type not found.");

            // Check if there are any rooms associated with this type
            if (accomodationTypeEntity.HotelRoomEntities.Count > 0)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Cannot delete. There are rooms associated with this accomodation type.");

            await _accomodationTypeRepository.DeleteAccomodationTypeAsync(id);

            return Success(true);
        }
    }
}
