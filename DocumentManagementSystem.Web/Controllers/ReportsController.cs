using AutoMapper;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Web.Models.Reports;
using System.Threading.Tasks;
using System;
using DocumentManagementSystem.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using DocumentManagementSystem.Core.Enums;
using Microsoft.AspNetCore.Hosting;

namespace DocumentManagementSystem.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IService<Reports> _reportsService;
        private readonly IService<User> _userService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        private readonly IWebHostEnvironment _env;
        private readonly IService<StudentFinalGrade> _studentFinalGradeService;
        public ReportsController(IService<Reports> reportsService, IService<User> userService, IService<StudentFinalGrade> studentFinalGradeService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _reportsService = reportsService;
            _userService = userService;
            _studentFinalGradeService = studentFinalGradeService;
            _mapper = mapper;
            _session = httpContextAccessor.HttpContext.Session;
            _env = env;
        }
        public IActionResult Index()
        {
            var reportsList = _reportsService.Include(x => x.User).Where(x => x.IsDeleted == false).OrderBy(x => x.ReportNo).ToList();

            #region Admin Değilse Sadece Kendi Verilerini Gör
            if (!SessionManagement.IsAdmin)
            {
                if (!SessionManagement.IsJuryMember)
                {
                    if (!SessionManagement.IsInstructor)
                    {
                        reportsList = reportsList.Where(x => x.UserId == SessionManagement.ActiveUserId).ToList();
                    }                    
                }                
            }
            #endregion
            var mapUser = _mapper.Map<IEnumerable<ReportsDto>>(reportsList);
            return View(mapUser);
        }

        [HttpGet]
        public IActionResult AddRate(int reportsId)
        {
            var model = new RateModel(reportsId, _reportsService, _userService);
            return PartialView("_Rate", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(RateModel model)
        {
            try
            {
                var studentFinalGrade = new StudentFinalGrade();
                string mesaj = "";

                #region Toplamlar
                //I. Rapor
                decimal? toplamRaporPuan = (model.DuzenBicim * model.DuzenBicimWeight) + (model.Alintilama * model.AlintilamaWeight) + (model.YazimveDilkalite * model.YazimveDilkaliteWeight) + (model.RaporBoyut * model.RaporBoyutWeight) + (model.ProjeTanimi * model.ProjeTanimiWeight) + (model.GereksinimAnaliz * model.GereksinimAnalizWeight) + (model.GercekciKisitlama * model.GercekciKisitlamaWeight) + (model.AhlakiKonu * model.AhlakiKonuWeight) + (model.SistemTasarim * model.SistemTasarimWeight) + (model.Uygulama * model.UygulamaWeight) + (model.Standartlar * model.StandartlarWeight) + (model.Testler * model.TestlerWeight) + (model.SistemKullanimKılavuz * model.SistemKullanimKılavuzWeight) + (model.CozumAciklama * model.CozumAciklamaWeight) + (model.Kaynak * model.KaynakWeight) + (model.Ekler * model.EklerWeight);
                //II. Danışman
                var toplamDanismanPuan = (model.Toplantisikligi * model.ToplantisikligiWeight) + (model.IlerlemeRapor * model.IlerlemeRaporWeight) + (model.ProjeGelistirme * model.ProjeGelistirmeWeight);
                //III. Projenin Kalitesi
                var toplamProjeKalitePuan = (model.DisiplinliGelistirme * model.DisiplinliGelistirmeWeight) + (model.YeniFikir * model.YeniFikirWeight) + (model.UygulamaKalitesi * model.UygulamaKalitesiWeight) + (model.UygulamaAraclari * model.UygulamaAraclariWeight) + (model.ProjeCozum * model.ProjeCozumWeight);
                //IV. Sunum
                var toplamSunumPuan = (model.Duzen * model.DuzenWeight) + (model.ZamanKullanim * model.ZamanKullanimWeight) + (model.SlaytKalite * model.SlaytKaliteWeight) + (model.IletisimYetenek * model.IletisimYetenekWeight) + (model.SoruCevap * model.SoruCevapWeight);
                //Toplam Puanlar
                var toplamPuan = (toplamRaporPuan + toplamDanismanPuan + toplamProjeKalitePuan + toplamSunumPuan) / 4;
                //Genel Toplam Puan
                var genelToplamPuan = toplamRaporPuan + toplamDanismanPuan + toplamProjeKalitePuan + toplamSunumPuan;

                #endregion

                #region Reports Ekle
                studentFinalGrade = new StudentFinalGrade
                {
                    ReportsId = model.ReportsId,
                    //I. Rapor
                    DuzenBicim = (decimal)model.DuzenBicim,
                    Alintilama = (decimal)model.Alintilama,
                    YazimveDilkalite = (decimal)model.YazimveDilkalite,
                    RaporBoyut = (decimal)model.RaporBoyut,
                    ProjeTanimi = (decimal)model.ProjeTanimi,
                    GereksinimAnaliz = (decimal)model.GereksinimAnaliz,
                    GercekciKisitlama = (decimal)model.GercekciKisitlama,
                    AhlakiKonu = (decimal)model.AhlakiKonu,
                    SistemTasarim = (decimal)model.SistemTasarim,
                    Uygulama = (decimal)model.Uygulama,
                    Standartlar = (decimal)model.Standartlar,
                    Testler = (decimal)model.Testler,
                    SistemKullanimKılavuz = (decimal)model.SistemKullanimKılavuz,
                    CozumAciklama = (decimal)model.CozumAciklama,
                    Kaynak = (decimal)model.Kaynak,
                    Ekler = (decimal)model.Ekler,
                    ToplamRaporPuan = (decimal)toplamRaporPuan,
                    //II. Danışman
                    Toplantisikligi = (decimal)model.Toplantisikligi,
                    IlerlemeRapor = (decimal)model.IlerlemeRapor,
                    ProjeGelistirme = (decimal)model.ProjeGelistirme,
                    ToplamDanismanPuan = (decimal)toplamDanismanPuan,
                    //III. Projenin Kalitesi
                    DisiplinliGelistirme = (decimal)model.DisiplinliGelistirme,
                    YeniFikir = (decimal)model.YeniFikir,
                    UygulamaKalitesi = (decimal)model.UygulamaKalitesi,
                    UygulamaAraclari = (decimal)model.UygulamaAraclari,
                    ProjeCozum = (decimal)model.ProjeCozum,
                    ToplamKalitePuan = (decimal)toplamProjeKalitePuan,
                    //IV. Sunum
                    Duzen = (decimal)model.Duzen,
                    ZamanKullanim = (decimal)model.ZamanKullanim,
                    SlaytKalite = (decimal)model.SlaytKalite,
                    IletisimYetenek = (decimal)model.IletisimYetenek,
                    SoruCevap = (decimal)model.SoruCevap,
                    ToplamSunumPuan = (decimal)toplamSunumPuan,
                    //Toplamlar
                    GenelToplamRaporPuan = (decimal)toplamPuan,
                    GenelBolunmusToplamPuan = (decimal)genelToplamPuan,
                    GenelOzgunluk = (decimal)model.Ozgunluk,
                    GenelSonPuan = ((decimal)toplamPuan * (decimal)model.Ozgunluk) / 100
                };
                await _studentFinalGradeService.AddAsync(studentFinalGrade);
                mesaj = "Puanlama Başarıyle Eklendi.";
                #endregion

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        [HttpGet]
        public IActionResult AddReports(int reportsId, string operationType)
        {
            var model = new ReportsModel(reportsId, operationType, _reportsService);
            _session.SetString("OperationType", operationType);
            return PartialView("_AddReports", model);
        }


        [HttpPost]
        public async Task<IActionResult> AddReports(IFormFile file, ReportsModel model)
        {
            try
            {
                var OperationType = _session.GetString("OperationType");
                var reports = new Reports();
                string mesaj = "";

                //Yeni Kayıt İse ekle 
                if (OperationType == Constants.OperationType.Insert)
                {
                    #region Reports Sistemde Var Mı Kontrolü
                    //reports = await _reportsService.SingleOrDefaultAsync(x => x.ReportNo == model.ReportNo || x.UserId == model.UserId && x.IsDeleted == false);
                    //if (reports != null)
                    //{
                    //    return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "Eklemek istediğiniz özelliklere sahip rapor sistemde zaten mevcut." });
                    //}
                    #endregion

                    #region File Upload
                    // Extract file name from whatever was posted by browser
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    string filePath = $"{_env.WebRootPath}/StudentReports/{fileName}";
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // Create new local file and copy contents of uploaded file
                    using (var localFile = System.IO.File.OpenWrite(filePath))
                    using (var uploadedFile = file.OpenReadStream())
                    {
                        uploadedFile.CopyTo(localFile);
                    }
                    #endregion

                    #region Reports Ekle
                    reports = new Reports
                    {
                        UserId = SessionManagement.ActiveUserId,
                        ReportName = model.ReportName,
                        ReportNo = model.ReportNo,
                        Description = model.Description,
                        ReportPath = fileName,
                        IsDeleted = false
                    };
                    await _reportsService.AddAsync(reports);
                    mesaj = "Yeni Reports Başarıyle Eklendi.";
                    #endregion
                }
                if (OperationType == Constants.OperationType.Update)
                {
                    reports = await _reportsService.GetByIdAsync(model.Id);

                    #region File Upload

                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    //Önce eski dosyayı silmesi için eski dosyanın yolu gönderiliyor.
                    string filePath = $"{_env.WebRootPath}/StudentReports/{reports.ReportPath}";
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    //Sonra yeni dosya yolu gönderiliyor.
                    filePath = $"{_env.WebRootPath}/StudentReports/{fileName}";

                    // Create new local file and copy contents of uploaded file
                    using (var localFile = System.IO.File.OpenWrite(filePath))
                    using (var uploadedFile = file.OpenReadStream())
                    {
                        uploadedFile.CopyTo(localFile);
                    }
                    #endregion

                    #region Update İşlemi

                    reports.ReportName = model.ReportName;
                    reports.Description = model.Description;
                    reports.ReportPath = fileName;
                    _reportsService.Update(reports);
                    mesaj = "Yeni Reports Başarıyle Güncellendi.";
                    #endregion
                }

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = mesaj });
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Sırasında Hata Oluştu." });
            }
        }

        [HttpGet]
        public IActionResult DownloadFile(int reportsId)
        {
            var reports = _reportsService.GetById(reportsId);
            string path = reports.ReportPath;
            return Json(new { path = path.ToString() });
        }

        public async Task<JsonResult> Delete(int reportsId)
        {
            try
            {
                var reports = await _reportsService.GetByIdAsync(reportsId);
                reports.IsDeleted = true;
                _reportsService.Update(reports);
                //_reportsService.Remove(user);

                return Json(new JsonMessage { HataMi = false, Baslik = "İşlem Başarılı", Mesaj = "Kayıt Silme İşleminiz Başarıyla Gerçekleşti." });
            }
            catch (Exception)
            {
                return Json(new JsonMessage { HataMi = true, Baslik = "İşlem Başarısız", Mesaj = "İşlem Başarısız. Yöneticinize Başvurun." });
            }
        }
    }
}
