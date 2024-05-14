using FPH.Common;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FavorParkHotelAPI.Application.PaymentManagement.Services
{
    public class DeletePaymentService : IRequest<Response<bool>>
    {
        public DeletePaymentService(int paymentId)
        {
            PaymentId = paymentId;
        }

        public int PaymentId { get; }
    }

    public class DeletePaymentServiceHandler : BaseHandler<DeletePaymentService, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ApplicationDbContext _dbContext;

        public DeletePaymentServiceHandler(IPaymentRepository paymentRepository, ApplicationDbContext dbContext)
        {
            _paymentRepository = paymentRepository;
            _dbContext = dbContext;
        }

        public override async Task<Response<bool>> Handle(DeletePaymentService request, CancellationToken cancellationToken)
        {
            var paymentEntity = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (paymentEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment not found.");
            }

            await _paymentRepository.DeleteAsync(paymentEntity);
            await _dbContext.SaveChangesAsync();

            return Success(true);
        }
    }
}

