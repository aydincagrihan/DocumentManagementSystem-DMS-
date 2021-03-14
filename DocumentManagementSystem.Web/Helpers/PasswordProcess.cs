using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DocumentManagementSystem.Web.Helpers
{
    public static class PasswordProcess
    {
        public static string HesaplaSHA256(string text)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// 8 karakter uzunluğunda Rastgele şifre oluşturur.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomPassword()
        {
            int sifreUzunluk = 8;
            string gecerliKarakterler = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890";
            StringBuilder strB = new StringBuilder(100);
            Random random = new Random();
            while (0 < sifreUzunluk--)
            {
                strB.Append(gecerliKarakterler[random.Next(gecerliKarakterler.Length)]);
            }
            return strB.ToString();
        }

        /// <summary>
        /// şifreyi kriterlere göre kontrol edip
        /// uymayan koşullarını string listesine geri dönüş yapan methoddur.
        /// Uygun olması durumunda null döndürecektir.
        /// </summary>
        /// <param name="sifre">Kullanıcı Şifresi</param>
        /// <returns>Şifrenin Uygun Olup Olmadığı Bilgisi (Açıklaması ile beraber)</returns>
        public static string IsValidPassword(string password)
        {
            //bu kısımda şifrenin olması gereken koşulları değerlendirilecek.
            string mesaj = null;

            if (password.Length < 8)
                mesaj = "Parola en az 8 karakter olmalıdır.";

            if (!Regex.IsMatch(password, @"^(?=.*[a-z])", RegexOptions.ECMAScript))
                mesaj = "Parola en az bir küçük harf içermelidir.";

            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])", RegexOptions.ECMAScript))
                mesaj = "Parola en az bir büyük harf içermelidir.";

            //if (!Regex.IsMatch(sifre, @"^(?=.*[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)])", RegexOptions.ECMAScript))
            //    mesaj = "Parola en az bir noktalama işareti içermelidir.";

            if (!Regex.IsMatch(password, @"^(?=.*[0-9])", RegexOptions.ECMAScript))
                mesaj = "Parola en az bir rakam içermelidir.";

            return mesaj == null ? null : mesaj;
        }
    }
}
