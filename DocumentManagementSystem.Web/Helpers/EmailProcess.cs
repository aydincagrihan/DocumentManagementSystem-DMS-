using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Web.Helpers
{
    public static class EmailProcess
    {
        /// <summary>
        /// Email Adresi Kontrol İşlemi
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <returns></returns>
        public static bool IsValidEmail(string emailAddress)
        {
            var patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+" + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" + @"[a-zA-Z]{2,}))$";
            var reStrict = new Regex(patternStrict);
            var isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;
        }
    }
}
