using System;

namespace CodeGenerate.Util
{
    public class DataTypeConvertUtil
    {
        #region Convert DataBase Field Type To DotNet Framework Type(Oracle)
        /// <summary>
        /// Convert DataBase Field Type To DotNet Framework Type(Oracle)
        /// </summary>
        /// <param name="dataType">DataBase Field Type</param>
        /// <param name="tempLength"> </param>
        /// <param name="tempScale"> </param>
        /// <returns></returns>
        public static string ConvertTypeOracle(string dataType, string tempLength, string tempScale)
        {
            string type = dataType.ToUpper();
            string ret = "string";

            if (type == "NVARCHAR2" || type == "VARCHAR2" || type == "CHAR")
            {
                ret = "string";
            }
            else if (type == "NUMBER" || type == "NUMERIC")
            {
                int length = 11;
                if (tempLength != string.Empty)
                {
                    length = Convert.ToInt32(tempLength);
                }
                int scale = 11;
                if (tempLength != string.Empty)
                {
                    scale = Convert.ToInt32(tempScale);
                }
                if (scale > 0)
                {
                    ret = "decimal?"; 
                }
                else if (length <= 10)
                {
                    ret = "int?"; 
                }
                else
                {
                    ret = "long?";
                }
            }
            else if (type == "DATE")
            {
                ret = "DateTime?";
            }

            return ret;
        }
        #endregion

        #region Convert DataBase Field Type To DotNet Framework Type(SqlServer)
        /// <summary>
        /// Convert DataBase Field Type To DotNet Framework Type(SqlServer)
        /// </summary>
        /// <param name="dbtype">DataBase Field Type</param>
        /// <param name="tempLength"> </param>
        /// <param name="tempScale"> </param>
        /// <returns></returns>
        public static string ConvertType2008SqlServer(string dbtype, string tempLength, string tempScale)
        {
            string str = string.Empty;
            string type = dbtype.ToUpper();
            string ret = "string";

            if (type == "NVARCHAR" || type == "VARCHAR" || type == "CHAR" || type == "NTEXT")
            {
                ret = "string";
            }
            else if (type == "INT")
            {
                int length = Convert.ToInt32(tempLength);
                int scale = Convert.ToInt32(tempScale);
                if (scale > 0)
                {
                    ret = "decimal?"; 
                }
                else if (length <= 10)
                {
                    ret = "int?"; 
                }
                else
                {
                    ret = "long?";
                }
            }
            else if (type == "IMAGE")
            {
                ret = "byte[]";
            }
            else if (type == "DATETIME")
            {
                ret = "DateTime?";
            }

            return ret;
        }
        #endregion
    }
}