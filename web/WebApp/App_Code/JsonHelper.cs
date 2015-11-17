using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

public class JsonHelper {
    public static string toJson(object obj) {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        serializer.MaxJsonLength = int.Parse(WebInfo.GetInfo("MaxJsonLength"));// section.MaxJsonLength;
        serializer.RecursionLimit = int.Parse(WebInfo.GetInfo("RecursionLimit"));// section.RecursionLimit;

        ScriptingJsonSerializationSection section = ConfigurationManager.GetSection("system.web.extensions/scripting/webServices/jsonSerialization") as ScriptingJsonSerializationSection;

        if (section != null) {
            serializer.MaxJsonLength = int.Parse(WebInfo.GetInfo("MaxJsonLength"));// section.MaxJsonLength;
            serializer.RecursionLimit = section.RecursionLimit;
        }
        return serializer.Serialize(obj);

    }
}


public class JsonResult {
    public bool result { get; set; }
    public String message { get; set; }

}