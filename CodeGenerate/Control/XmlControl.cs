using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace CodeGenerate.Control
{
    class XmlControl
    {
        //XML  Path
        private static readonly string xmlPath = Application.StartupPath + "\\Xml\\Corresponding.xml";

        public static void CreateCorrespondingXmlWhenNotExist()
        {
            if (!File.Exists(xmlPath))
            {
                var dtTableName = new DataTable();
                dtTableName.Columns.Add("OriName");
                dtTableName.Columns.Add("PascalName");
                dtTableName.TableName = "dt";
                dtTableName.Rows.Add(new[] { "1", "1" });
                dtTableName.WriteXml(xmlPath);
            }
        }

        /// <summary>
        /// Read Xml,return DataSet
        /// </summary>
        /// <returns></returns>
        public static DataSet ReadXmlDs()
        {

            var ds = new DataSet();
            ds.ReadXml(xmlPath);
            return ds;
        }

        /// <summary>
        /// Read Xml,return KeyValue Pair
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXml()
        {
            var dsXml = new DataSet();
            dsXml.ReadXml(xmlPath);
            var dic = new Dictionary<string, string>();
            for (int i = 0; i < dsXml.Tables[0].Rows.Count; i++)
            {
                dic.Add(dsXml.Tables[0].Rows[i]["OriName"].ToString(),
                        dsXml.Tables[0].Rows[i]["PascalName"].ToString());
            }
            return dic;
        }

        /// <summary>
        /// WriteXml
        /// </summary>
        /// <param name="dgvTables"></param>
        /// <param name="dgvColumns"></param>
        public static void WriteXml(DataGridView dgvTables, DataGridView dgvColumns)
        {
            DataSet dsxml = ReadXmlDs();

            #region Table Relation

            var dtTables = dgvTables.DataSource as DataTable;
            for (int i = 0; i < dtTables.Rows.Count; i++)
            {
                string tableName = dtTables.Rows[i]["TableName"].ToString().Trim();
                string pascalName = dtTables.Rows[i]["PascalName"].ToString().Trim();
                DataRow[] drs = dsxml.Tables[0].Select(" OriName='" + tableName + "'");
                //Add Relation
                if (drs.Length == 0)
                {
                    DataRow dr = dsxml.Tables[0].NewRow();
                    dr["OriName"] = tableName;
                    dr["PascalName"] = pascalName;
                    dsxml.Tables[0].Rows.Add(dr);
                }
                else //Update Relation
                {
                    drs[0]["PascalName"] = pascalName;
                }
            }

            #endregion

            #region Column Relation

            var dtColumns = dgvColumns.DataSource as DataTable;
            for (int i = 0; i < dtColumns.Rows.Count; i++)
            {
                string columnName = dtColumns.Rows[i]["Column_Name"].ToString().Trim();
                string pascalName = dtColumns.Rows[i]["PascalName"].ToString().Trim();
                DataRow[] drs = dsxml.Tables[0].Select(" OriName='" + columnName + "'");
                //Add Relation
                if (drs.Length == 0)
                {
                    DataRow dr = dsxml.Tables[0].NewRow();
                    dr["OriName"] = columnName;
                    dr["PascalName"] = pascalName;
                    dsxml.Tables[0].Rows.Add(dr);
                }
                else //Update Relation
                {
                    drs[0]["PascalName"] = pascalName;
                }
            }

            #endregion

            for (int i = 0; i < dsxml.Tables[0].Rows.Count; i++)
            {
                if (dsxml.Tables[0].Rows[i]["PascalName"].ToString().Trim() == string.Empty)
                {
                    dsxml.Tables[0].Rows[i].Delete();
                }
            }
            dsxml.AcceptChanges();
            dsxml.WriteXml(xmlPath);
        }
    }
}
