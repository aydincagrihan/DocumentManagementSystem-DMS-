using DocumentManagementSystem.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult GetNewPassword()
        {
            string password = PasswordProcess.GenerateRandomPassword();
            return Json(new { Password = password });
        }
    }
}
