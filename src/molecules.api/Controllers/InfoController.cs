using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace molecules.api.Controllers
{
    /// <summary>
    /// Controller to test the OKTA authentication
    /// </summary>
    [ApiController]
    [Route("api")]
    public class InfoController : ControllerBase
    {
        /// <summary>
        /// GET: api/whoami
        /// </summary>
        /// <returns>return claims</returns>
        [HttpGet]
        [Route("whoami")]
        [Authorize]
        public Dictionary<string, string> GetAuthorized()
        {
            var principal = HttpContext.User.Identity as ClaimsIdentity;
            if ( principal == null) return new Dictionary<string, string>();
            return principal.Claims
                        .GroupBy(claim => claim.Type)
                            .ToDictionary(claim => claim.Key, claim => claim.First().Value);
        }

        /// <summary>
        /// GET: api/hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hello")]
        [AllowAnonymous]
        public string GetAnonymous()
        {
            return "You are anonymous";
        }
    }
}
