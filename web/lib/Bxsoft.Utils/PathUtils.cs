using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Bxsoft.Utils {
    public static class PathUtils {
        public static string Combine(string path1, string path2) {
            string result = Path.Combine(path1.Replace("/", "\\"), path2.Replace("/", "\\").TrimStart('\\'));
            if (result.Length > 2) {
                if (':' == result[1] && '\\' != result[2]) {
                    result = result.Insert(2, "\\");
                }
            }
            return result;
        }
        public static string Combine(string path1, string path2, string path3) {
            return PathUtils.Combine(PathUtils.Combine(path1, path2), path3);
        }
        public static string Combine(string path1, string path2, string path3, string path4) {
            return PathUtils.Combine(PathUtils.Combine(path1, path2, path3), path4);
        }
    }
}