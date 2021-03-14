using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.Semester;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;

namespace DocumentManagementSystem.Web.Controllers
{
    public class SemesterController : Controller
    {
        private readonly IService<Semester> _semesterService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public SemesterController(IService<Semester> semesterService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _semesterService = semesterService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var semesterList = _semesterService.Where(x => x.IsDeleted == false).OrderBy(x => x.Code).AsEnumerable();
            var mapUser = _mapper.Map<IEnumerable<SemesterDto>>(semesterList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddSemester(int semesterId, string operationType)
        {
            var model = new SemesterModel(semesterId, operationType, _semesterService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddSemester", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSemester(SemesterModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var semester = new Semester();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region Semester Sistemde Var Mı Kontrolü
                    semester = await _semesterService.SingleOrDefaultAsync(x => x.Name == model.Name && x.SemesterType == model.SemesterType && x.IsDeleted == false);
                    if (semester != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip semester sistemde zaten mevcut." });
                    }
                    #endregion

                    #region Semester Ekle
                    semester = new Semester
                    {
                        Name = model.Name,
                        SemesterType = model.SemesterType,
                        Code = model.Code,
                        IsDeleted = false
                    };
                    await _semesterService.AddAsync(semester);
                    mesaj = "Yeni Semester Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    semester = await _semesterService.GetByIdAsync(model.Id);
                    semester.Name = model.Name;
                    semester.SemesterType = model.SemesterType;
                    semester.Code = model.Code;
                    _semesterService.Update(semester);
                    mesaj = "Yeni Semester Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int semesterId)
        {
            try
            {
                var semester = await _semesterService.GetByIdAsync(semesterId);
                semester.IsDeleted = true;
                _semesterService.Update(semester);
                //_semesterService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
