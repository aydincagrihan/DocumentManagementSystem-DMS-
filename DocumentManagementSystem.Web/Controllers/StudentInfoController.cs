using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.StudentInfo;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentManagementSystem.Web.Controllers
{
    public class StudentInfoController : Controller
    {
        private readonly IService<StudentInfo> _studentInfoService;
        private readonly IService<Programs> _programService;
        private readonly IService<Course> _courseService;
        private readonly IService<ProjectType> _projectTypeService;
        private readonly IMapper _mapper;
        private readonly ISession _session;

        public StudentInfoController(IService<StudentInfo> studentInfoService, IService<Programs> programService, IService<Course> courseervice, IService<ProjectType> projectTypeService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _studentInfoService = studentInfoService;
            _programService = programService;
            _courseService = courseervice;
            _projectTypeService = projectTypeService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var studentInfoList = new List<StudentInfo>();
            var studentInfo = _studentInfoService.Include(x => x.User).ToList();
            foreach (var item in studentInfo)
            {
                if (item.IsDeleted == false)
                {
                    item.Course = _courseService.GetById(item.CourseId);
                    item.Course.Programs = _programService.GetById(item.Course.ProgramsId);
                    item.ProjectType = _projectTypeService.GetById(item.ProjectTypeId);
                    studentInfoList.Add(item);
                }
            }
            #region Admin Değilse Sadece Kendi Verilerini Gör
            if (!SessionManagement.IsAdmin)
            {
                studentInfoList = studentInfoList.Where(x => x.UserId == SessionManagement.ActiveUserId).ToList();
            }
            #endregion

            var mapUser = _mapper.Map<IEnumerable<StudentInfoDto>>(studentInfoList);
            return View(mapUser);
        }

        [HttpPost]
        public IActionResult GetCourse(int programId)
        {
            ViewBag.CourseList = new SelectList(_courseService.Where(x => x.ProgramsId == programId && x.IsDeleted == false).OrderBy(x => x.Name).ToList(), "Id", "Name");
            var programCode = _programService.GetById(programId).Code;
            return Json(new { courseList = ViewBag.CourseList, programCode = programCode.ToString() });
        }

        [HttpGet]
        public IActionResult AddStudentInfo(int studentInfoId, string operationType)
        {
            var model = new StudentInfoModel(studentInfoId, operationType, _studentInfoService, _courseService, _programService);
            ViewBag.ProgramList = new SelectList(_programService.Where(x => x.IsDeleted == false), "Id", "Name");
            ViewBag.ProjectTyepList = new SelectList(_projectTypeService.Where(x => x.IsDeleted == false), "Id", "Name");
            if (operationType == Constants.OperationType.Update)
            {
                ViewBag.CourseList = new SelectList(_courseService.Where(x => x.ProgramsId == model.ProgramsId && x.IsDeleted == false), "Id", "Name");
            }
            else
            {
                ViewBag.CourseList = new SelectList(string.Empty, "Value", "Text");
            }
            _session.SetString("OperationType", operationType);
            return PartialView("_AddStudentInfo", model);
        }

        [HttpGet]
        public IActionResult DownloadFile(int studentInfoId)
        {
            var studentInfo = _studentInfoService.GetById(studentInfoId);
            var course = _courseService.GetById(studentInfo.CourseId);
            var program = _programService.GetById(course.ProgramsId);
            var projectType = _projectTypeService.GetById(studentInfo.ProjectTypeId);
            string path = "";
            if (program.Code == ProgramsType.CMPE.GetHashCode())
            {
                if (course.Name.Trim().ToLower() == "405".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "CMPE405/REPORTS/EN_SW_REPORTS.rar";
                    }
                    if (projectType.Code == ProjectTypes.HW.GetHashCode())
                    {
                        path = "CMPE405/REPORTS/EN_HW_REPORTS.rar";
                    }
                }
                if (course.Name.Trim().ToLower() == "406".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "CMPE406/REPORTS/EN_SW_REPORTS.rar";
                    }
                    if (projectType.Code == ProjectTypes.HW.GetHashCode())
                    {
                        path = "CMPE406/REPORTS/EN_HW_REPORTS.rar";
                    }
                }
            }
            if (program.Code == ProgramsType.CMSE.GetHashCode())
            {
                if (course.Name.Trim().ToLower() == "405".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "CMSE405/REPORTS/EN_SW_REPORTS.rar";
                    }
                }
                if (course.Name.Trim().ToLower() == "406".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "CMSE406/REPORTS/EN_SW_REPORTS.rar";
                    }
                }
            }
            if (program.Code == ProgramsType.BLGM.GetHashCode())
            {
                if (course.Name.Trim().ToLower() == "405".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "BLGM405/REPORTS/TR_SW_REPORTS.rar";
                    }
                    if (projectType.Code == ProjectTypes.HW.GetHashCode())
                    {
                        path = "BLGM405/REPORTS/TR_HW_REPORTS.rar";
                    }
                }
                if (course.Name.Trim().ToLower() == "406".ToLower())
                {
                    if (projectType.Code == ProjectTypes.SW.GetHashCode())
                    {
                        path = "BLGM406/REPORTS/TR_SW_REPORTS.rar";
                    }
                    if (projectType.Code == ProjectTypes.HW.GetHashCode())
                    {
                        path = "BLGM406/REPORTS/TR_HW_REPORTS.rar";
                    }
                }
            }
            return Json(new { path = path.ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentInfo(StudentInfoModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var studentInfo = new StudentInfo();
                string mesaj = "";

                if (OperationType == Constants.OperationType.Insert)
                {
                    #region StudentInfo Sistemde Var Mı Kontrolü
                    studentInfo = await _studentInfoService.SingleOrDefaultAsync(x => x.UserId == SessionManagement.ActiveUserId && x.IsDeleted == false);
                    if (studentInfo != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip ders bilgisi sistemde zaten mevcut." });
                    }
                    #endregion

                    #region Default Project Type Ataması
                    var course = await _courseService.GetByIdAsync(model.CourseId);
                    var program = await _programService.GetByIdAsync(course.ProgramsId);
                    var projectType = await _projectTypeService.SingleOrDefaultAsync(x => x.Code == ProjectTypes.SW.GetHashCode());
                    if (program.Code == ProgramsType.CMSE.GetHashCode())
                    {
                        model.ProjectTypeId = projectType.Id;
                    }
                    #endregion

                    #region StudentInfo Ekle
                    studentInfo = new StudentInfo
                    {
                        UserId = SessionManagement.ActiveUserId,
                        CourseId = model.CourseId,
                        ProjectTypeId = model.ProjectTypeId,
                        IsDeleted = false
                    };
                    await _studentInfoService.AddAsync(studentInfo);
                    mesaj = "Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    studentInfo = await _studentInfoService.GetByIdAsync(model.Id);
                    studentInfo.UserId = SessionManagement.ActiveUserId;
                    studentInfo.CourseId = model.CourseId;
                    studentInfo.ProjectTypeId = model.ProjectTypeId;
                    _studentInfoService.Update(studentInfo);
                    mesaj = "Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int studentInfoId)
        {
            try
            {
                var studentInfo = await _studentInfoService.GetByIdAsync(studentInfoId);
                studentInfo.IsDeleted = true;
                _studentInfoService.Update(studentInfo);
                //_studentInfoService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
