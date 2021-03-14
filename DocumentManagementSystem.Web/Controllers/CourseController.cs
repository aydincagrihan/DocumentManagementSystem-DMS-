using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.Course;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentManagementSystem.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IService<Course> _courseService;
        private readonly IService<Programs> _programService;
        private readonly IService<Semester> _semesterService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public CourseController(IService<Course> courseService, IService<Programs> programService, IService<Semester> semesterService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _courseService = courseService;
            _programService = programService;
            _semesterService = semesterService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var courseList = new List<Course>();
            var course = _courseService.Include(x => x.Programs).ToList();

            foreach (var item in course)
            {
                var semester = _semesterService.GetById(item.SemesterId);
                item.Semester = semester;
                if (item.IsDeleted == false)
                {
                    courseList.Add(item);
                }
            }
            var mapUser = _mapper.Map<IEnumerable<CourseDto>>(courseList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddCourse(int courseId, string operationType)
        {
            var model = new CourseModel(courseId, operationType, _courseService);
            ViewBag.ProgramList = new SelectList(_programService.Where(x => x.IsDeleted == false), "Id", "Name");
            ViewBag.SemesterList = new SelectList(_semesterService.Where(x => x.IsDeleted == false), "Id", "NameSemesterType");
            _session.SetString("OperationType", operationType);
            return PartialView("_AddCourse", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var course = new Course();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region Course Sistemde Var Mı Kontrolü
                    course = await _courseService.SingleOrDefaultAsync(x => x.Name == model.Name && x.ProgramsId == model.ProgramsId && x.IsDeleted == false);
                    if (course != null)
                    {
                        return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip course sistemde zaten mevcut." });
                    }
                    #endregion

                    #region Course Ekle
                    course = new Course
                    {
                        Name = model.Name,
                        Code = model.Code,
                        SemesterId = model.SemesterId,
                        Descripton = model.Descripton,
                        ProgramsId = model.ProgramsId,
                        Credit = model.Credit,
                        IsDeleted = false
                    };
                    await _courseService.AddAsync(course);
                    mesaj = "Yeni Course Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    course = await _courseService.GetByIdAsync(model.Id);
                    course.Name = model.Name;
                    //course.Code = model.Code;
                    course.SemesterId = model.SemesterId;
                    course.Descripton = model.Descripton;
                    course.ProgramsId = model.ProgramsId;
                    course.Credit = model.Credit;
                    _courseService.Update(course);
                    mesaj = "Yeni Course Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int courseId)
        {
            try
            {
                var course = await _courseService.GetByIdAsync(courseId);
                course.IsDeleted = true;
                _courseService.Update(course);
                //_courseService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
