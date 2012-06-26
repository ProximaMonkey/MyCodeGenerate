using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeGenerate.Util;
using System.Data;

namespace CodeGenerate.Control
{
    class ModelControl
    {
        #region Fields And Properties
        public string dbTableName;
        public string tableName;
        public string preNS;
        public string preModel;
        public bool isPascal;
        public EnumDbType dbType;
        #endregion

        #region Constructors
        public ModelControl(string dbTableName, string tableName, string preNS, string preModel, bool isPascal, EnumDbType dbType)
        {
            this.dbTableName = dbTableName;
            this.tableName = tableName;
            this.preNS = preNS;
            this.preModel = preModel;
            this.isPascal = isPascal;
            this.dbType = dbType;
        }
        #endregion

        /// <summary>
        /// Generate Domain Class
        /// </summary>
        /// <param name="dbTableName">TableName in DataBase</param>
        /// <param name="tableName">TableName what'll generate</param>
        public StringBuilder Generate()
        {
            var sbTemp = new StringBuilder();

            DataTable dtColumns = ColumnControl.GetTableColumnsByTableName(dbTableName, dbType);

            string colName = string.Empty;

            #region Generate NameSpace And Class

            sbTemp.Append("using System;"); 
            sbTemp.Append("\r\n");
            sbTemp.Append("\r\n").Append("namespace ").Append(preNS); 
            sbTemp.Append("\r\n").Append("{");
            sbTemp.Append("\r\n").Append("/// <summary>");
            sbTemp.Append("\r\n").Append("///").Append("Domain Class");
            sbTemp.Append("\r\n").Append("/// </summary>");
            sbTemp.Append("\r\n").Append("[Serializable]");

            sbTemp.Append("\r\n").Append("public class ").Append(preModel).Append(tableName);
            sbTemp.Append("\r\n").Append("{"); 

            #endregion

            #region Generate Constructors

            sbTemp.Append("\r\n").Append("/// <summary>");
            sbTemp.Append("\r\n").Append("///").Append("Constructor(initial properties null)");
            sbTemp.Append("\r\n").Append("/// </summary>");
            sbTemp.Append("\r\n").Append("public ").Append(preModel).Append(tableName).Append("()");
            sbTemp.Append("\r\n").Append("{");
            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string tempColumnName = dtColumns.Rows[i]["COLUMN_NAME"].ToString();
                if (isPascal)
                {
                    colName = dtColumns.Rows[i]["PascalName"].ToString();
                }
                else
                {
                    colName = tempColumnName;
                }
                string tempType = dtColumns.Rows[i]["DATA_TYPE"].ToString();


                sbTemp.Append("\r\n").Append("this.").Append(colName).Append(" = null").Append(";");
            }
            sbTemp.Append("\r\n}");

            #endregion

            #region Generate fields And properties

            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string tempColumnName = dtColumns.Rows[i]["COLUMN_NAME"].ToString();
                if (isPascal)
                {
                    colName = dtColumns.Rows[i]["PascalName"].ToString();
                }
                else
                {
                    colName = tempColumnName;
                }
                string tempType = dtColumns.Rows[i]["DATA_TYPE"].ToString();
                string tempLength = dtColumns.Rows[i]["DATA_LENGTH"].ToString();
                string tempDescription = dtColumns.Rows[i]["Comments"].ToString();
                string tempPrecision = dtColumns.Rows[i]["DATA_PRECISION"].ToString();
                string tempScale = dtColumns.Rows[i]["DATA_SCALE"].ToString();

                sbTemp.Append("\r\n");
                sbTemp.Append("\r\n/// <summary>");
                sbTemp.Append("\r\n///").Append(tempDescription);
                sbTemp.Append("\r\n/// </summary>");
                if (dbType == EnumDbType.Oracle)
                {
                    sbTemp.Append("\r\npublic ").Append(DataTypeConvertUtil.ConvertTypeOracle(tempType, tempPrecision,
                                                                                      tempScale)).Append(" ").Append(
                                                                                          colName).Append(
                                                                                              " { get; set; }");
                }
                else
                {
                    sbTemp.Append("\r\npublic ").Append(DataTypeConvertUtil.ConvertType2008SqlServer(tempType, tempPrecision,
                                                                                      tempScale)).Append(" ").Append(
                                                                                          colName).Append(
                                                                                              " { get; set; }");
                }
            }
            sbTemp.Append("\r\n}");
            sbTemp.Append("\r\n}");

            #endregion

            return sbTemp;
        }
    }
}
