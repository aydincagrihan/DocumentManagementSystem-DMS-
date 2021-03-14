using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Enums;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.Helpers;
using DocumentManagementSystem.Web.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService<User> _userService;
        private readonly IService<UserType> _userTypeService;
        public LoginController(IService<User> userService, IService<UserType> userTypeService)
        {
            _userService = userService;
            _userTypeService = userTypeService;
        }
        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<JsonResult> UserLogin(LoginViewModel model)
        {
            #region Email Validity Check 
            if (EmailProcess.IsValidEmail(model.UserMail) == false)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Geçersiz E-Mail Adresi. Lütfen Kontrol Edin." });
            }
            #endregion

            var password = PasswordProcess.HesaplaSHA256(model.UserPassword).ToLower();
            var user = await _userService.SingleOrDefaultAsync(x => x.Email == model.UserMail && x.Password == password && x.IsDeleted == false);

            if (user != null)
            {
                #region Session Settings
                var userType = await _userTypeService.GetByIdAsync((int)user.UserTypeId);
                SessionManagement.ActiveUserNameSurname = user.Name + " " + user.Surname;
                SessionManagement.ActiveUserPictureUrl = user.PictureUrl;
                SessionManagement.ActiveUserId = user.Id;
                if (userType.Code == UserTypes.Admin.GetHashCode())
                {
                    HttpContext.Session.SetString("IsAdmin", userType.Name);
                    HttpContext.Session.GetString("IsAdmin");
                    SessionManagement.IsAdmin = true;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.Student.GetHashCode())
                {
                    HttpContext.Session.SetString("IsStudent", userType.Name);
                    HttpContext.Session.GetString("IsStudent");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = true;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.JuryMember.GetHashCode())
                {
                    HttpContext.Session.SetString("IsJuryMember", userType.Name);
                    HttpContext.Session.GetString("IsJuryMember");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = true;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.Assistant.GetHashCode())
                {
                    HttpContext.Session.SetString("IsAssistant", userType.Name);
                    HttpContext.Session.GetString("IsAssistant");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = true;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.Instructor.GetHashCode())
                {
                    HttpContext.Session.SetString("IsInstructor", userType.Name);
                    HttpContext.Session.GetString("IsInstructor");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = true;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.Chair.GetHashCode())
                {
                    HttpContext.Session.SetString("IsChair", userType.Name);
                    HttpContext.Session.GetString("IsChair");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = true;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.Coordinator.GetHashCode())
                {
                    HttpContext.Session.SetString("IsCoordinator", userType.Name);
                    HttpContext.Session.GetString("IsCoordinator");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = true;
                    SessionManagement.IsExternalJuryMember = false;
                }
                if (userType.Code == UserTypes.ExternalJuryMember.GetHashCode())
                {
                    HttpContext.Session.SetString("IsExternalJuryMember", userType.Name);
                    HttpContext.Session.GetString("IsExternalJuryMember");
                    SessionManagement.IsAdmin = false;
                    SessionManagement.IsStudent = false;
                    SessionManagement.IsJuryMember = false;
                    SessionManagement.IsAssistant = false;
                    SessionManagement.IsInstructor = false;
                    SessionManagement.IsChair = false;
                    SessionManagement.IsCoordinator = false;
                    SessionManagement.IsExternalJuryMember = true;
                }
                #endregion

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Sisteme Giriş İşlemi Başarıyla Gerçekleşti." });
            }
            return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Geçersiz Kullanıcı Adı veya Şifre" });

        }
    }
}