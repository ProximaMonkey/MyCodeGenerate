using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CJ.Demo.DBUtility.DBCommon;
using CodeGenerate.Util;

namespace CodeGenerate.Control
{
    class ColumnControl
    {

        /// <summary>
        /// Get Table Columns By TableName
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static DataTable GetColumnsPascalByTableName(string tableName, EnumDbType dbType)
        {
            DataTable dtColumns = GetTableColumnsByTableName(tableName, dbType);

            XmlControl.CreateCorrespondingXmlWhenNotExist();
            Dictionary<string, string> dic = XmlControl.ReadXml();

            //Pascal Columns
            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string name = dtColumns.Rows[i]["Column_Name"].ToString();
                if (dic.ContainsKey(name))
                {
                    dtColumns.Rows[i]["PascalName"] = dic[name];
                }
            }
            dtColumns.AcceptChanges();

            return dtColumns;
        }

        public static DataTable GetTableColumnsByTableName(string tableName, EnumDbType dbType)
        {
            string sqlstr = String.Empty;
            if (dbType == EnumDbType.Oracle)
            {
                sqlstr = String.Format(
                    @"SELECT utc.*,ucc.comments,'' PascalName FROM user_tab_columns utc
                            INNER JOIN user_col_comments ucc ON utc.table_name=ucc.table_name AND utc.column_name=ucc.column_name
                            WHERE UPPER(utc.table_name)='{0}'", tableName);
            }
            else if (dbType == EnumDbType.SqlServer)
            {
                sqlstr = String.Format(
                     @"Declare @tblName nvarchar(1000)
                        set @tblName='{0}'
                        declare @TblID int
                        set @TblID=(select [object_id] as tblID  from sys.all_objects where [type] ='U' and [name]<>'dtproperties' and [name]=@tblName) 
                        
                        select syscolumns.name as Column_Name,
                        systypes.name as DATA_TYPE,
                        syscolumns.length as DATA_LENGTH,
                        syscolumns.prec as DATA_PRECISION,  --xprec
                        syscolumns.scale as DATA_SCALE, --xscale
	                    (SELECT   [value] FROM  ::fn_listextendedproperty(NULL, 'user', 'dbo', 'table', object_name(@TblID), 'column', syscolumns.name)  as e where e.name='MS_Description') as comments,
                        '' PascalName
	                        from sysColumns 
	                        left join sysTypes on sysTypes.xtype = sysColumns.xtype and sysTypes.xusertype = sysColumns.xusertype 
	                        left join sysobjects on sysobjects.id = syscolumns.cdefault and sysobjects.type='D' 
	                        left join syscomments on syscomments.id = sysobjects.id 
                        where syscolumns.id=@TblID
                        ORDER BY Column_Name", tableName);

            }
            var dtColumns = DBHelper.GetDataTable(sqlstr);
            return dtColumns;
        }

        /// <summary>
        /// Convert TableColumn to Pascal Form
        /// </summary>
        /// <param name="dtColumns"></param>
        /// <returns>All TableColumn has Pascal Form return true,else return false</returns>
        public static bool GetColRelaPascalName(DataTable dtColumns)
        {
            Dictionary<string, string> dic = XmlControl.ReadXml();

            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string colName = dtColumns.Rows[i]["Column_Name"].ToString();
                if (dic.ContainsKey(colName))
                {
                    dtColumns.Rows[i]["PascalName"] = dic[colName];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static DataTable GetColumnAndPascalNameFromXml(string tableName,EnumDbType dbType)
        {
            DataTable dtColumns = ColumnControl.GetTableColumnsByTableName(tableName, dbType);

            XmlControl.CreateCorrespondingXmlWhenNotExist();

            var dic = XmlControl.ReadXml();
         
            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string name = dtColumns.Rows[i]["Column_Name"].ToString();
                if (dic.ContainsKey(name))
                {
                    dtColumns.Rows[i]["PascalName"] = dic[name];
                }
            }
            dtColumns.AcceptChanges();
            return dtColumns;
        }

    }
}
