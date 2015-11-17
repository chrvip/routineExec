using System;
using System.Web;
using System.Text;

/// <summary>
///为了便于使用，不用在调用工具方法时，添加一些长长的参数，影响代码的整洁与阅读，直接给网页类添加了些工具方法
///网页类有两个一个是System.Web.UI.Page，另一个是System.Web.UI.UserControl
///本工具类主要是把Function里边涉及到HTTP上下文的方法搬过来了，这样就无需传递类似于 HttpContext.Current...这样的上下文环境了
///性能问题：用扩展方法，是编译时把代码注入类空间，对执行效率无影响，但是会微小的加长编译时间，不过一般可以忽略不计
///         Linq的库实现时，大量运用了扩展方法，证明这种方法是可取的。
///使用方法：直接把本文件添加工程中，然后就可以在页面文件上直接使用了
/// </summary>
public static class WebExternal {
    public static void rw(this System.Web.UI.Page page, string value) {
        page.Response.Write(value);
    }
    public static void rwln(this System.Web.UI.Page page, string value) {
        page.Response.Write(value + "\n");
    }
    public static void drw(this System.Web.UI.Page page, string value) {
        page.Response.Write(value + "<br/>\n");
    }
    public static void drwsql(this System.Web.UI.Page page, string value) {
        page.Response.Write("<pre>" + value + "</pre>\n");
    }

    public static void rwFormat(this System.Web.UI.Page page, string format, params object[] args) {
        page.Response.Write(String.Format(format, args));
    }

    public static string GetRequest(this System.Web.UI.Page page, string key) {

        if (page.Request[key] == null)
            return string.Empty;
        else
            return page.Request[key].ToString().Trim();
    }
    public static string GetSession(this System.Web.UI.Page page, string key) {
        try {
            if (page.Session[key] == null)
                return "";
            else
                return page.Session[key].ToString().Trim();
        }
        catch {
            return string.Empty;
        }
    }
    /// <summary>
    /// 检验是否已经登录－即有权限
    /// </summary>
    /// <returns>返回true or false</returns>
    public static bool CheckLevel(this System.Web.UI.Page page) {
        bool result = true;
        string Levels = (page.Session["Role_Levels"] == null) ? "" : page.Session["Role_Levels"].ToString();
        if (Levels == "" || Levels == null) {
            //MyLogin.Wrong = "01009";
            result = false;
        }
        return result;
    }

    public static void rw(this System.Web.UI.UserControl page, string value) {
        page.Response.Write(value + "\n");
    }
    public static void drw(this System.Web.UI.UserControl page, string value) {
        page.Response.Write(value + "<br/>\n");
    }
    public static void drwsql(this System.Web.UI.UserControl page, string value) {
        page.Response.Write("<pre>" + value + "</pre>\n");
    }

    public static string GetRequest(this System.Web.UI.UserControl page, string key) {

        if (page.Request[key] == null)
            return string.Empty;
        else
            return page.Request[key].ToString().Trim();
    }
    public static string GetRequest(this System.Web.HttpContext page, string key) {

        if (page.Request[key] == null)
            return string.Empty;
        else
            return page.Request[key].ToString().Trim();
    }
    public static string GetSession(this System.Web.UI.UserControl page, string key) {
        try {
            if (page.Session[key] == null)
                return "";
            else
                return page.Session[key].ToString().Trim();
        }
        catch {
            return string.Empty;
        }
    }
    public static string GetSession(this System.Web.HttpContext page, string key) {
        try {
            if (page.Session[key] == null)
                return "";
            else
                return page.Session[key].ToString().Trim();
        }
        catch {
            return string.Empty;
        }
    }
    public static void rw(this System.Web.HttpContext page, string value) {
        page.Response.Write(value + "\n");
    }
    public static void rwln(this System.Web.HttpContext page, string value) {
        page.Response.Write(value + "\n");
    }
    public static void drw(this System.Web.HttpContext page, string value) {
        page.Response.Write(value + "<br/>\n");
    }
    public static void drwsql(this System.Web.HttpContext page, string value) {
        page.Response.Write("<pre>" + value + "</pre>\n");
    }



    public static string genSearchConditionForm(this System.Web.UI.Page html, HttpRequest Request, string pageSize) {
        bool havePageSize = false;
        bool havePageIndex = false;
        StringBuilder sb = new StringBuilder();
        foreach (string item in Request.QueryString) {
            havePageSize |= ("PAGESIZE" == item.ToUpper());
            havePageIndex |= ("PAGEINDEX" == item.ToUpper());
            sb.AppendLine("<input type=\"hidden\" name=\"" + item + "\" value=\"" + Request.QueryString[item] + "\" >");
        }
        foreach (string item in Request.Form) {
            havePageSize |= ("PAGESIZE" == item.ToUpper());
            havePageIndex |= ("PAGEINDEX" == item.ToUpper());
            sb.AppendLine("<input type=\"hidden\" name=\"" + item + "\" value=\"" + Request.Form[item] + "\" >");
        }
        if (!havePageSize) {
            sb.AppendLine("<input type=\"hidden\" name=\"pageSize\" value=\"" + pageSize + "\" >");
        }
        if (!havePageIndex) {
            sb.AppendLine("<input type=\"hidden\" name=\"pageIndex\" value=\"1\" >");
        }
        return sb.ToString();
    }

}