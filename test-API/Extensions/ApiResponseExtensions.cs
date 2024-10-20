using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using test_API.Domain.Entities;

namespace test_API.Extensions
{
    public static class ApiResponseExtensions
    {
        
        public static IActionResult ToBadRequestResponse<T>(this ApiResponse<T> response)
        {
            return new BadRequestObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = response.Message,
                Data = response.Data
            });
        }

        
        public static IActionResult ToNotFoundResponse<T>(this ApiResponse<T> response)
        {
            return new NotFoundObjectResult(new
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = response.Message,
                Data = response.Data
            });
        }

        public static IActionResult ToOkResponse<T>(this ApiResponse<T> response)
        {
            return new OkObjectResult(new
            {
                StatusCode = StatusCodes.Status200OK,
                Message = response.Message,
                Data = response.Data
            });
        }

    }
}
