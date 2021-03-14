using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.DTOs
{
    public class StudentFinalGradeDto
    {
        public int Id { get; set; }

        public int ReportNo { get; set; }

        public string ReporstUserName { get; set; }

        public string ReportsUserSurname { get; set; }

        public string ReportName { get; set; }

		public decimal DuzenBicim { get; set; }
		public decimal Alintilama { get; set; }
		public decimal YazimveDilkalite { get; set; }
		public decimal RaporBoyut { get; set; }
		public decimal ProjeTanimi { get; set; }
		public decimal GereksinimAnaliz { get; set; }
		public decimal GercekciKisitlama { get; set; }
		public decimal AhlakiKonu { get; set; }
		public decimal SistemTasarim { get; set; }
		public decimal Uygulama { get; set; }
		public decimal Standartlar { get; set; }
		public decimal Testler { get; set; }
		public decimal SistemKullanimKılavuz { get; set; }
		public decimal CozumAciklama { get; set; }
		public decimal Kaynak { get; set; }
		public decimal Ekler { get; set; }
		public decimal ToplamRaporPuan { get; set; }

		//II. Danışman ile İşbirliği (10 %)
		public decimal Toplantisikligi { get; set; }
		public decimal IlerlemeRapor { get; set; }
		public decimal ProjeGelistirme { get; set; }
		public decimal ToplamDanismanPuan { get; set; }

		//III. Projenin Kalitesi ve Katkıları (30 %)
		public decimal DisiplinliGelistirme { get; set; }
		public decimal YeniFikir { get; set; }
		public decimal UygulamaKalitesi { get; set; }
		public decimal UygulamaAraclari { get; set; }
		public decimal ProjeCozum { get; set; }
		public decimal ToplamKalitePuan { get; set; }

		//IV. Sunum (20 %)
		public decimal Duzen { get; set; }
		public decimal ZamanKullanim { get; set; }
		public decimal SlaytKalite { get; set; }
		public decimal IletisimYetenek { get; set; }
		public decimal SoruCevap { get; set; }
		public decimal ToplamSunumPuan { get; set; }

		//Toplam
		public decimal GenelToplamRaporPuan { get; set; }//400 üzerinden 
		public decimal GenelBolunmusToplamPuan { get; set; }
		public decimal GenelOzgunluk { get; set; }//% ile ifade edilecek kısım
		public decimal GenelSonPuan { get; set; } //BolunmusToplamPuanXOzgunluk 
	}
}
