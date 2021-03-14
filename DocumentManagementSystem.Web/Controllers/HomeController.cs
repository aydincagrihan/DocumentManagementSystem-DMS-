using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Programs> _programService;
        private readonly IService<Course> _courseService;
        private readonly IService<User> _userService;
        private readonly IService<Announcement> _announcementService;
        private readonly IService<Reports> _reportService;
        private readonly IMapper _mapper;

        public HomeController(IService<Programs> programService, IService<Course> courseService, IService<User> userService, IService<Announcement> announcementService, IService<Reports> reportService, IMapper mapper)
        {
            _programService = programService;
            _courseService = courseService;
            _userService = userService;
            _announcementService = announcementService;
            _reportService = reportService;
            _mapper = mapper;
        }
        // GET: Home
        public IActionResult Index()
        {
            var totalProgram = _programService.Where(x => x.IsDeleted == false).ToList().Count();
            var totalCourse = _courseService.Where(x => x.IsDeleted == false).ToList().Count();
            var totalUser = _userService.Where(x => x.IsDeleted == false).ToList().Count();
            var totalReport = _reportService.Where(x => x.IsDeleted == false).ToList().Count();

            ViewBag.TotalUser = totalUser;
            ViewBag.TotalCourse = totalCourse;
            ViewBag.TotalProgram = totalProgram;
            ViewBag.TotalReport = totalReport;

            var announcementList = _announcementService.Where(x => x.IsDeleted == false).ToList().TakeLast(2);
            var mapAnnouncement = _mapper.Map<IEnumerable<AnnouncementDto>>(announcementList);

            return View(mapAnnouncement);
        }
    }
}