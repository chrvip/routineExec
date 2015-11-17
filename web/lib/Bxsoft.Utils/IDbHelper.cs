using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
namespace Bxsoft.Utils {
    /// <summary>
    ///SqlHelper 的摘要说明
    ///该类主要用于数据库操作,返回对应的数值
    /// </summary>
    public interface IDblHelper : IDisposable {

        SqlConnection Connection {
            get;
        }
        DataTable GetDataTableBySql(string QueryString);

        [Obsolete("应改用新的意义更明确的名字GetDataTableBySql")]
        DataTable GetInformationBySql(string QueryString);
        [Obsolete("应改用新的意义更明确的名字GetDataTableByCmd")]
        DataTable GetInformation(DbCommand cmd);

        DataTable GetDataTableByCmd(DbCommand cmd);
        DbDataReader GetReaderBySql(string QueryString);
        DbCommand GetCmd();
        DbParameter GetParameter(string parameterName, SqlDbType dbType, Object value);
        DbDataReader GetReaderByCmd(DbCommand cmd);

        [Obsolete("应改用新的意义更明确的名字GetDataSetBySql")]
        DataSet GetInformationBySqlToDataSet(string QueryString);

        DataSet GetDataSetBySql(string QueryString);



        int ExecuteNonQuery(DbParameter[] parammeters, string sqlStr);


        int ExecuteNonQuery(string sqlStr);

        [Obsolete("应改用新的意义更明确的名字GetScalarBySql")]
        object ExecuteScalar(string sqlStr);
        object GetScalarBySql(string QueryString);
        object ExecuteScalar(SqlParameter[] parammeters, string sqlStr);
        string cmdToString(DbCommand cmd);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="parammeters"></param>
        /// <param name="insertStr"></param>
        /// <returns></returns>
        bool InsertDataByParameter(DbParameter[] parammeters, string insertStr);


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="parammeters"></param>
        /// <param name="insertStr"></param>
        /// <returns></returns>
        bool UpdateDataByParameter(DbParameter[] parammeters, string updStr);
    }


    public static class DbExternal {
        public static string toString(this DbCommand cmd) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(cmd.CommandText);
            foreach (DbParameter item in cmd.Parameters) {
                sb.AppendLine(String.Format("参数{0}={1}", item.ParameterName, item.Value.ToString()));
            }
            return sb.ToString();
        }

        public static DateTime GetDateTime(this DbDataReader dr, string name, DateTime defaultValue=default(DateTime)) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetDateTime(ordinal);
            }
        }
        public static string GetString(this DbDataReader dr, string name, string defaultValue="") {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetString(ordinal);
            }
        }
        public static string GetGuidString(this DbDataReader dr, string name, string defaultValue = "") {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetGuid(ordinal).ToString();
            }
        }
        public static int GetInt32(this DbDataReader dr, string name, int defaultValue=0) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetInt32(ordinal);
            }
        }
        public static bool GetBoolean(this DbDataReader dr, string name, bool defaultValue=false) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return Convert.ToBoolean(dr[ordinal]);
                //dr.GetBoolean(ordinal);
            }
        }
        public static float GetFloat(this DbDataReader dr, string name, float defaultValue=0) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return (float)dr.GetDouble(ordinal);
            }
        }
    }
}