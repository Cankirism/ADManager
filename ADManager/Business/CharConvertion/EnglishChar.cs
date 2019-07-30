using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADManager
{
  public static class EnglishChar
    {
      
        // Parametre olarak girilen metni, ingilizce karakter setine uygun döndürür.
        public static string ConvertTRCharToENChar(string textToConvert)
        {
            return String.Join("",textToConvert.Normalize(NormalizationForm.FormD)
            .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }
    }
}
