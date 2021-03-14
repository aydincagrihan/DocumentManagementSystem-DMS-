using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Web.Controllers
{
    public class RubricController : Controller
    {
        private readonly IService<Programs> _programService;
        private readonly IService<Course> _courseService;
        private readonly IService<User> _userService;

        public RubricController(IService<Programs> programService, IService<Course> courseService, IService<User> userService)
        {
            _programService = programService;
            _courseService = courseService;
            _userService = userService;
        }
        // GET: Home
        public IActionResult Index()
        {
            return View();
        }
    }
}