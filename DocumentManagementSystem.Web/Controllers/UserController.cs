using DocumentManagementSystem.Web.Models.User;
using System;
using Microsoft.AspNetCore.Mvc;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Core.Entities;
using System.Threading.Tasks;
using DocumentManagementSystem.Web.Helpers;
using DocumentManagementSystem.Core.Enums;
using System.Collections.Generic;
using AutoMapper;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace DocumentManagementSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IService<User> _userService;
        private readonly IService<UserType> _userTypeService;
        private readonly IMapper _mapper;
        private readonly ISession _session;

        private readonly IMailer _mailer;

        public UserController(IService<User> userService, IService<UserType> userTypeService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMailer mailer)
        {
            _userService = userService;
            _userTypeService = userTypeService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
            _mailer = mailer;
        }

        public IActionResult Index()
        {
            var userList = new List<User>();
            var users = _userService.Include(x => x.UserType);
            foreach (var item in users)
            {
                if (item.IsDeleted == false)
                {
                    userList.Add(item);
                }
            }
            var mapUser = _mapper.Map<IEnumerable<UserDto>>(userList);
            return View(mapUser);
        }

        public IActionResult Register()
        {
            return View();
        }

        // Post: Register
        [HttpPost]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            try
            {
                var user = new User();

                #region Email Validity Check 
                if (EmailProcess.IsValidEmail(model.Email) == false)
                {
                    return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Geçersiz E-Mail Adresi. Lütfen Kontrol Edin." });
                }
                #endregion

                #region Password Validity Check 
                var checkPassword = PasswordProcess.IsValidPassword(model.Password);
                if (checkPassword != null)
                {
                    return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = checkPassword });

                }
                if (model.Password != model.ReTypePassword)
                {
                    return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Lütfen Parola Bilgilerinizi Kontrol Edin." });
                }
                #endregion

                #region Default Picture Url Settings
                string defautPictureUrl = null;
                if (model.Gender == Constants.Gender.Male)
                {
                    defautPictureUrl = Constants.DefaultPictureUrl.DefaultPictureUrlMale;
                }
                if (model.Gender == Constants.Gender.Female)
                {
                    defautPictureUrl = Constants.DefaultPictureUrl.DefaultPictureUrlFemale;
                }
                #endregion

                #region Default User Type Settings
                var studentUserType = await _userTypeService.SingleOrDefaultAsync(x => x.Code == UserTypes.Student.GetHashCode());
                #endregion

                #region User Sistemde Var Mı Kontrolü
                user = await _userService.SingleOrDefaultAsync(x => x.Email == model.Email && x.Name.ToLower() == model.UserName.ToLower() && x.Surname.ToLower() == model.UserSurname.ToLower() && x.IsDeleted == false);
                if (user != null)
                {
                    return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip kullanıcı sistemde zaten mevcut." });
                }
                #endregion

                #region Kayıt İşlemi

                user = new User
                {
                    Name = model.UserName,
                    Surname = model.UserSurname,
                    Email = model.Email,
                    Password = PasswordProcess.HesaplaSHA256(model.Password),
                    RegisterDate = DateTime.Now,
                    IsDeleted = false,
                    UserTypeId = studentUserType.Id,
                    PictureUrl = defautPictureUrl,
                    Gender = model.Gender,
                };
                await _userService.AddAsync(user);

                #endregion

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }

        [HttpGet]
        public IActionResult UserOperations(int userId, string operationType)
        {
            var model = new UserModel(userId, operationType, _userService);
            ViewBag.UserTypeList = new SelectList(_userTypeService.Where(x => x.IsDeleted == false), "Id", "Name");
            //Session
            _session.SetString("OperationType", operationType);
            return PartialView("_AddUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> UserOperations(UserModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var user = new User();
                string mesaj = "";

                //Kayıt olan kullanıcının cinsyetine göre otomatik avatar resmi ataması.
                #region Default Picture Settings 
                var pictureUrl = model.Gender == Constants.Gender.Male ? Constants.DefaultPictureUrl.DefaultPictureUrlMale : Constants.DefaultPictureUrl.DefaultPictureUrlFemale;
                #endregion

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region User Sistemde Var Mı Kontrolü
                    user = await _userService.SingleOrDefaultAsync(x => x.Email == model.Email && x.Name.ToLower() == model.Name.ToLower() && x.Surname.ToLower() == model.Surname.ToLower() && x.IsDeleted == false);
                    if (user != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip kullanıcı sistemde zaten mevcut." });
                    }
                    #endregion

                    #region User Ekle
                    user = new User
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        Email = model.Email,
                        UserTypeId = model.UserTypeId,
                        PictureUrl = pictureUrl,
                        IsDeleted = false,
                        RegisterDate = DateTime.Now,
                        Password = PasswordProcess.HesaplaSHA256(model.Password),
                    };
                    await _userService.AddAsync(user);
                    mesaj = "Yeni Kullanıcı Başarıyle Eklendi.";
                    #endregion

                    #region Send Mail
                    string body = "Sisteme hoşgeldiniz. " + user.Email + " Şifreniz: " + model.Password;
                    await _mailer.SendEmailAsync(/*"cagrihanaydin@gmail.com"*/user.Email, "Test", body);

                    #endregion
                }
                //Güncelleme ise
                if (OperationType == Constants.OperationType.Update)
                {
                    #region User Update
                    user = await _userService.GetByIdAsync(model.Id);
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Gender = model.Gender;
                    user.PictureUrl = pictureUrl;
                    user.Email = model.Email;
                    user.UserTypeId = model.UserTypeId;
                    _userService.Update(user);
                    mesaj = "Yeni Kullanıcı Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int userId)
        {
            try
            {
                //Burada kulanıcıyı veritabanından tamamen silmiyoruz. IsDeleted alanını true olarak güncelliyoruz.
                var user = await _userService.GetByIdAsync(userId);
                user.IsDeleted = true;
                _userService.Update(user);
                //_userService.Remove(user); Bu işlem ilgili kaydı veritabanından tamamen siler.

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}