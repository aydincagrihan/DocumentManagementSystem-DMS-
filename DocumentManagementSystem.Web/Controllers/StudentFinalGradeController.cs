using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Core.Services;
using DocumentManagementSystem.Web.DTOs;
using DocumentManagementSystem.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagementSystem.Web.Controllers
{
    public class StudentFinalGradeController : Controller
    {
        private readonly IService<StudentFinalGrade> _studentFinalGradeService;
        private readonly IService<Reports> _reportService;
        private readonly IService<User> _userService;
        private readonly IMapper _mapper;

        public StudentFinalGradeController(IService<StudentFinalGrade> studentFinalGradeService, IService<Reports> reportService, IService<User> userService, IMapper mapper)
        {
            _studentFinalGradeService = studentFinalGradeService;
            _reportService = reportService;
            _userService = userService;
            _mapper = mapper;
        }
        // GET: Home
        public async Task<IActionResult> Index()
        {
            var studentFinalGradeList = await _studentFinalGradeService.GetAllAsync();
            var studentFinalGradeListDto = new List<StudentFinalGradeDto>();
            foreach (var item in studentFinalGradeList)
            {
                var report = await _reportService.GetByIdAsync(item.ReportsId);
                var user = _userService.GetById(report.UserId);
                var studentFinalGrade = new StudentFinalGradeDto
                {
                    DuzenBicim = item.DuzenBicim,
                    Alintilama = item.Alintilama,
                    YazimveDilkalite = item.YazimveDilkalite,
                    RaporBoyut = item.RaporBoyut,
                    ProjeTanimi = item.ProjeTanimi,
                    GereksinimAnaliz = item.GereksinimAnaliz,
                    GercekciKisitlama = item.GercekciKisitlama,
                    AhlakiKonu = item.AhlakiKonu,
                    SistemTasarim = item.SistemTasarim,
                    Uygulama = item.Uygulama,
                    Standartlar = item.Standartlar,
                    Testler = item.Testler,
                    SistemKullanimKılavuz = item.SistemKullanimKılavuz,
                    CozumAciklama = item.CozumAciklama,
                    Kaynak = item.Kaynak,
                    Ekler = item.Ekler,
                    Toplantisikligi = item.Toplantisikligi,
                    IlerlemeRapor = item.IlerlemeRapor,
                    ProjeGelistirme = item.ProjeGelistirme,
                    DisiplinliGelistirme = item.DisiplinliGelistirme,
                    YeniFikir = item.YeniFikir,
                    UygulamaKalitesi = item.UygulamaKalitesi,
                    UygulamaAraclari = item.UygulamaAraclari,
                    ProjeCozum = item.ProjeCozum,
                    Duzen = item.Duzen ,
                    ZamanKullanim = item.ZamanKullanim,
                    SlaytKalite = item.SlaytKalite,
                    IletisimYetenek = item.IletisimYetenek,
                    SoruCevap = item.SoruCevap,
                    ToplamRaporPuan = item.ToplamRaporPuan,
                    ToplamDanismanPuan = item.ToplamDanismanPuan,
                    ToplamKalitePuan = item.ToplamKalitePuan,
                    ToplamSunumPuan = item.ToplamSunumPuan,
                    GenelToplamRaporPuan = item.GenelToplamRaporPuan,
                    GenelBolunmusToplamPuan = item.GenelBolunmusToplamPuan,
                    GenelOzgunluk = item.GenelOzgunluk,
                    GenelSonPuan = item.GenelSonPuan,
                    ReportNo = item.Reports.ReportNo,
                    ReportName = item.Reports.ReportName,
                    ReporstUserName = user.Name,
                    ReportsUserSurname = user.Surname
                };
                studentFinalGradeListDto.Add(studentFinalGrade);
            }
            return View(studentFinalGradeListDto);
        }
    }
}