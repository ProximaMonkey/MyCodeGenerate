using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;

namespace CodeGenerate.Util
{
    public class ReflectionUtil
    {
        #region Convert DataRow To Object
        /// <summary>
        /// Convert DataRow To Object
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="dataTable">dataTable</param>
        /// <param name="rowIndex">rowIndex</param>
        public static void ConvertDataRowToModel(object obj, DataTable dataTable, int rowIndex)
        {
            if (dataTable.Rows.Count < (rowIndex + 1))
            {
                throw new Exception("directed row is not exist!");
            }

            if (dataTable.Columns.Count < 1)
            {
                throw new Exception("DataColumns is empty!");
            }

            Type type = obj.GetType();
            PropertyInfo[] pInfos = type.GetProperties();

            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    for (int j = 0; j < pInfos.Length; j++)
                    {
                        //convert to lower is prevent columnName in database is not inconsistently to properties(Case)
                        if (dataTable.Columns[i].ColumnName.ToLower() == pInfos[j].Name.ToLower())
                        {
                            PropertyInfo pInfo = type.GetProperty(pInfos[j].Name);  //property in object

                            object colValue = dataTable.Rows[rowIndex][i];

                            #region assign column value to object property
                            if (!objectIsNull(colValue))
                            {
                                if (pInfos[j].PropertyType.FullName == "System.String")
                                {
                                    pInfo.SetValue(obj, Convert.ToString(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Int32")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt32(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Int64")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt64(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Single")
                                {
                                    pInfo.SetValue(obj, Convert.ToSingle(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Double")
                                {
                                    pInfo.SetValue(obj, Convert.ToDouble(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Decimal")
                                {
                                    pInfo.SetValue(obj, Convert.ToDecimal(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Char")
                                {
                                    pInfo.SetValue(obj, Convert.ToChar(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Boolean")
                                {
                                    pInfo.SetValue(obj, Convert.ToBoolean(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.DateTime")
                                {
                                    pInfo.SetValue(obj, Convert.ToDateTime(colValue), null);
                                }
                                //Nullable Type
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToDateTime(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToDateTime(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt32(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt32(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Int64, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt64(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToInt64(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Decimal, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToDecimal(colValue), null);
                                }
                                else if (pInfos[j].PropertyType.FullName == "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]")
                                {
                                    pInfo.SetValue(obj, Convert.ToDecimal(colValue), null);
                                }
                                else
                                {
                                    throw new Exception("Data Type is not Support!");
                                }
                            }
                            else
                            {
                                pInfo.SetValue(obj, null, null);
                            }
                            #endregion

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Convert Object To New DataRow
        /// <summary>
        /// Convert Object To New DataRow
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="dataTable">dataTable</param>
        /// <param name="rowIndex">rowIndex</param>
        public static void ConvertModelToNewDataRow(object obj, DataTable dataTable, int rowIndex)
        {
            //DataColumns is empty
            if (dataTable.Columns.Count < 1)
            {
                throw new Exception("DataColumns is empty!");
            }

            DataRow dr = dataTable.NewRow();
            Type type = obj.GetType();
            PropertyInfo[] pInfos = type.GetProperties();

            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    for (int j = 0; j < pInfos.Length; j++)
                    {
                        //convert to lower is prevent columnName in database is not inconsistently to properties(Case)
                        if (dataTable.Columns[i].ColumnName.ToLower() == pInfos[j].Name.ToLower())
                        {
                            PropertyInfo pInfo = type.GetProperty(pInfos[j].Name);

                            object beanValue = pInfo.GetValue(obj, null);

                            //assign object property value to datarow column
                            if (!objectIsNull(beanValue))
                            {
                                dr[i] = beanValue;
                            }
                            else
                            {
                                dr[i] = DBNull.Value;
                            }
                            break;
                        }
                    }
                }

                dataTable.Rows.InsertAt(dr, rowIndex);
                dataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Convert Object To Directed DataRow
        /// <summary>
        /// Convert Object To Directed DataRow
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="dataTable">dataTable</param>
        /// <param name="rowIndex">rowIndex</param>
        public static void ConvertModelToSpecDataRow(object obj, DataTable dataTable, int rowIndex)
        {
            //Directed DataRow is not exist
            if (dataTable.Rows.Count < (rowIndex + 1))
            {
                throw new Exception("Directed DataRow is not exist!");
            }

            //DataColumns is empty
            if (dataTable.Columns.Count < 1)
            {
                throw new Exception("DataColumns is empty!");
            }

            Type type = obj.GetType();
            PropertyInfo[] pInfos = type.GetProperties();

            try
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    for (int j = 0; j < pInfos.Length; j++)
                    {
                        //convert to lower is prevent columnName in database is not inconsistently to properties(Case)
                        if (dataTable.Columns[i].ColumnName.ToLower() == pInfos[j].Name.ToLower())
                        {
                            PropertyInfo pInfo = type.GetProperty(pInfos[j].Name);
                            object beanValue = pInfo.GetValue(obj, null);

                            //assign object property value to datarow column
                            if (!objectIsNull(beanValue))
                            {
                                dataTable.Rows[rowIndex][i] = beanValue;
                            }
                            else
                            {
                                dataTable.Rows[rowIndex][i] = DBNull.Value;
                            }
                            break;
                        }
                    }
                }
                dataTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region get class object by object name
        /// <summary>
        /// get class object by object name
        /// </summary>
        /// <param name="parObjectName">object name</param>
        /// <returns>class object</returns>
        public static object GetObjectByObjectName(string parObjectName)
        {
            Type t = Type.GetType(parObjectName); 
            return System.Activator.CreateInstance(t);         
        }
        #endregion

        #region judge object is null
        /// <summary>
        /// judge object is null
        /// </summary>
        /// <param name="obj">对象objectparam>
        /// <returns></returns>
        static private bool objectIsNull(Object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString().Equals("") || obj.ToString() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
