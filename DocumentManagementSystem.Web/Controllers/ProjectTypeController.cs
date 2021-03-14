using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.ProjectType;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;

namespace DocumentManagementSystem.Web.Controllers
{
    public class ProjectTypeController : Controller
    {
        private readonly IService<ProjectType> _projectTypeService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public ProjectTypeController(IService<ProjectType> projectTypeService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _projectTypeService = projectTypeService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var projectTypeList = _projectTypeService.Where(x => x.IsDeleted == false).OrderBy(x => x.Code).AsEnumerable();
            var mapUser = _mapper.Map<IEnumerable<ProjectTypeDto>>(projectTypeList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddProjectType(int projectTypeId, string operationType)
        {
            var model = new ProjectTypeModel(projectTypeId, operationType, _projectTypeService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddProjectType", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectType(ProjectTypeModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var projectType = new ProjectType();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region ProjectType Sistemde Var Mı Kontrolü
                    projectType = await _projectTypeService.SingleOrDefaultAsync(x => x.Name == model.Name || x.Code == model.Code && x.IsDeleted == false);
                    if (projectType != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip projectType sistemde zaten mevcut." });
                    }
                    #endregion

                    #region ProjectType Ekle
                    projectType = new ProjectType
                    {
                        Name = model.Name,
                        Code = model.Code,
                        IsDeleted = false
                    };
                    await _projectTypeService.AddAsync(projectType);
                    mesaj = "Yeni ProjectType Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    projectType = await _projectTypeService.GetByIdAsync(model.Id);
                    projectType.Name = model.Name;
                    projectType.Code = model.Code;
                    _projectTypeService.Update(projectType);
                    mesaj = "Yeni ProjectType Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int projectTypeId)
        {
            try
            {
                var projectType = await _projectTypeService.GetByIdAsync(projectTypeId);
                projectType.IsDeleted = true;
                _projectTypeService.Update(projectType);
                //_projectTypeService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
