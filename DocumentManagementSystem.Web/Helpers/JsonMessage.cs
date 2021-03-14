using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Helpers
{
    /// <summary>
    /// Metotlarda Json dönüş tipi için kullanılan sınıf
    /// </summary>
    public class JsonMessage
    {
        public string Id { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public bool YeniKayitMi { get; set; }
        public string MesajTuru { get; set; }
        public bool HataMi { get; set; }
    }
}
