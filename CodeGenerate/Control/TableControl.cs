using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CJ.Demo.DBUtility.DBCommon;
using CodeGenerate.Model;
using CodeGenerate.Util;

namespace CodeGenerate.Control
{
    class TableControl
    {
        /// <summary>
        /// bind Table Data
        /// </summary>
        /// <param name="dic"></param>
        public static DataTable GetTables(Dictionary<string, string> dic, string filterValue, EnumDbType dbType)
        {
            string strSql = string.Empty;

            if (dbType == EnumDbType.Oracle)
            {
                strSql =
                    "SELECT ut.table_name TableName ,utc.comments,'' PascalName FROM user_tables ut " +
                    "INNER JOIN user_tab_comments utc ON ut.table_name=utc.table_name";
                if (filterValue != string.Empty)
                {
                    strSql += " WHERE LOWER(ut.table_name) LIKE '" + filterValue + "%'";
                }
                strSql += " ORDER BY ut.table_name";
            }
            else if (dbType == EnumDbType.SqlServer)
            {
                strSql =
                    @"SELECT t.[name] as TableName,ep.[value] comments,'' PascalName FROM sys.tables t
                        LEFT JOIN sys.extended_properties ep
                        ON t.[object_id]=ep.major_id AND ep.minor_id=0";
                if (filterValue != string.Empty)
                {
                    strSql += " WHERE LOWER(ep.[name]) LIKE '" + filterValue + "%'";
                }
            }

            DataTable dt = DBHelper.GetDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dic.ContainsKey(dt.Rows[i]["TableName"].ToString()))
                {
                    dt.Rows[i]["PascalName"] = dic[dt.Rows[i]["TableName"].ToString()];
                }
            }

            return dt;
        }

        /// <summary>
        /// Get Primary Key In Table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetPrimayKeys(string tableName, EnumDbType dbType)
        {
            string sqlstr = string.Empty;

            if (dbType == EnumDbType.Oracle)
            {
                sqlstr =
                     string.Format(
                    @"SELECT * FROM user_cons_columns ucc WHERE UPPER(ucc.table_name)='{0}' AND UPPER(ucc.constraint_name)=CONCAT('PK_','{1}')",
                    tableName, tableName);
            }
            else if (dbType == EnumDbType.SqlServer)
            {
                sqlstr =
                    string.Format(@" SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='{0}'",
                                       tableName);
            }
            DataTable dtPrimaryKeys = DBHelper.GetDataTable(sqlstr);
            var primayKeys = new List<string>();
            foreach (DataRow item in dtPrimaryKeys.Rows)
            {
                string columnName = item["column_name"].ToString();
                if (!primayKeys.Contains(columnName))
                {
                    primayKeys.Add(columnName);
                }
            }
            return primayKeys;
        }

        /// <summary>
        /// Get Checked Table Info
        /// </summary>
        /// <param name="dgvTables"></param>
        /// <returns></returns>
        public static List<Table> GetCheckedTableName(DataGridView dgvTables, bool isPascal)
        {
            var list = new List<Table>();
            Table model;
            for (int i = 0; i < dgvTables.Rows.Count; i++)
            {
                if (dgvTables.Rows[i].Cells["ckbCheck"].Value != null &&
                    (bool)dgvTables.Rows[i].Cells["ckbCheck"].Value)
                {
                    model = new Table();
                    model.TableName = dgvTables.Rows[i].Cells["Table_Name"].Value.ToString();
                    if (isPascal)
                    {  
                        model.TabPascalName = dgvTables.Rows[i].Cells["TabPascalName"].Value.ToString();
                        {
                            if(string.IsNullOrWhiteSpace(model.TabPascalName))
                            {
                                list.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        model.TabPascalName = model.TableName;
                    }

                    model.Comments = dgvTables.Rows[i].Cells["comments"].Value.ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
