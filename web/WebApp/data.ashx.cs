using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using Bxsoft.Utils;

namespace WebApp {
    public class Routine {
        public int id { get; set; }
        public string name { get; set; }
        public int showOrder { get; set; }
        public float duration { get; set; }
    }
    public class RoutineItem {
        public int id { get; set; }
        public int listId { get; set; }
        public string name { get; set; }
        public float duration { get; set; }
        public int showOrder { get; set; }

    }
    /// <summary>
    /// list 的摘要说明
    /// </summary>
    public class data : IHttpHandler {


        HttpResponse Response = null;
        HttpContext context = null;
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("appId", WebInfo.GetInfo("appId"));
            Response = context.Response;
            this.context = context;
            string act = context.GetRequest("act");
            if ("lists" == act) {
                getLists();
                context.Response.End();
                return;
            }
            if ("list" == act) {
                getList();
                context.Response.End();
                return;
            }
            context.Response.End();
            return;
        }

        private void getList() {
            string id = context.GetRequest("id");
            if (String.IsNullOrEmpty(id)) {
                id = "1";
            }
            //context.drw("supmodulename=" + supmodulename);
            //context.drw("modulename=" + modulename);
            //context.drw("submodulename=" + submodulename);

            string sql = @"select * from tbListItem where listId=@listId order by ShowOrder";
            //context.drwsql(sql);
            using (SqlHelper sh = new SqlHelper(WebInfo.GetConnString("DBConnStr"))) {
                IEnumerable<RoutineItem> Result = sh.Connection.Query<RoutineItem>(sql, new { listId = Int32.Parse(id) });
                Response.Write(JsonHelper.toJson(Result));
            }
        }

        private void getLists() {
            string username = context.GetRequest("username");
            if (String.IsNullOrEmpty(username)) {
                username = "chr";
            }
            //context.drw("supmodulename=" + supmodulename);
            //context.drw("modulename=" + modulename);
            //context.drw("submodulename=" + submodulename);

            string sql = "select tbList.id,tbList.name,tbList.showOrder,sum(tbListItem.duration) as duration  from  tbList \n";
            sql += "left join tbListItem on tbList.id=tbListItem.listId \n";
            sql += "where tbList.userName=@username \n";
            sql += "group by tbList.id, tbList.name,tbList.showOrder \n";
            sql += "order by tbList.showOrder";
            //context.drwsql(sql);
            using (SqlHelper sh = new SqlHelper(WebInfo.GetConnString("DBConnStr"))) {
                IEnumerable<Routine> Result = sh.Connection.Query<Routine>(sql, new { username = username });
                Response.Write(JsonHelper.toJson(Result));
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}