using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parking.EF;
using Parking.Entities;

namespace Parking.Controllers
{
    public class BaseController : Controller
    {
        protected User GetCurrentUser(UserManager<User> userManager)
        {
            if (ConsumeAppSettingElements.IsAzureActive())
            {
                return userManager.FindByEmailAsync(GetCurrentEmailFromAzureClaims()).Result;
            }
            else
            {
                return userManager.FindByIdAsync(GetCurrentUserIDFromClaims()).Result;
            }
        }

        protected string GetCurrentUserIDFromClaims()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected string GetCurrentEmailFromAzureClaims()
        {
            return AllClaimsFromAzure()[3];
        }

        protected List<string> AllClaimsFromAzure()
        {
            ClaimsIdentity claimsIdentity = ((ClaimsIdentity)User.Identity);
            return claimsIdentity.Claims.Select(x => x.Value).ToList();
        }
    }
}