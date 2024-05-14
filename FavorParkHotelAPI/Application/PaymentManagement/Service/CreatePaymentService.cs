using FavorParkHotelAPI.Application.PaymentManagement.Dto;
using FPH.Common;
using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using MediatR;

namespace FavorParkHotelAPI.Application.PaymentManagement.Service
{
    public class CreatePaymentService : IRequest<Response<PaymentDto>>
    {
        public CreatePaymentService(CreatePaymentDto paymentDto)
        {
            PaymentDto = paymentDto;
        }

        public CreatePaymentDto PaymentDto { get; }
    }

    public class CreatePaymentServiceHandler : BaseHandler<CreatePaymentService, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ApplicationDbContext _dbContext;

        public CreatePaymentServiceHandler(IPaymentRepository paymentRepository, ApplicationDbContext dbContext)
        {
            _paymentRepository = paymentRepository;
            _dbContext = dbContext;
        }

        public override async Task<Response<PaymentDto>> Handle(CreatePaymentService request, CancellationToken cancellationToken)
        {

            // Create PaymentEntity
            var paymentEntity = new PaymentEntity
            {
                BookingId = request.PaymentDto.BookingId,
                PaymentTypeId = request.PaymentDto.PaymentTypeId,
                PaymentAmount = request.PaymentDto.PaymentAmount,
                IsActive = request.PaymentDto.IsActive,
                IsPaid = request.PaymentDto.IsPaid
            };

            // Save payment to repository
            await _paymentRepository.AddAsync(paymentEntity);

            // Create PaymentDto to return
            var paymentDto = new PaymentDto
            {
                BookingId = paymentEntity.BookingId,
                PaymentTypeId = paymentEntity.PaymentTypeId,
                PaymentAmount = paymentEntity.PaymentAmount,
                IsActive = paymentEntity.IsActive,
                IsPaid = paymentEntity.IsPaid
            };

            await _dbContext.SaveChangesAsync();

            return Success(paymentDto);
            
        }
    }
}

