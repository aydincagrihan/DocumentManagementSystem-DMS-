using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.Announcement;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentManagementSystem.Web.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IService<Announcement> _announcementService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        public AnnouncementController(IService<Announcement> announcementService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _announcementService = announcementService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            var announcementList = _announcementService.Where(x => x.IsDeleted == false).OrderBy(x => x.ReleaseDate).AsEnumerable();
            var mapUser = _mapper.Map<IEnumerable<AnnouncementDto>>(announcementList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddAnnouncement(int announcementId, string operationType)
        {
            var model = new AnnouncementModel(announcementId, operationType, _announcementService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddAnnouncement", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(AnnouncementModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var announcement = new Announcement();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region Announcement Ekle
                    announcement = new Announcement
                    {
                        Title = model.Title,
                        Content = model.Content,
                        ReleaseDate = (DateTime)model.ReleaseDate,                       
                        IsDeleted = false
                    };
                    await _announcementService.AddAsync(announcement);
                    mesaj = "Yeni Announcement Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    #region Update İşlemi
                    announcement = await _announcementService.GetByIdAsync(model.Id);
                    announcement.Title = model.Title;
                    //announcement.Code = model.Code;
                    announcement.Content = model.Content;
                    announcement.ReleaseDate = (DateTime)model.ReleaseDate;                    
                    _announcementService.Update(announcement);
                    mesaj = "Yeni Duyuru Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        public async Task<JsonResult> Delete(int announcementId)
        {
            try
            {
                var announcement = await _announcementService.GetByIdAsync(announcementId);
                announcement.IsDeleted = true;
                _announcementService.Update(announcement);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
