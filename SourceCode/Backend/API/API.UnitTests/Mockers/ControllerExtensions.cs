using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.UnitTests.Mockers
{
    public static class ControllerExtensions
    {
        public static void MockControllerContext(this ControllerBase controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim("client_id", "unittests")
                    }))
                }
            };
        }
    }
}
