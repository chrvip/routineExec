using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bxsoft.Utils {
    public class IpHelper {
        /// <summary>
        /// 将IP地址格式化
        /// </summary>
        /// <param name="ip"></param>
        public static string GetIP3(string iptemp) {
            string returnIP = "";
            string[] dianhaos = iptemp.Split('.');
            foreach (string dianhao in dianhaos) {
                if (dianhao.Length == 1) {
                    returnIP += "00" + dianhao + ".";
                }
                else if (dianhao.Length == 2) {
                    returnIP += "0" + dianhao + ".";
                }
                else if (dianhao.Length > 3) {
                    return "";
                }
                else {
                    returnIP += dianhao + ".";
                }
                if (Convert.ToInt16(dianhao) > 255 || Convert.ToInt16(dianhao) < 0) {
                    return "";
                }
            }
            return returnIP.TrimEnd('.');
        }
    }
}
