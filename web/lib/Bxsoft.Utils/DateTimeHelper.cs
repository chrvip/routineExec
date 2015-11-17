using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bxsoft.Utils {
   public static class DateTimeHelper {
       public static string GetYYYY_MM_DD_HH_MM_SS(this DateTime dt) {
           try {
               return dt.ToString("yyyy-MM-dd HH:mm:ss");
           }
           catch {
               return "";
           }
       }

       public static string FormatYYYY_MM_DD(this string DateStr) {
           try {
               DateTime dt = DateTime.Parse(DateStr);
               return dt.ToString("yyyy-MM-dd");
           }
           catch {
               return "";
           }

       }

       public static string GetYYYY_MM_DD(this DateTime dt) {
           try {
               return dt.ToString("yyyy-MM-dd");
           }
           catch {
               return "";
           }

       }
       public static string GetYYYY_MM_DD(this DateTime? dt) {
           try {
               if (dt.HasValue) {
                   return dt.Value.ToString("yyyy-MM-dd");
               }
               else {
                   return string.Empty;
               }
           }
           catch {
               return string.Empty;
           }

       }
    }
}
