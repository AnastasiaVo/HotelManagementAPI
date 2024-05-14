using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.PaymentTypeManagement.Services
{
    public class DeletePaymentTypeService : IRequest<Response<bool>>
    {
        public DeletePaymentTypeService(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeletePaymentTypeServiceHandler : BaseHandler<DeletePaymentTypeService, bool>
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public DeletePaymentTypeServiceHandler(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public override async Task<Response<bool>> Handle(DeletePaymentTypeService request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var paymentTypeEntity = await _paymentTypeRepository.GetPaymentTypeByIdAsync(id);
            if (paymentTypeEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment type not found.");

            // Check if there are any payments associated with this type
            if (paymentTypeEntity.Payment != null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Cannot delete. There are payments associated with this accomodation type.");

            await _paymentTypeRepository.DeletePaymentTypeAsync(id);

            return Success(true);
        }
    }
}
