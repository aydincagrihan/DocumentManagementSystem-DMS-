using DocumentManagementSystem.Core.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagementSystem.Web.Models.Reports
{
    public class RateModel
    {
        // I.Rapor %40 lık kısım
        public int Id { get; set; }
        public int ReportsId { get; set; }

        
        public decimal? DuzenBicim { get; set; }

        
        public decimal? Alintilama { get; set; }

        
        public decimal? YazimveDilkalite { get; set; }

        
        public decimal? RaporBoyut { get; set; }

        
        public decimal? ProjeTanimi { get; set; }

        
        public decimal? GereksinimAnaliz { get; set; }

        
        public decimal? GercekciKisitlama { get; set; }

        
        public decimal? AhlakiKonu { get; set; }

        
        public decimal? SistemTasarim { get; set; }

        
        public decimal? Uygulama { get; set; }

        
        public decimal? Standartlar { get; set; }

        
        public decimal? Testler { get; set; }

        
        public decimal? SistemKullanimKılavuz { get; set; }

        
        public decimal? CozumAciklama { get; set; }

        
        public decimal? Kaynak { get; set; }

        
        public decimal? Ekler { get; set; }

        #region Report Weights
        public decimal? DuzenBicimWeight { get; set; }
        public decimal? AlintilamaWeight { get; set; }
        public decimal? YazimveDilkaliteWeight { get; set; }
        public decimal? RaporBoyutWeight { get; set; }
        public decimal? ProjeTanimiWeight { get; set; }
        public decimal? GereksinimAnalizWeight { get; set; }
        public decimal? GercekciKisitlamaWeight { get; set; }
        public decimal? AhlakiKonuWeight { get; set; }
        public decimal? SistemTasarimWeight { get; set; }
        public decimal? UygulamaWeight { get; set; }
        public decimal? StandartlarWeight { get; set; }
        public decimal? TestlerWeight { get; set; }
        public decimal? SistemKullanimKılavuzWeight { get; set; }
        public decimal? CozumAciklamaWeight { get; set; }
        public decimal? KaynakWeight { get; set; }
        public decimal? EklerWeight { get; set; }

        #endregion

        //II. Danışman ile İşbirliği (10 %)
        
        public decimal? Toplantisikligi { get; set; }

        
        public decimal? IlerlemeRapor { get; set; }

        
        public decimal? ProjeGelistirme { get; set; }

        #region Danışman Weights
        public decimal? ToplantisikligiWeight { get; set; }
        public decimal? IlerlemeRaporWeight { get; set; }
        public decimal? ProjeGelistirmeWeight { get; set; }
        #endregion

        //III. Projenin Kalitesi ve Katkıları (30 %)
        
        public decimal? DisiplinliGelistirme { get; set; }

        
        public decimal? YeniFikir { get; set; }

        
        public decimal? UygulamaKalitesi { get; set; }

        
        public decimal? UygulamaAraclari { get; set; }

        
        public decimal? ProjeCozum { get; set; }

        #region Proje Kalite Weights
        public decimal? DisiplinliGelistirmeWeight { get; set; }
        public decimal? YeniFikirWeight { get; set; }
        public decimal? UygulamaKalitesiWeight { get; set; }
        public decimal? UygulamaAraclariWeight { get; set; }
        public decimal? ProjeCozumWeight { get; set; }
        #endregion

        //IV. Sunum (20 %)
        
        public decimal? Duzen { get; set; }

        
        public decimal? ZamanKullanim { get; set; }

        
        public decimal? SlaytKalite { get; set; }

        
        public decimal? IletisimYetenek { get; set; }

        
        public decimal? SoruCevap { get; set; }

        #region Sunum Weights
        public decimal? DuzenWeight { get; set; }
        public decimal? ZamanKullanimWeight { get; set; }
        public decimal? SlaytKaliteWeight { get; set; }
        public decimal? IletisimYetenekWeight { get; set; }
        public decimal? SoruCevapWeight { get; set; }
        #endregion

        //Toplam
        public decimal? ToplamRaporPuan { get; set; }//400 üzerinden 
        public decimal? BolunmusToplamPuan { get; set; }

        [Range(1, 100, ErrorMessage = "Puan aralığı 1-100 olmalıdır")]
        public decimal? Ozgunluk { get; set; }//% ile ifade edilecek kısım
        public decimal? SonPuan { get; set; } //BolunmusToplamPuanXOzgunluk 

        public string Baslik { get; set; }

        public RateModel(int reportsId, IService<DocumentManagementSystem.Core.Entities.Reports> _reportsService, IService<DocumentManagementSystem.Core.Entities.User> _userService)
        {
            ReportsId = reportsId;
            DuzenBicimWeight = 1;
            AlintilamaWeight = 1;
            YazimveDilkaliteWeight = 3;
            RaporBoyutWeight = 1;
            ProjeTanimiWeight = 3;
            GereksinimAnalizWeight = 4;
            GercekciKisitlamaWeight = 2;
            AhlakiKonuWeight = 1;
            SistemTasarimWeight = 6;
            UygulamaWeight = 6;
            StandartlarWeight = 1;
            TestlerWeight = 4;
            SistemKullanimKılavuzWeight = 3;
            CozumAciklamaWeight = 2;
            KaynakWeight = 1;
            EklerWeight = 1;

            ToplantisikligiWeight = 3;
            IlerlemeRaporWeight = 3;
            ProjeGelistirmeWeight = 4;

            DisiplinliGelistirmeWeight = 2;
            YeniFikirWeight = 6;
            UygulamaKalitesiWeight = 10;
            UygulamaAraclariWeight = 4;
            ProjeCozumWeight = 8;

            DuzenWeight = 2;
            ZamanKullanimWeight = 2;
            SlaytKaliteWeight = 4;
            IletisimYetenekWeight = 4;
            SoruCevapWeight = 8;

            var report = _reportsService.GetById(reportsId);
            var user = _userService.GetById(report.UserId);
            Baslik = user.Name + " " + user.Surname + "(" + report.ReportName + ")";

        }

        /// <summary>
        /// Ajax.BeginForm tarafında submit edilebilmesi için
        /// Bu constructor eklendi
        /// </summary>
        public RateModel()
        {

        }
    }
}
