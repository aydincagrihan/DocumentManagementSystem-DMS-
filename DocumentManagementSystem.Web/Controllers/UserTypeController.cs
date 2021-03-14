using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.UserType;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;

namespace DocumentManagementSystem.Web.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IService<UserType> _userTypeService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public UserTypeController(IService<UserType> userTypeService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userTypeService = userTypeService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var userTypeList = _userTypeService.Where(x => x.IsDeleted == false).OrderBy(x => x.Code).AsEnumerable();
            var mapUser = _mapper.Map<IEnumerable<UserTypeDto>>(userTypeList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddUserType(int userTypeId, string operationType)
        {
            var model = new UserTypeModel(userTypeId, operationType, _userTypeService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddUserType", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserType(UserTypeModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var userType = new UserType();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region UserType Sistemde Var Mı Kontrolü
                    userType = await _userTypeService.SingleOrDefaultAsync(x => x.Name == model.Name || x.Code == model.Code && x.IsDeleted == false);
                    if (userType != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip userType sistemde zaten mevcut." });
                    }
                    #endregion

                    #region UserType Ekle
                    userType = new UserType
                    {
                        Name = model.Name,
                        Code = model.Code,
                        IsDeleted = false
                    };
                    await _userTypeService.AddAsync(userType);
                    mesaj = "Yeni UserType Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    userType = await _userTypeService.GetByIdAsync(model.Id);
                    userType.Name = model.Name;
                    userType.Code = model.Code;
                    _userTypeService.Update(userType);
                    mesaj = "Yeni UserType Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int userTypeId)
        {
            try
            {
                var userType = await _userTypeService.GetByIdAsync(userTypeId);
                userType.IsDeleted = true;
                _userTypeService.Update(userType);
                //_userTypeService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
