using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.API.Middleware
{
public class ThrowUnauthorizedError
    {
        private readonly RequestDelegate _next;

        public ThrowUnauthorizedError(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Token Validation Has Failed. Request Access Denied");
            }

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException("Forbidden Access. User type does not match");
            }
        }
    }
}