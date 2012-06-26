using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CodeGenerate.Abstract;
using CodeGenerate.Util;
using System.Windows.Forms;

namespace CodeGenerate.Control
{
    public class DALControl : DbOperateBase
    {
        #region Fields and Properties
        private string colName;
        private const string commentsConcatVar = " //Concat Variable";
        private const string commentsSql = " //SQL";
        private const string commentsVar = " //Parameter Variable";
        private const string dot = ".";

        /// <summary>
        /// DbHelper DataBase Type
        /// </summary>
        private string dbHelper;
        /// <summary>
        /// parameter DataBase Type 
        /// </summary>
        private string parameter;
        /// <summary>
        /// parmsHelper DataBase Type 
        /// </summary>
        private string paramsHelper;

        private readonly DataTable dtColumns = null;
        private List<string> listPrimaryKey;

        protected string PreClass;
        protected string PascalTableName;

        /// <summary>
        /// Is Generate In Pascal Form
        /// </summary>
        protected bool IsPascal;
        protected bool IsLog;
        protected EnumDbType DbType;
        #endregion

        #region Constructors
        public DALControl(string dbTableName, string PascalTableName, bool isPascal, string preNS,
                            string preClass, string preModel,
                            bool isLog, CheckedListBox checkedListBox1, EnumDbType dbType)
        {
            this.TableName = dbTableName;
            this.PascalTableName = PascalTableName;
            this.IsPascal = isPascal;
            this.PreNs = preNS;
            this.SbTemp = new StringBuilder();

            this.PreClass = preClass;
            this.PreModel = preModel;
            this.IsLog = isLog;
            this.CheckedListBox1 = checkedListBox1;
            this.DbType = dbType;

            if (this.DbType == EnumDbType.Oracle)
            {
                dbHelper = "DbHelperOracle";
                parameter = "OracleParameter";
                paramsHelper = "ParamsHelperOracle";
            }
            else if (this.DbType == EnumDbType.SqlServer)
            {
                dbHelper = "DbHelperSqlServer";
                parameter = "SqlParameter";
                paramsHelper = "ParamsHelperSqlServer";
            }

            listPrimaryKey = TableControl.GetPrimayKeys(dbTableName, this.DbType);
            dtColumns = ColumnControl.GetTableColumnsByTableName(dbTableName, this.DbType);
        }
        #endregion

        /// <summary>
        /// Generate Method
        /// </summary>
        public override StringBuilder Generate()
        {
            concatNamespace();

            concatGet();

            concatInsert();

            concatUpdate();

            concatDelete();

            concatLog();

            this.SbTemp.Append("\r\n}");
            this.SbTemp.Append("\r\n}");

            return this.SbTemp;
        }

        #region Generate NameSpace And Class
        /// <summary>
        /// NameSpace
        /// </summary>
        protected override void concatNamespace()
        {
            this.SbTemp.Append("using System;");
            this.SbTemp.Append("\r\nusing System.Data;");
            this.SbTemp.Append("\r\nusing System.Collections.Generic;");

            this.SbTemp.Append("\r\nusing System.Text;");

            this.SbTemp.Append("\r\n");
            this.SbTemp.Append("\r\n").Append("namespace ").Append(this.PreNs);
            this.SbTemp.Append("\r\n").Append("{");
            this.SbTemp.Append("\r\n").Append("/// <summary>");
            this.SbTemp.Append("\r\n").Append("///").Append("Data Access Layer");
            this.SbTemp.Append("\r\n").Append("/// </summary>");
            this.SbTemp.Append("\r\n").Append("public class ").Append(this.PreClass).Append(PascalTableName);
            this.SbTemp.Append("\r\n").Append("{");
        }
        #endregion

        #region Generate Query Code
        /// <summary>
        /// Query
        /// </summary>
        protected override void concatGet()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetList()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModel()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                this.SbTemp.Append("\r\n#region Query");
            }

