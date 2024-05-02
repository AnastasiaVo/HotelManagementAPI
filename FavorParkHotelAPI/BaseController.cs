using Microsoft.AspNetCore.Mvc;
using FPH.Common;
using MediatR;

namespace FavorParkHotelAPI
{
    //public class BaseController : Controller
    //{
    //    protected IActionResult Result<T>(Response<T> result)
    //    {
    //        if (result.IsSuccess)
    //        {
    //            if (result.Result is Unit)
    //            {
    //                return NoContent();
    //            }

    //            return Ok(result.Result);
    //        }
    //        else
    //        {
    //            //result.ValidationResult.AddToModelState(ModelState, null);
    //            return ValidationProblem(ModelState);
    //        }
    //    }
    //}

    public class BaseController : Controller
    {
        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; set; }

        protected IActionResult Result<T>(Response<T> result)
        {
            if (result.IsSuccess)
            {
                if (result.Result is Unit)
                {
                    return NoContent();
                }

                return Ok(result.Result);
            }
            else
            {
                //result.ValidationResult.AddToModelState(ModelState, null);
                return ValidationProblem(ModelState);
            }
        }
    }
}
