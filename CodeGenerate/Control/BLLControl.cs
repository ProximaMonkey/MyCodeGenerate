using System.Text;
using System.Windows.Forms;
using CodeGenerate.Abstract;

namespace CodeGenerate.Control
{
    class BLLControl : DbOperateBase
    {
        protected string PreClass;

        public BLLControl(string tableName, string preNS, string preClass, string preModel, CheckedListBox checkedListBox1)
        {
            this.TableName = tableName;
            this.PreNs = preNS;
            this.PreModel = preModel;
            this.PreClass = preClass;
            this.CheckedListBox1 = checkedListBox1;
            this.SbTemp = new StringBuilder();
        }

        public override StringBuilder Generate()
        {
            concatNamespace();

            this.SbTemp.AppendFormat(" \r\nprivate readonly Dal{0} dal = new Dal{1}();", this.TableName, this.TableName);
            this.SbTemp.Append("\r\n");

            concatGet();

            concatInsert();

            concatUpdate();

            concatDelete();

            this.SbTemp.Append("\r\n}");
            this.SbTemp.Append("\r\n");
            this.SbTemp.Append("}\r\n");

            return this.SbTemp;
        }

        /// <summary>
        /// Generate NameSpace And Class
        /// </summary>
        protected override void concatNamespace()
        {
            this.SbTemp.Append("using System;"); 
            this.SbTemp.Append("\r\nusing System.Data;"); 
            this.SbTemp.Append("\r\nusing System.Collections.Generic;"); 

            this.SbTemp.Append("\r\n");
            this.SbTemp.Append("\r\nnamespace ").Append(this.PreNs); 
            this.SbTemp.Append("\r\n{ ");
            this.SbTemp.Append("\r\n///</summary>");
            this.SbTemp.Append("\r\n///Business Logic Layer ");
            this.SbTemp.Append("\r\n///</summary>");
            this.SbTemp.Append("\r\npublic class ").Append(this.PreClass).Append(this.TableName);
            this.SbTemp.Append("\r\n{");
        }

        /// <summary>
        /// Generate Query Code
        /// </summary>
        protected override void concatGet()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetList()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModel()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                this.SbTemp.Append("\r\n#region Query");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("GetList()"))
            {
                //GetList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetList(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic DataTable GetList({0} model)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.GetList(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("GetModel()"))
            {
                //GetModel
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetModel(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic {0} GetModel({1} model)", this.PreModel + this.TableName, this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.GetModel(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                //GetModelList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetModelList(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic List<{0}> GetModelList({1} model)", this.PreModel + this.TableName,
                                    this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.GetModelList(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("GetList()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModel()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }
            this.SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Insert Code
        /// </summary>
        protected override void concatInsert()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Insert()")
                || this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                this.SbTemp.Append("\r\n#region Insert");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("Insert()"))
            {
                //Insert Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Insert Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Insert({0} model)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.Insert(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                //InsertList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Insert Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? InsertList(List<{0}> listModel)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.InsertList(listModel);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("Insert()")
                || this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }
            this.SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Update Code
        /// </summary>
        protected override void concatUpdate()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Update()") ||
                this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                this.SbTemp.Append("\r\n#region Update");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("Update()"))
            {
                //Update Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Update Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Update({0} model)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.Update(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                //UpdateList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Update Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? UpdateList(List<{0}> listModel)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.UpdateList(listModel);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("Update()") ||
                this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }

            this.SbTemp.Append("\r\n");
        }

        /// <summary>
        /// Generate Delete Code
        /// </summary>
        protected override void concatDelete()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Delete()")
                || this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                this.SbTemp.Append("\r\n#region Delete");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("Delete()"))
            {
                //Delete Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Delete Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Delete({0} model)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.Delete(model);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                //DeleteList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Delete Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? DeleteList(List<{0}> listModel)", this.PreModel + this.TableName);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.DeleteList(listModel);");
                this.SbTemp.Append("\r\n}");
            }

            if (this.CheckedListBox1.CheckedItems.Contains("DeletebyKeys()"))
            {
                //Delete By String Array（Format '1','2','3')[DeletebyKeys]
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Delete By String Array（Format '1','2','3')(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.Append("\r\npublic int? DeletebyKeys(string keys)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn dal.DeletebyKeys(keys);");
                this.SbTemp.Append("\r\n}");
            }
            if (this.CheckedListBox1.CheckedItems.Contains("Delete()")
                || this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }


            this.SbTemp.Append("\r\n");
        }
    }
}
