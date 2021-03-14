using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.Program;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;

namespace DocumentManagementSystem.Web.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IService<Programs> _programService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public ProgramController(IService<Programs> programService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _programService = programService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var programList = _programService.Where(x => x.IsDeleted == false).OrderBy(x => x.Code).AsEnumerable();
            var mapUser = _mapper.Map<IEnumerable<ProgramDto>>(programList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddProgram(int programId, string operationType)
        {
            var model = new ProgramModel(programId, operationType, _programService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddProgram", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProgram(ProgramModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var program = new Programs();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region Program Sistemde Var Mı Kontrolü
                    program = await _programService.SingleOrDefaultAsync(x => x.Name == model.Name || x.Code == model.Code && x.IsDeleted == false);
                    if (program != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip program sistemde zaten mevcut." });
                    }
                    #endregion

                    #region Program Ekle
                    program = new Programs
                    {
                        Name = model.Name,
                        Code = model.Code,
                        IsDeleted = false
                    };
                    await _programService.AddAsync(program);
                    mesaj = "Yeni Program Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    program = await _programService.GetByIdAsync(model.Id);
                    program.Name = model.Name;
                    program.Code = model.Code;
                    _programService.Update(program);
                    mesaj = "Yeni Program Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int programId)
        {
            try
            {
                var program = await _programService.GetByIdAsync(programId);
                program.IsDeleted = true;
                _programService.Update(program);
                //_programService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
