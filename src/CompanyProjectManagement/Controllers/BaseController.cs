using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;

namespace USP.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// User Id
        /// </summary>
        protected Guid? UserId { 
            get {
                if(User == null)
                    return null;
                var claim = User.Claims.FirstOrDefault(c => c.Type == "userId");

                if (claim == null)
                    return null;

                return Guid.Parse(claim.Value);
            }
        }
    }
}
