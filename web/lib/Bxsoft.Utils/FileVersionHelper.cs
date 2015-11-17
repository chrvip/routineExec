using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Bxsoft.Utils {
    public static class FileVersionHelper {
        public static FileVersionInfo getFileVersionInfo(string filePath) {
            return FileVersionInfo.GetVersionInfo(filePath);
        }
        public static Version getFileVersion(string filePath) {
            FileVersionInfo fvi = getFileVersionInfo(filePath);
            Version v = new Version(fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
            return v;
            //Assembly assem = Assembly.ReflectionOnlyLoadFrom(filePath);
            //AssemblyName assemName = assem.GetName();
            //return assemName.Version;
        }
        public static string getFileVersionInfoStr(string filePath) {
            return getFileVersionInfo(filePath).FileVersion;
        }
    }
}