            GetList();
            GetModel();
            GetModelList();
            getParam();

            if (this.CheckedListBox1.CheckedItems.Contains("GetList()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModel()")
                || this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }

            this.SbTemp.Append("\r\n");
        }

        protected void GetList()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetList()"))
            {
                //GetList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetList(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic DataTable GetList({0} model)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\ngetParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nDataTable returnValue=null;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.GetDataTable(sqlstr, listParam);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "GetList", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        protected void GetModel()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetModel()"))
            {
                //GetModel
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetModel(exception or empty data when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic {0} GetModel({1} model)", this.PreModel + PascalTableName, this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\ngetParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nDataTable dt;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\ndt = dbHelper.GetDataTable(sqlstr, listParam);");
                this.SbTemp.Append("\r\nif(dt.Rows.Count>0)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nReflectionUtil.ConvertDataRowToModel(model, dt, 0);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nelse");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nmodel=null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "GetModel", this.IsLog);
                this.SbTemp.Append("\r\nmodel = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn model;");
                this.SbTemp.Append("\r\n}");
            }
        }

        protected void GetModelList()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                //GetModelList
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///GetModelList(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic List<{0}> GetModelList({1} model)", this.PreModel + PascalTableName,
                                    this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\ngetParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.AppendFormat("\r\nvar listModel = new List<{0}>();", this.PreModel + PascalTableName);
                this.SbTemp.AppendFormat("\r\n{0} tempModel=null;", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nDataTable dt = dbHelper.GetDataTable(sqlstr, listParam);");
                this.SbTemp.Append("\r\nfor(int i = 0; i < dt.Rows.Count; i++)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\ntempModel=new {0}();", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\nReflectionUtil.ConvertDataRowToModel(tempModel, dt, i); //Convert DataRow To Model");
                this.SbTemp.Append("\r\nlistModel.Add(tempModel);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "GetModelList", this.IsLog);
                this.SbTemp.Append("\r\nlistModel = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn listModel;");
                this.SbTemp.Append("\r\n}");
            }
        }

        protected void getParam()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("GetList()")
            || this.CheckedListBox1.CheckedItems.Contains("GetModel()")
            || this.CheckedListBox1.CheckedItems.Contains("GetModelList()"))
            {
                string simpleForTableName = PascalTableName.Substring(0, 1).ToLower();

                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Concat Parameter");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat(
                    "\r\nprivate void getParam({0} model, ref string sqlstr, ref  List<{1}> listParam)",
                    this.PreModel + PascalTableName, parameter);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("if (sqlstr == string.Empty)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nsqlstr = \"SELECT * FROM {0} {1}\";", PascalTableName, simpleForTableName);
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nvar sbWhere = new StringBuilder();");
                this.SbTemp.AppendFormat("\r\nvar param = new {0}();", paramsHelper).Append(commentsConcatVar);

                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n#region Concat Parameter");
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    colName = this.IsPascal ? dtColumns.Rows[i]["PascalName"].ToString() : dtColumns.Rows[i]["Column_Name"].ToString();

                    string simpleDotColName = simpleForTableName + dot + colName;

                    string comments = dtColumns.Rows[i]["Comments"].ToString(); //注释

                    this.SbTemp.Append("\r\n//").Append(comments);
                    this.SbTemp.AppendFormat("\r\nif (model.{0} != null)", colName);
                    this.SbTemp.Append("\r\n{");

                    if (this.DbType == EnumDbType.Oracle)
                    {
                        this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=:{1}\");", simpleDotColName, colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
                    }
                    else if (this.DbType == EnumDbType.SqlServer)
                    {
                        this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=@{1}\");", simpleDotColName, colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
                    }

                    this.SbTemp.Append("\r\n}");
                    this.SbTemp.Append("\r\n");
                }
                this.SbTemp.Append("\r\n#endregion");

                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n//拼接Where条件");
                this.SbTemp.Append("\r\nif (sbWhere.Length > 0)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append(
                    "\r\nsqlstr += sbWhere.ToString().Substring(sbWhere.ToString().IndexOf(\"AND\") + 3).Insert(0, \" Where\");");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nlistParam = param.ListParameter;");
                this.SbTemp.Append("\r\n}");
            }
        }
        #endregion

        #region Generate Insert Code
        /// <summary>
        /// Insert
        /// </summary>
        protected override void concatInsert()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Insert()")
                || this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                this.SbTemp.Append("\r\n#region Insert");
            }

            Insert();
            InsertList();
            addParam();

            if (this.CheckedListBox1.CheckedItems.Contains("Insert()")
                || this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }
            this.SbTemp.Append("\r\n");
        }

        private void Insert()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Insert()"))
            {
                //Insert Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Insert Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Insert({0} model)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\naddParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSql(sqlstr, listParam);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "Insert", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void InsertList()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Insert Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? InsertList(List<{0}> listModel)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "listModel");

                this.SbTemp.Append("\r\nvar listSql = new List<string>();").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nvar listParams = new List<List<{0}>>();", parameter).Append(
                    commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> param = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nforeach (var item in listModel)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\naddParam(item, ref  sqlstr, ref  param);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\nlistSql.Add(sqlstr);");
                this.SbTemp.Append("\r\nlistParams.Add(param);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSqlArray(listSql, listParams);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "InsertList", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void addParam()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Insert()")
               || this.CheckedListBox1.CheckedItems.Contains("InsertList()"))
            {

                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Concat Add SqlString And Parameters");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat(
                    "\r\nprivate void addParam({0} model, ref string sqlstr, ref  List<{1}> listParam)",
                    this.PreModel + PascalTableName, parameter);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("if (sqlstr == string.Empty)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nsqlstr = \"INSERT INTO ").Append(PascalTableName).Append("({0}) VALUES({1})\";");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nvar sbField = new StringBuilder();");
                this.SbTemp.Append("\r\nvar sbParam = new StringBuilder();");
                this.SbTemp.AppendFormat("\r\nvar param = new {0}();", paramsHelper).Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append(" \r\n#region Concat Parameter");
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    if (this.IsPascal)
                    {
                        colName = dtColumns.Rows[i]["PascalName"].ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i]["Column_Name"].ToString();
                    }
                    string comments = dtColumns.Rows[i]["Comments"].ToString(); //Comments

                    this.SbTemp.Append("\r\n//").Append(comments);
                    this.SbTemp.AppendFormat("\r\nif (model.{0} != null)", colName);
                    this.SbTemp.Append("\r\n{");

                    this.SbTemp.AppendFormat("\r\nsbField.Append(\"{0},\");", colName);


                    if (this.DbType == EnumDbType.Oracle)
                    {
                        this.SbTemp.AppendFormat("\r\nsbParam.Append(\":{0},\");", colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
                    }
                    else if (this.DbType == EnumDbType.SqlServer)
                    {
                        this.SbTemp.AppendFormat("\r\nsbParam.Append(\"@{0},\");", colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
                    }


                    this.SbTemp.Append("\r\n}");
                    this.SbTemp.Append("\r\n");
                }

                this.SbTemp.Append("\r\n#endregion");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n//Concat Condition");
                this.SbTemp.Append(
                    "\r\nsqlstr = string.Format(sqlstr, sbField.ToString().TrimEnd(','), sbParam.ToString().TrimEnd(','));");
                this.SbTemp.Append("\r\nlistParam = param.ListParameter;");
                this.SbTemp.Append("\r\n}");
            }
        }
        #endregion

        #region Generate Update Code
        /// <summary>
        /// Update
        /// </summary>
        protected override void concatUpdate()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Update()") ||
                this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                this.SbTemp.Append("\r\n#region Update");
            }

            update();
            updateList();
            updateParam();

            if (this.CheckedListBox1.CheckedItems.Contains("Update()") ||
                this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }

            this.SbTemp.Append("\r\n");
        }

        private void update()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Update()"))
            {
                //Update Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Update Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Update({0} model)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\nupdateParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSql(sqlstr, listParam);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "Update", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void updateList()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                //Update Items
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Update Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? UpdateList(List<{0}> listModel)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "listModel");

                this.SbTemp.Append("\r\nvar listSql = new List<string>();").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nvar listParams = new List<List<{0}>>();", parameter).Append(
                    commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> param = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nforeach (var item in listModel)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nupdateParam(item, ref  sqlstr, ref  param);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\nlistSql.Add(sqlstr);");
                this.SbTemp.Append("\r\nlistParams.Add(param);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSqlArray(listSql, listParams);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "UpdateList", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }

        }

        private void updateParam()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Update()") ||
               this.CheckedListBox1.CheckedItems.Contains("UpdateList()"))
            {
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Concat Update SqlString And Parameters");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat(
                    "\r\nprivate void updateParam({0} model, ref string sqlstr, ref  List<{1}> listParam)",
                    this.PreModel + PascalTableName, parameter);
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("if (sqlstr == string.Empty)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nsqlstr = \"UPDATE ").Append(PascalTableName).Append(" SET {0} WHERE {1}\";");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nvar sbParam = new StringBuilder();");
                this.SbTemp.Append("\r\nvar sbWhere = new StringBuilder();");
                this.SbTemp.AppendFormat("\r\nvar param = new {0}();", paramsHelper).Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n#region Concat Parameter");

                //Concat Non-PrimaryKey
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    if (this.IsPascal)
                    {
                        colName = dtColumns.Rows[i]["PascalName"].ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i]["Column_Name"].ToString();
                    }
                    if (!listPrimaryKey.Contains(dtColumns.Rows[i]["Column_Name"].ToString())) //Non-PrimaryKey
                    {
                        string comments = dtColumns.Rows[i]["Comments"].ToString(); //Comments

                        this.SbTemp.Append("\r\n//").Append(comments);
                        this.SbTemp.AppendFormat("\r\nif (model.{0} != null)", colName);
                        this.SbTemp.Append("\r\n{");

                        string dataPrecison = dtColumns.Rows[i]["Data_Precision"].ToString();
                        string dataType = dtColumns.Rows[i]["Data_Type"].ToString();
                        string dataScale = dtColumns.Rows[i]["Data_Scale"].ToString();
                        if (this.DbType == EnumDbType.Oracle)
                        {
                            //String
                            if (string.IsNullOrEmpty(dataPrecison))
                            {
                                this.SbTemp.AppendFormat("\r\nsbParam.Append(\"{0}=:{1},\");", colName, colName);
                                this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
                            }
                            else
                            {
                                if (dataType == "NUMBER") //NUMBER
                                {
                                    if (Convert.ToInt32(dataPrecison) > 10) //long
                                    {
                                        updateEmptyCol(this.SbTemp, colName, "long", this.DbType);
                                    }
                                    else
                                    {
                                        updateEmptyCol(this.SbTemp, colName, "int", this.DbType);
                                    }
                                }
                            }
                        }
                        else if (this.DbType == EnumDbType.SqlServer)
                        {
                            //String、image、ntext ect.
                            if (string.IsNullOrEmpty(dataScale))
                            {
                                this.SbTemp.AppendFormat("\r\nsbParam.Append(\"{0}=@{1},\");", colName, colName);
                                this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
                            }
                            else
                            {
                                if (dataType.ToLower() == "int") //Number
                                {
                                    if (Convert.ToInt32(dataPrecison) > 10) //long
                                    {
                                        updateEmptyCol(this.SbTemp, colName, "long", this.DbType);
                                    }
                                    else
                                    {
                                        updateEmptyCol(this.SbTemp, colName, "int", this.DbType);
                                    }
                                }
                            }

                        }
                        this.SbTemp.Append("\r\n}");
                        this.SbTemp.Append("\r\n");
                    }
                }

                //Concat PrimaryKey
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    if (this.IsPascal)
                    {
                        colName = dtColumns.Rows[i]["PascalName"].ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i]["Column_Name"].ToString();
                    }
                    if (listPrimaryKey.Contains(dtColumns.Rows[i]["Column_Name"].ToString())) //PrimaryKey
                    {
                        string comments = dtColumns.Rows[i]["Comments"] + "[主键]"; //Comments

                        this.SbTemp.Append("\r\n//").Append(comments);
                        this.SbTemp.AppendFormat("\r\nif (model.{0} != null)", colName);
                        this.SbTemp.Append("\r\n{");

                        if (this.DbType == EnumDbType.Oracle)
                        {
                            this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=:{1}\");", colName, colName);
                            this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
                        }
                        else if (this.DbType == EnumDbType.SqlServer)
                        {
                            this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=@{1}\");", colName, colName);
                            this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
                        }
                        this.SbTemp.Append("\r\n}");
                        this.SbTemp.Append("\r\n");
                    }
                }
                this.SbTemp.Append("\r\n#endregion");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n//Concat Condition");
                this.SbTemp.Append(
                    "\r\nsqlstr = string.Format(sqlstr, sbParam.ToString().TrimEnd(','), sbWhere.ToString().Substring(sbWhere.ToString().IndexOf(\"AND\") + 3));");
                this.SbTemp.Append("\r\nlistParam = param.ListParameter;");
                this.SbTemp.Append("\r\n}");
            }
        }
        #endregion

        #region Generate Delete Code
        /// <summary>
        /// Delete
        /// </summary>
        protected override void concatDelete()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Delete()")
                || this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                this.SbTemp.Append("\r\n#region 删除");
            }

            delete();
            deleteList();
            deleteByKeys();
            deleteParam();

            if (this.CheckedListBox1.CheckedItems.Contains("Delete()")
                || this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                this.SbTemp.Append("\r\n#endregion");
            }
            this.SbTemp.Append("\r\n");
        }

        private void delete()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Delete()"))
            {
                //Delete Item
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Delete Item(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? Delete({0} model)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "model");

                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> listParam = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\ndeleteParam(model, ref sqlstr, ref listParam);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSql(sqlstr, listParam);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "Delete", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void deleteList()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                //Delete Items
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Delete Items(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? DeleteList(List<{0}> listModel)", this.PreModel + PascalTableName);
                this.SbTemp.Append("\r\n{");

                modelNullReturn(this.SbTemp, "listModel");

                this.SbTemp.Append("\r\nvar listSql = new List<string>();").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nvar listParams = new List<List<{0}>>();", parameter).Append(
                    commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nstring sqlstr = string.Empty;").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nList<{0}> param = null;", parameter).Append(commentsVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nforeach (var item in listModel)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\ndeleteParam(item, ref  sqlstr, ref  param);").Append(commentsConcatVar);
                this.SbTemp.Append("\r\nlistSql.Add(sqlstr);");
                this.SbTemp.Append("\r\nlistParams.Add(param);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSqlArray(listSql, listParams);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "DeleteList", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue =null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void deleteByKeys()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("DeletebyKeys()"))
            {
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///  //Delete By String Array（Format '1','2','3')(exception when return null)");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat("\r\npublic int? DeletebyKeys(string keys)", PascalTableName);
                this.SbTemp.Append("\r\n{");

                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("if(string.IsNullOrWhiteSpace(keys))");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nreturn null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");

                this.SbTemp.Append("\r\nstring sqlstr = \" DELETE FROM {0} WHERE {1} IN ({2})\";").Append(commentsSql);
                this.SbTemp.AppendFormat("\r\nvar param = new {0}();", paramsHelper).Append(commentsConcatVar);

                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    string columnName = dtColumns.Rows[i]["Column_Name"].ToString();
                    if (listPrimaryKey.Contains(columnName)) //Non-PrimayKey
                    {
                        string comments = dtColumns.Rows[i]["Comments"].ToString(); //Comments
                        this.SbTemp.AppendFormat("\r\nsqlstr=string.Format(sqlstr,\"{0}\",\"{1}\",{2});", PascalTableName,
                                            columnName, "keys");
                        this.SbTemp.Append("\r\n");
                        break;
                    }
                }

                this.SbTemp.Append("\r\nint? returnValue;");
                this.SbTemp.Append("\r\ntry");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.AppendFormat("\r\nvar dbHelper=new {0}();", dbHelper);
                this.SbTemp.Append("\r\nreturnValue = dbHelper.ExecuteSql(sqlstr);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\ncatch (Exception ex)");
                this.SbTemp.Append("\r\n{");
                writeLog(this.SbTemp, "DeletebyKeys", this.IsLog);
                this.SbTemp.Append("\r\nreturnValue = null;");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nreturn returnValue;");
                this.SbTemp.Append("\r\n}");
            }
        }

        private void deleteParam()
        {
            if (this.CheckedListBox1.CheckedItems.Contains("Delete()")
            || this.CheckedListBox1.CheckedItems.Contains("DeleteList()"))
            {
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Concat Delete Parameters");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.AppendFormat(
                    "\r\nprivate void deleteParam({0} model, ref string sqlstr, ref  List<{1}> listParam)",
                    this.PreModel + PascalTableName, parameter);
                this.SbTemp.Append("{\r\n");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("if (sqlstr == string.Empty)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nsqlstr = \"DELETE FROM ").Append(PascalTableName).Append(" WHERE {0}\";");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nvar sbWhere = new StringBuilder();");
                this.SbTemp.AppendFormat("\r\nvar param = new {0}();", paramsHelper).Append(commentsConcatVar);
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\n#region Concat Parameter");
                for (int i = 0; i < dtColumns.Rows.Count; i++)
                {
                    if (this.IsPascal)
                    {
                        colName = dtColumns.Rows[i]["PascalName"].ToString();
                    }
                    else
                    {
                        colName = dtColumns.Rows[i]["Column_Name"].ToString();
                    }

                    if (!listPrimaryKey.Contains(dtColumns.Rows[i]["Column_Name"].ToString())) //Non-PrimaryKey
                    {
                        continue;
                    }
                    string comments = dtColumns.Rows[i]["Comments"].ToString(); //Comments

                    this.SbTemp.Append("\r\n//").Append(comments);
                    this.SbTemp.AppendFormat("\r\nif (model.{0} != null)", colName);
                    this.SbTemp.Append("\r\n{");

                    if (this.DbType == EnumDbType.Oracle)
                    {
                        this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=:{1}\");", colName, colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
                    }
                    else if (this.DbType == EnumDbType.SqlServer)
                    {
                        this.SbTemp.AppendFormat("\r\nsbWhere.Append(\" AND {0}=@{1}\");", colName, colName);
                        this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
                    }


                    this.SbTemp.Append("\r\n}");
                    this.SbTemp.Append("\r\n");
                }
                this.SbTemp.Append("\r\n#endregion");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append(
                    "\r\nsqlstr = string.Format(sqlstr, sbWhere.ToString().Substring(sbWhere.ToString().IndexOf(\"AND\") + 3));");
                this.SbTemp.Append("\r\nlistParam = param.ListParameter;");

                this.SbTemp.Append("\r\n}");
            }
        }
        #endregion

        #region Generate Log Code
        /// <summary>
        /// Log
        /// </summary>
        private void concatLog()
        {
            if (this.IsLog)
            {
                this.SbTemp.Append("\r\n#region Log Method");
                this.SbTemp.Append("\r\n///<summary>");
                this.SbTemp.Append("\r\n///Log Method");
                this.SbTemp.Append("\r\n///</summary>");
                this.SbTemp.Append("\r\nprivate void writeLog(string methodName,string message)");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append(
                    "\r\nstring msg = DateTime.Now.ToString() + \"Location Class：\" +this.ToString()+Environment.NewLine +\"Location Method：\" +methodName+Environment.NewLine +\"Exception Message:\" + message ;");
                this.SbTemp.Append(
                    "\r\nstring strFilePath = System.Configuration.ConfigurationManager.AppSettings[\"logPath\"];");
                this.SbTemp.Append("\r\nif (string.IsNullOrWhiteSpace(strFilePath)) //No Node Or Empty Value");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nstrFilePath = \"c:\\this_is_log_test_CJ.txt\";");
                this.SbTemp.Append("\r\nmsg += \"\tNote：If Default Log path isn't exist,use this_is_log_demo.txt as log path temporary，please add in time(logPath)!\";");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append("\r\nSystem.IO.FileStream fs = null;");
                this.SbTemp.Append("\r\nif (!System.IO.File.Exists(strFilePath)) //Create New File If Not Exist");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nfs = new System.IO.FileStream(strFilePath, System.IO.FileMode.OpenOrCreate);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\nelse //Add Data After File");
                this.SbTemp.Append("\r\n{");
                this.SbTemp.Append("\r\nfs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n");
                this.SbTemp.Append(
                    "\r\nSystem.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);");
                this.SbTemp.Append("\r\nsw.WriteLine(msg);");
                this.SbTemp.Append("\r\nsw.Close();");
                this.SbTemp.Append("\r\nfs.Close();");
                this.SbTemp.Append("\r\n}");
                this.SbTemp.Append("\r\n#endregion");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// if int equal int.MinValue,update empty string
        /// </summary>
        /// <param name="sbTemp"></param>
        /// <param name="colName"></param>
        /// <param name="type"></param>
        /// <param name="dbType"></param>
        private void updateEmptyCol(StringBuilder sbTemp, string colName, string type, EnumDbType dbType)
        {
            this.SbTemp.AppendFormat("\r\nif (model.{0} == {1}.MinValue)", colName, type);
            this.SbTemp.Append("\r\n{");
            this.SbTemp.AppendFormat("\r\nsbParam.Append(\"{0}='',\");", colName);
            this.SbTemp.Append("\r\n}");
            this.SbTemp.Append("\r\nelse");
            this.SbTemp.Append("\r\n{");

            if (this.DbType == EnumDbType.Oracle)
            {
                this.SbTemp.AppendFormat("\r\nsbParam.Append(\"{0}=:{1},\");", colName, colName);
                this.SbTemp.AppendFormat("\r\nparam.Add(\":{0}\", model.{1});", colName, colName);
            }
            else if (this.DbType == EnumDbType.SqlServer)
            {
                this.SbTemp.AppendFormat("\r\nsbParam.Append(\"{0}=@{1},\");", colName, colName);
                this.SbTemp.AppendFormat("\r\nparam.Add(\"@{0}\", model.{1});", colName, colName);
            }

            this.SbTemp.Append("\r\n}");
        }

        //Is Contain Log
        private void writeLog(StringBuilder sbTemp, string methodName, bool isLog)
        {
            if (this.IsLog)
            {
                this.SbTemp.AppendFormat("\r\nthis.writeLog(\"{0}\",ex.Message);", methodName);
            }
        }

        /// <summary>
        /// return null when model is null
        /// </summary>
        /// <param name="sbTemp"></param>
        private void modelNullReturn(StringBuilder sbTemp, string modelName)
        {
            this.SbTemp.Append("\r\n");
            this.SbTemp.AppendFormat("if({0}==null)", modelName);
            this.SbTemp.Append("\r\n{");
            this.SbTemp.Append("\r\nreturn null;");
            this.SbTemp.Append("\r\n}");
            this.SbTemp.Append("\r\n");
        }
        #endregion
    }
}
