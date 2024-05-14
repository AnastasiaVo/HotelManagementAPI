using FPH.Common;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using FavorParkHotelAPI.Application.PaymentManagement.Dto;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.PaymentManagement.Services
{
    public class UpdatePaymentService : IRequest<Response<PaymentDto>>
    {
        public UpdatePaymentService(int paymentId, PaymentDto paymentDto)
        {
            PaymentId = paymentId;
            PaymentDto = paymentDto;
        }

        public int PaymentId { get; }
        public PaymentDto PaymentDto { get; }
    }

    public class UpdatePaymentServiceHandler : BaseHandler<UpdatePaymentService, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ApplicationDbContext _dbContext;

        public UpdatePaymentServiceHandler(IPaymentRepository paymentRepository, ApplicationDbContext dbContext)
        {
            _paymentRepository = paymentRepository;
            _dbContext = dbContext;
        }

        public override async Task<Response<PaymentDto>> Handle(UpdatePaymentService request, CancellationToken cancellationToken)
        {
            var paymentEntity = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (paymentEntity == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Payment not found.");
            }

            var paymentDto = request.PaymentDto;


            paymentEntity.BookingId = paymentDto.BookingId;
            paymentEntity.PaymentTypeId = paymentDto.PaymentTypeId; // Using initialized PaymentType Id
            paymentEntity.PaymentAmount = paymentDto.PaymentAmount;
            paymentEntity.IsActive = paymentDto.IsActive;
            paymentEntity.IsPaid = paymentDto.IsPaid;

            await _paymentRepository.UpdateAsync(paymentEntity);

            var updatedPaymentDto = new PaymentDto
            {
                Id = paymentEntity.Id,
                BookingId = paymentEntity.BookingId,
                PaymentTypeId = paymentEntity.PaymentTypeId,
                PaymentAmount = paymentEntity.PaymentAmount,
                IsActive = paymentEntity.IsActive,
                IsPaid = paymentEntity.IsPaid
            };

            await _dbContext.SaveChangesAsync();

            return Success(updatedPaymentDto);
        }
    }
}
