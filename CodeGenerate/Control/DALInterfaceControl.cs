using System.Text;
using System.Windows.Forms;
using CodeGenerate.Abstract;

namespace CodeGenerate.Control
{
    public class DALInterfaceControl : DbOperateBase
    {
        public DALInterfaceControl(string tableName, string preNS, string preModel, CheckedListBox checkedListBox1)
        {
            this.TableName = tableName;
            this.PreNs = preNS;
            this.PreModel = preModel;
            this.CheckedListBox1 = checkedListBox1;
            this.SbTemp = new StringBuilder();
        }

        /// <summary>
        /// Generate IDAL
        /// </summary>
        public override StringBuilder Generate()
        {
            concatNamespace();

            concatGet();

            concatInsert();

            concatUpdate();

            concatDelete();

            SbTemp.Append("\r\n}");
            SbTemp.Append("\r\n}");

            return SbTemp;
        }

        /// <summary>
        /// Generate NameSpace Code
        /// </summary>
        protected override void concatNamespace()
        {
            //Cite NameSpace
            SbTemp.Append("using System;");
            SbTemp.Append("\r\n");
            SbTemp.Append("using System.Data;");
            SbTemp.Append("\r\n");
            SbTemp.Append("using System.Collections.Generic;");


            SbTemp.Append("\r\n");
            SbTemp.Append("\r\n").Append("namespace ").Append(PreNs);
            SbTemp.Append("\r\n").Append("{");
            SbTemp.Append("\r\n").Append("/// <summary>");
            SbTemp.Append("\r\n").Append("///").Append("Data Access Interface");
            SbTemp.Append("\r\n").Append("/// </summary>");
            SbTemp.Append("\r\n").Append("public interface I").Append(TableName);
            SbTemp.Append("\r\n").Append("{");
        }

        /// <summary>
        /// Generate Query Code
        /// </summary>
        protected override void concatGet()
        {
            if (CheckedListBox1.CheckedItems.Contains("GetList()")
                || CheckedListBox1.CheckedItems.Contains("GetModel()")
                || CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                SbTemp.Append("\r\n#region Query");
            }

            #region GetList

            if (CheckedListBox1.CheckedItems.Contains("GetList()"))
            {
                //GetList
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///GetList(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n DataTable GetList({0} model);", PreModel + TableName);
            }

            #endregion

            #region GetModel

            if (CheckedListBox1.CheckedItems.Contains("GetModel()"))
            {
                //GetModel
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///GetModel(exception or empty data when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n {0} GetModel({1} model);", PreModel + TableName, PreModel + TableName);
            }

            #endregion

            #region GetModelList

            if (CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                //GetModelList
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///GetModelList(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n List<{0}> GetModelList({1} model);", PreModel + TableName,
                                    PreModel + TableName);
            }

            #endregion

            if (CheckedListBox1.CheckedItems.Contains("GetList()")
                || CheckedListBox1.CheckedItems.Contains("GetModel()")
                || CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                SbTemp.Append("\r\n#endregion");
            }

            SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Insert Code
        /// </summary>
        protected override void concatInsert()
        {
            string colName;
            if (CheckedListBox1.CheckedItems.Contains("Insert()")
                || CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                SbTemp.Append("\r\n#region Insert");
            }

            #region Insert

            if (CheckedListBox1.CheckedItems.Contains("Insert()"))
            {
                //Insert Item
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///Insert Item(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? Insert({0} model);", PreModel + TableName);
            }

            #endregion

            #region InsertList

            if (CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///InsertList(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? InsertList(List<{0}> listModel);", PreModel + TableName);
            }

            #endregion

            if (CheckedListBox1.CheckedItems.Contains("Insert()")
                || CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                SbTemp.Append("\r\n#endregion");
            }
            SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Update Code
        /// </summary>
        protected override void concatUpdate()
        {
            if (CheckedListBox1.CheckedItems.Contains("Update()") ||
                CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                SbTemp.Append("\r\n#region Update");
            }

            #region Update

            if (CheckedListBox1.CheckedItems.Contains("Update()"))
            {
                //UpdateItem
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///UpdateItem(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? Update({0} model);", PreModel + TableName);
            }

            #endregion

            #region UpdateList

            if (CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                //UpdateList
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///UpdateItemList(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? UpdateList(List<{0}> listModel);", PreModel + TableName);
            }

            #endregion

            if (CheckedListBox1.CheckedItems.Contains("Update()") ||
                CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                SbTemp.Append("\r\n#endregion");
            }

            SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Delete Code
        /// </summary>
        protected override void concatDelete()
        {
            string colName;
            if (CheckedListBox1.CheckedItems.Contains("Delete()")
                || CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                SbTemp.Append("\r\n#region Delete");
            }

            #region Delete 

            if (CheckedListBox1.CheckedItems.Contains("Delete()"))
            {
                //Delete Item
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///Delete Item(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? Delete({0} model);", PreModel + TableName);
            }

            #endregion

            #region DeleteList

            if (CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                //DeleteList
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///Delete Items(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? DeleteList(List<{0}> listModel);", PreModel + TableName);
            }

            #endregion

            #region Delete By String Array（Format '1','2','3')[DeletebyKeys]

            if (CheckedListBox1.CheckedItems.Contains("DeletebyKeys()"))
            {
                SbTemp.Append("\r\n");
                SbTemp.Append("\r\n///<summary>");
                SbTemp.Append("\r\n///Delete By String Array（Format'1','2','3')(exception when return null)");
                SbTemp.Append("\r\n///</summary>");
                SbTemp.AppendFormat("\r\n int? DeletebyKeys(string keys);", TableName);
            }

            #endregion

            if (CheckedListBox1.CheckedItems.Contains("Delete()")
                || CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                SbTemp.Append("\r\n#endregion");
            }
            SbTemp.Append("\r\n");
        }

    }
}