using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.Utilities;
using Bxsoft.Encrypt;

namespace Bxsoft.Utils.Web
{

    public class Config
    {
        #region 参数
        public string WebAddress { get; private set; }
        public string DBAddress { get; private set; }
        public int DBPort { get; private set; }
        public string DBName { get; private set; }
        public string DBUserName { get; private set; }
        public string DBPassword { get; private set; }
        public string DBConnStr { get; private set; }
        public string KeyWords { get; private set; }
        public string VideoKeyWords { get; private set; }
        public string Mp3ZipRate { get; private set; }
        public string RemoteMediaUploaderVer { get; private set; }
        public string FsControllerVer { get; private set; }

        public string ProductVersion { get; private set; }
        public bool UseLocalAccount { get; private set; }
        public bool PKI { get; private set; }
        public bool showCreateTime { get; private set; }
        public bool showStartTime { get; private set; }
        public bool StartTimeNeeded { get; private set; }
        public string Unit { get; private set; }
        public string Branch { get; private set; }
        public string BranchCode { get; private set; }
        public string Tel { get; private set; }
        public string ZRR { get; private set; }
        public string UnitAddr { get; private set; }
        public string sysTitle { get; private set; }
        public bool cascade { get; private set; }
        public string parentSystemUrl { get; private set; }
        private Dictionary<string, string> paramList = new Dictionary<string, string>();
        #endregion  参数
        public string getConnStr() { 
            //return string.Format("Data Source={0|;Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
            //    DBAddress,DBName,DBUserName,DBPassword
            //    );
            return DBConnStr;
        }
        public string getValue(string name) {
            string result = paramList[name];
            if (null == result) {
                result = string.Empty;
            }
            return result;
        }
        public string getDecryptValue(string name) {
            string result = getValue(name);
            if (!String.IsNullOrEmpty(result)) {
                result = bxEncrypt.Decrypt(result);
            }
            return result;
        }
        public static Config getConfig(string webAddress) {
            Config config = new Config();
            config.WebAddress = webAddress;
            if (!config.WebAddress.EndsWith("/")) {
                config.WebAddress += "/";
            }
            string paramUrl = config.WebAddress + "Params.aspx";
            //Console.WriteLine(paramUrl);
            string paramStr = HttpHelper.GetHtml(paramUrl);
            //Console.WriteLine(paramStr);
            if (paramStr.Length == 0) {
                return null;
            }
            string[] configLines = paramStr.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string key, value, splitStr = "=";
            foreach (string aLine in configLines) {
                key = aLine.Substring(0, aLine.IndexOf(splitStr));
                value = aLine.Substring(aLine.IndexOf(splitStr) + splitStr.Length);
                config.paramList.Add(key, value);
                //Console.WriteLine("key=" + key);
                //Console.WriteLine("value=" + value);
                //Console.WriteLine();
                switch (key) {
                    case "DBAddress":
                        config.DBAddress = bxEncrypt.Decrypt(value);
                        break;
                    case "DBPort":
                        config.DBPort = Int32.Parse(bxEncrypt.Decrypt(value));
                        break;
                    case "DBName":
                        config.DBName = bxEncrypt.Decrypt(value);
                        break;
                    case "DBUserName":
                        config.DBUserName = bxEncrypt.Decrypt(value);
                        break;
                    case "DBPassword":
                        config.DBPassword = bxEncrypt.Decrypt(value);
                        break;
                    case "DbConnStr":
                        config.DBConnStr = bxEncrypt.Decrypt(value);
                        break;
                    case "KeyWords":
                        config.KeyWords = value;
                        break;
                    case "VideoKeyWords":
                        config.VideoKeyWords = value;
                        break;
                    case "Mp3ZipRate":
                        config.Mp3ZipRate = value;
                        break;
                    case "RemoteMediaUploaderVer":
                        config.RemoteMediaUploaderVer = value;
                        break;
                    case "FsControllerVer":
                        config.FsControllerVer = value;
                        break;
                    case "ProductVersion":
                        config.ProductVersion = value;
                        break;
                    case "UseLocalAccount":
                        config.UseLocalAccount = ("1" == value);
                        break;
                    case "PKI":
                        config.PKI = ("1" == value);
                        break;
                    case "showCreateTime":
                        config.showCreateTime = ("1" == value);
                        break;
                    case "StartTimeNeeded":
                        config.StartTimeNeeded = ("1" == value);
                        break;
                    case "Unit":
                        config.Unit = value;
                        break;
                    case "Branch":
                        config.Branch = value;
                        break;
                    case "BranchCode":
                        config.BranchCode = value;
                        break;
                    case "ZRR":
                        config.ZRR = value;
                        break;
                    case "Tel":
                        config.Tel = value;
                        break;
                    case "UnitAddr":
                        config.UnitAddr = value;
                        break;
                    case "sysTitle":
                        config.sysTitle = value;
                        break;
                    case "cascade":
                        config.cascade = ("1" == value);
                        break;
                    case "parentSystemUrl":
                        config.parentSystemUrl = value;
                        break;
                    case "FsManageService":
                        config.FsManageService = value;
                        break;             
                    case "fsUserName":
                        config.fsUserName = bxEncrypt.Decrypt(value);;
                        break;
                    case "fsUserPassword":
                        config.fsUserPassword = bxEncrypt.Decrypt(value);;
                        break;
                    case "CollectUserName":
                        config.CollectUserName = bxEncrypt.Decrypt(value);;
                        break;
                    case "CollectUserPassword":
                        config.CollectUserPassword = bxEncrypt.Decrypt(value);;
                        break; 
                }
            }
            return config;
        }

        public string FsManageService { get; set; }
        public string fsUserName { get; set; }
        public string fsUserPassword { get; set; }

        public string CollectUserName { get; set; }
        public string CollectUserPassword { get; set; }
    }
}
