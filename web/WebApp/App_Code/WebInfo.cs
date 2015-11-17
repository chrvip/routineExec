using System.Configuration;
using System.Web;
using System.Web.Configuration;
using Bxsoft.Encrypt;
using System.Xml;


public class WebInfo {
    public WebInfo() {
    }

    public static string GetConnString(string ConnName) {
        Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");
        ConnectionStringSettings connString;
        if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count) {
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings[ConnName];
            if (null != connString)
                return connString.ConnectionString;

            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    public static string GetInfo(string key) {
        Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");
        KeyValueConfigurationElement kvce = rootWebConfig.AppSettings.Settings[key];
        return (string)((null == kvce) ? "" : kvce.Value);
    }

    public static string GetEncryptInfo(string key) {
        Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");
        string tmp = (string)rootWebConfig.AppSettings.Settings[key].Value;
        return bxEncrypt.Encrypt(tmp);
    }
    public static string GetWcfInfo(string name) {
        Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");
        //rootWebConfig.GetSectionGroup("system.serviceModel").SectionGroups
        //KeyValueConfigurationElement kvce =rootWebConfig.AppSettings.Settings[key];

        XmlDocument doc = new XmlDocument();
        doc.Load(rootWebConfig.FilePath);
        XmlNode endPointSetting = doc.SelectSingleNode("configuration/system.serviceModel/client/endpoint[@name='" + name + "']");

        if (null == endPointSetting) {
            return string.Empty;
        }
        else {
            return endPointSetting.Attributes["address"].Value;
        }
    }

    /// <summary>
    /// 程序开始路径
    /// </summary>
    /// <returns></returns>
    public static string ServerPath() {
        return HttpContext.Current.Server.MapPath("~");
    }
    public static string GetMyUrl() {
        string url = string.Empty;
        if (!Virtualserver.ToLower().StartsWith("http://")) {
            url += "http://";
        }
        url += Virtualserver;
        if ("80" != virtualPort) {
            url += ":" + virtualPort;
        }
        if (!VirtualRoot.StartsWith("/")) {
            url += "/";
        }
        url += VirtualRoot;
        return url;
    }



    public static string Branch {
        get {
            return WebInfo.GetInfo("Branch");
        }
    }

    public static string BranchCode {
        get {
            return WebInfo.GetInfo("BranchCode");
        }
    }

    public static string VideoKeyWords {
        get {
            return WebInfo.GetInfo("VideoKeyWords");
        }

    }

    public static string ProductVersion {
        get {

            return WebInfo.GetInfo("ProductVersion");
        }

    }


    public static string Unit {
        get {
            return WebInfo.GetInfo("Unit");
        }
    }

    public static string ZRR {
        get {
            return WebInfo.GetInfo("ZRR");
        }
    }

    public static string Tel {
        get {
            return WebInfo.GetInfo("Tel");
        }
    }


    public static string UnitAddr {
        get {
            return WebInfo.GetInfo("UnitAddr");
        }
    }

    public static string Virtualserver {
        get {
            return WebInfo.GetInfo("Virtualserver");
        }
    }
    public static string virtualPort {
        get {
            return WebInfo.GetInfo("virtualPort");
        }
    }
    public static string VirtualRoot {
        get {
            return WebInfo.GetInfo("VirtualRoot");
        }
    }


    public static string FTPAddress {
        get {
            return WebInfo.GetEncryptInfo("FTPAddress");
        }
    }


    public static string FtpUser {
        get {
            return WebInfo.GetEncryptInfo("FtpUser");
        }
    }

    public static string FtpPwd {
        get {
            return WebInfo.GetEncryptInfo("FtpPwd");
        }
    }

    public static string FtpPort {
        get {
            return WebInfo.GetEncryptInfo("FtpPort");
        }
    }

    public static string FtpRootDir {
        get {
            return WebInfo.GetEncryptInfo("FtpRootDir");
        }
    }

    public static string DbServer {
        get {
            return WebInfo.GetEncryptInfo("DbServer");
        }
    }

    public static string DBPort {
        get {
            return WebInfo.GetEncryptInfo("DBPort");
        }
    }



    public static string DbName {
        get {
            return WebInfo.GetEncryptInfo("DbName");
        }
    }


    public static string DBUserName {
        get {
            return WebInfo.GetEncryptInfo("DBUserName");
        }
    }


    public static string DBPassword {
        get {
            return WebInfo.GetEncryptInfo("DBPassword");
        }
    }

    public static string KeyWords {
        get {
            return WebInfo.GetInfo("KeyWords");
        }
    }

    public static string ShowCreateTime {
        get {
            return WebInfo.GetInfo("ShowCreateTime");
        }
    }

    public static string ShowStartTime {
        get {
            if ("1" == StartTimeNeeded) {
                return "1";
            }
            else {
                return WebInfo.GetInfo("showStartTime");
            }
        }
    }
    public static string StartTimeNeeded {
        get {
            return WebInfo.GetInfo("StartTimeNeeded");
        }
    }



    public static string UseThisDBAccount {
        get {
            return WebInfo.GetInfo("UseThisDBAccount");
        }
    }

    public static string UseLocalAccount {
        get {
            return WebInfo.GetInfo("UseLocalAccount");
        }
    }

    public static string SysMode {
        get {
            return WebInfo.GetInfo("SysMode");
        }
    }


    public static bool CanSP {
        get {
            return "true" == WebInfo.GetInfo("CanSP").ToLower();
        }
    }

    public static string DmNotDeal {
        get {
            return "0";
        }
    }


    public static string DmDel {
        get {
            return "1";
        }
    }


    public static string DmMove {
        get {
            return "2";
        }
    }

    public static string Mp3ZipRate {
        get {
            return WebInfo.GetInfo("Mp3ZipRate");
        }
    }

    public static string OmmitCerUnitMc {
        get {
            return WebInfo.GetInfo("OmmitCerUnitMc");
        }
    }



    public static string AppPMICode {
        get {
            return WebInfo.GetInfo("AppPMICode");
        }
    }

    public static string PKILoginURL {
        get {
            return WebInfo.GetInfo("PKILoginURL");
        }
    }

    public static string UnitLevel {
        get {
            return WebInfo.GetInfo("UnitLevel");
        }
    }

    public static string SysTitle {
        get {
            return WebInfo.GetInfo("SysTitle");
        }
    }

    public static string JYZX {
        get {
            return WebInfo.GetInfo("JYZX");
        }
    }
    public static string SoftRegIp {
        get {
            return WebInfo.GetInfo("SoftRegIp");
        }
    }
    public static string SoftRegNo {
        get {
            return WebInfo.GetInfo("SoftRegNo");
        }
    }
    public static string SoftRegSn {
        get {
            return WebInfo.GetInfo("SoftRegSn");
        }
    }

    public static string SaveTime {
        get {
            return WebInfo.GetInfo("SaveTime");
        }
    }


    public static bool SyncFromDPT {
        get {
            return ("1" == WebInfo.GetInfo("SyncFromDPT"));
        }
    }
    public static bool canDelFile {
        get {
            return ("true" == WebInfo.GetInfo("canDelFile").ToLower());
        }
    }

    internal static void setInfo(string key, string value) {
        Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");
        KeyValueConfigurationElement kvce = rootWebConfig.AppSettings.Settings[key];
        if (null == kvce) {
            rootWebConfig.AppSettings.Settings.Add(key, value);
        }
        else {
            kvce.Value = value;
        }
        rootWebConfig.Save(ConfigurationSaveMode.Minimal);
        ConfigurationManager.RefreshSection("appSettings");
    }
}