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
    public class SqlHelper : IDisposable {
        private SqlConnection DBConnection;//对非托管资源的连接

        public SqlHelper(string ConnString) {
            DBConnection = new SqlConnection(ConnString);
            DBConnection.Open();
        }

        public SqlConnection Connection {
            get {
                return DBConnection;
            }
        }
        public DataTable GetDataTableBySql(string QueryString) {
            DataTable dt = new DataTable();
            using (SqlDataAdapter DBAdapter = new SqlDataAdapter(QueryString, DBConnection)) {
                DBAdapter.Fill(dt);
                return dt;
            }
        }
        [Obsolete("应改用新的意义更明确的名字GetDataTableBySql")]
        public DataTable GetInformationBySql(string QueryString) {
            return GetDataTableBySql(QueryString);
        }
        [Obsolete("应改用新的意义更明确的名字GetDataTableByCmd")]
        public DataTable GetInformation(SqlCommand cmd) {
            return GetDataTableByCmd(cmd);
        }

        public DataTable GetDataTableByCmd(SqlCommand cmd) {
            DataTable dt = new DataTable();
            using (SqlDataAdapter DBAdapter = new SqlDataAdapter(cmd)) {
                DBAdapter.Fill(dt);
                return dt;
            }
        }
        public SqlDataReader GetReaderBySql(string QueryString) {
            SqlCommand cmd = new SqlCommand(QueryString);
            return GetReaderByCmd(cmd);
        }
        public SqlCommand GetCmd() {
            return new SqlCommand() { Connection = DBConnection };
        }
        public SqlParameter GetParameter(string parameterName, SqlDbType dbType, Object value) {
            SqlParameter result = new SqlParameter(parameterName, dbType);
            result.Value = value;
            return result;
        }
        public SqlParameter GetParameter(string parameterName, SqlDbType dbType,int size, Object value) {
            SqlParameter result = new SqlParameter(parameterName, dbType,size);
            result.Value = value;
            return result;
        }
        public SqlDataReader GetReaderByCmd(SqlCommand cmd) {
            cmd.Connection = DBConnection;
            return cmd.ExecuteReader();
        }

        [Obsolete("应改用新的意义更明确的名字GetDataSetBySql")]
        public DataSet GetInformationBySqlToDataSet(string QueryString) {
            return GetDataSetBySql(QueryString);
        }

        public DataSet GetDataSetBySql(string QueryString) {
            DataSet ds = new DataSet();
            using (SqlDataAdapter DBAdapter = new SqlDataAdapter(QueryString, DBConnection)) {
                DBAdapter.Fill(ds);

                return ds;
            }
        }



        public int ExecuteNonQuery(SqlParameter[] parammeters, string sqlStr) {
            if (parammeters == null) {
                throw new ArgumentNullException("parammeters");
            }
            if (string.IsNullOrEmpty(sqlStr)) {
                throw new ArgumentNullException("sqlStr");
            }

            using (SqlCommand cmd = new SqlCommand(sqlStr, DBConnection)) {
                if (parammeters.Length > 0) {
                    cmd.Parameters.AddRange(parammeters);
                }

                return cmd.ExecuteNonQuery();
            }
        }


        public int ExecuteNonQuery(string sqlStr) {
            if (string.IsNullOrEmpty(sqlStr)) {
                throw new ArgumentNullException("sqlStr");
            }
            try {
                SqlCommand cmd = new SqlCommand(sqlStr, DBConnection);
                return cmd.ExecuteNonQuery();
            }
            catch {
                return 0;
            }
        }

        [Obsolete("应改用新的意义更明确的名字GetScalarBySql")]
        public object ExecuteScalar(string sqlStr) {
            return GetScalarBySql(sqlStr);
        }
        public object GetScalarBySql(string QueryString) {
            using (SqlCommand cmd = Connection.CreateCommand()) {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = QueryString;
                return cmd.ExecuteScalar();
            }
        }
        public object ExecuteScalar(SqlParameter[] parammeters, string sqlStr) {
            if (parammeters == null) {
                throw new ArgumentNullException("parammeters");
            }
            if (string.IsNullOrEmpty(sqlStr)) {
                throw new ArgumentNullException("sqlStr");
            }

            using (SqlCommand cmd = new SqlCommand(sqlStr, DBConnection)) {
                if (parammeters.Length > 0) {
                    cmd.Parameters.AddRange(parammeters);
                }

                return cmd.ExecuteScalar();
            }
        }
        public static string cmdToString(SqlCommand cmd) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(cmd.CommandText);
            foreach (SqlParameter item in cmd.Parameters) {
                sb.AppendLine(String.Format("参数{0}={1}", item.ParameterName, item.Value.ToString()));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="parammeters"></param>
        /// <param name="insertStr"></param>
        /// <returns></returns>
        public bool InsertDataByParameter(SqlParameter[] parammeters, string insertStr) {
            try {
                SqlCommand cmd = new SqlCommand("", DBConnection);
                cmd.Parameters.AddRange(parammeters);
                cmd.CommandText = insertStr;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="parammeters"></param>
        /// <param name="insertStr"></param>
        /// <returns></returns>
        public bool UpdateDataByParameter(SqlParameter[] parammeters, string updStr) {
            try {
                SqlCommand cmd = new SqlCommand(updStr, DBConnection);
                cmd.Parameters.AddRange(parammeters);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }
        }

        #region IDisposable 成员

        private void Dispose(bool disposing) {
            if (DBConnection.State == ConnectionState.Open) {
                DBConnection.Close();
            }
            if (disposing)
                return;
        }

        public void Dispose() {
            Dispose(true);
        }
        #endregion
    }


    /*
    public static class SqlExternal {
        public static string toString(this SqlCommand cmd) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(cmd.CommandText);
            foreach (SqlParameter item in cmd.Parameters) {
                sb.AppendLine(String.Format("参数{0}={1}", item.ParameterName, item.Value.ToString()));
            }
            return sb.ToString();
        }

        public static DateTime GetDateTime(this DbDataReader dr, string name, DateTime defaultValue = default(DateTime)) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetDateTime(ordinal);
            }
        }
        public static string GetString(this DbDataReader dr, string name, string defaultValue = "") {
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
        public static int GetInt32(this DbDataReader dr, string name, int defaultValue = 0) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return dr.GetInt32(ordinal);
            }
        }
        public static bool GetBoolean(this DbDataReader dr, string name, bool defaultValue = false) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return Convert.ToBoolean(dr[ordinal]);
                //dr.GetBoolean(ordinal);
            }
        }
        public static float GetFloat(this DbDataReader dr, string name, float defaultValue = 0) {
            int ordinal = dr.GetOrdinal(name);
            if (dr.IsDBNull(ordinal)) {
                return defaultValue;
            }
            else {
                return (float)dr.GetDouble(ordinal);
            }
        }
    }

    */
}