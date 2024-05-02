using MediatR;
using FPH.Common;
using FPH.DataBase.Abstractions;

namespace FavorParkHotelAPI;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
    where TRequest : IRequest<Response<TResponse>>
{
    public abstract Task<Response<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    protected Response<TResponse> Success(TResponse result)
    {
        return new Response<TResponse>(result);
    }
}
