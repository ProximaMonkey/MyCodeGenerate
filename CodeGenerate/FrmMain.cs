using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CodeGenerate.Abstract;
using CodeGenerate.Control;
using CodeGenerate.Properties;
using CodeGenerate.Util;
using CodeGenerate.Model;

namespace CodeGenerate
{
    public partial class FrmMain : Form
    {
        #region Fields and Properties
        private static EnumDbType dbType;
        private string prefixNameSpace;
        private string prefixClass;
        private string prefixModel;

        private List<Table> listCheckedTables;

        private bool isPascal;
        private bool isLog;
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            bindEvent();
        }

        private void bindEvent()
        {
            //Save Relationship
            btnSaveConvert.Click += (sender, e) => XmlControl.WriteXml(dgvDataTables, dgvColumns);

            this.btnGeneWord.Click += (sender, e) => WordControl.Generate(this.dgvDataTables, this.rbPascal.Checked, dbType);

            #region Display DataGridView Line Number

            dgvColumns.RowPostPaint += (sender, e) =>
            {
                var rectangle = new Rectangle(e.RowBounds.Location.X,
                                              e.RowBounds.Location.Y,
                                              dgvColumns.RowHeadersWidth - 4,
                                              e.RowBounds.Height);

                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                      dgvColumns.RowHeadersDefaultCellStyle.Font,
                                      rectangle,
                                      dgvColumns.RowHeadersDefaultCellStyle.ForeColor,
                                      TextFormatFlags.VerticalCenter |
                                      TextFormatFlags.Right);
            };

            dgvDataTables.RowPostPaint += (sender, e) =>
            {
                var rectangle = new Rectangle(e.RowBounds.Location.X,
                                              e.RowBounds.Location.Y,
                                              dgvDataTables.RowHeadersWidth - 4,
                                              e.RowBounds.Height);

                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                      dgvDataTables.RowHeadersDefaultCellStyle.Font,
                                      rectangle,
                                      dgvDataTables.RowHeadersDefaultCellStyle.ForeColor,
                                      TextFormatFlags.VerticalCenter |
                                      TextFormatFlags.Right);
            };

            #endregion
        }
        #endregion

        #region Form_load
        private void FrmMain_Load(object sender, EventArgs e)
        {
            CheckMethod();

            rbOracle_CheckedChanged(sender, e);

            txtFilterTable.Focus();
        }

        private void CheckMethod()
        {
            checkedListBox1.Items.Add("GetList()", true);
            checkedListBox1.Items.Add("GetModel()", true);
            checkedListBox1.Items.Add("GetModelList()", true);
            checkedListBox1.Items.Add("Insert()", true);
            checkedListBox1.Items.Add("InsertList()", true);
            checkedListBox1.Items.Add("Update()", true);
            checkedListBox1.Items.Add("UpdateList()", true);
            checkedListBox1.Items.Add("Delete()", true);
            checkedListBox1.Items.Add("DeleteList()", true);
            checkedListBox1.Items.Add("DeletebyKeys()", false);
        }
        #endregion

        #region Generate Code
        private bool validate()
        {
            if (string.IsNullOrWhiteSpace(txtNameSpacePrefix.Text.Trim()))
            {
                MessageBox.Show(Resources.NameSpace_Is_Not_Empty);
                txtNameSpacePrefix.Focus();
                return false;
            }

            this.listCheckedTables = TableControl.GetCheckedTableName(dgvDataTables, this.rbPascal.Checked);

            if (this.listCheckedTables.Count == 0)
            {
                MessageBox.Show(ConstantUtil.CheckedNoTable);
                return false;
            }

            return true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!validate()) return;

            getPrefixValue();

            this.isPascal = this.rbPascal.Checked;
            this.isLog = this.ckbLogStrConcat.Checked;

            if (rbForm.Checked) //UI Layer
            {
                FormGenerate();
            }
            else if (rbModel.Checked) //Model Layer
            {
                ModelGenerate();
            }
            else if (rbBLL.Checked) //BLL Layer
            {
                BLLGenerate();
            }
            else if (rbDAL.Checked) //DAL Layer
            {
                DALGenerate();
            }
            else if (this.rbIDAL.Checked) //IDAL Layer
            {
                IDALGenerate();
            }
        }

        #region Layer Generate
        private void IDALGenerate()
        {
            var sbTemp = new StringBuilder();
            if (rbPreview.Checked)
            {
                DbOperateBase dbOperateBase = new DALInterfaceControl(this.listCheckedTables[0].TabPascalName, this.prefixNameSpace, this.prefixModel, this.checkedListBox1);
                sbTemp = dbOperateBase.Generate();
                rtbPreview.Text = sbTemp.ToString();
                tabControl1.SelectedIndex = 1;
            }
            else //Generate File
            {
                foreach (Table t in this.listCheckedTables)
                {
                    DbOperateBase dbOperateBase = new DALInterfaceControl(t.TabPascalName, this.prefixNameSpace, this.prefixModel, this.checkedListBox1);
                    sbTemp = dbOperateBase.Generate();
                    generateFile("IDAL", t.TabPascalName, sbTemp);
                }
                if (sbTemp.ToString() == string.Empty) return;
                openGenerateFile("Success in generate DataAccess Interface Layer Files!", "C:\\GeneFile\\IDAL\\");
            }
        }
        private void DALGenerate()
        {
            var sbTemp = new StringBuilder();
            if (rbPreview.Checked)
            {
                if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;

                DbOperateBase dbOperateBase = new DALControl(this.listCheckedTables[0].TableName, this.listCheckedTables[0].TabPascalName,
                                             isPascal, this.prefixNameSpace, this.prefixClass, this.prefixModel,
                                             isLog, this.checkedListBox1, dbType);
                sbTemp = dbOperateBase.Generate();
                rtbPreview.Text = sbTemp.ToString();
                tabControl1.SelectedIndex = 1;
            }
            else //Generate File
            {
                foreach (Table t in this.listCheckedTables)
                {
                    if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;
                    DbOperateBase dbOperateBase = new DALControl(this.listCheckedTables[0].TableName, this.listCheckedTables[0].TabPascalName,
                                               isPascal, this.prefixNameSpace, this.prefixClass, this.prefixModel,
                                               isLog, this.checkedListBox1, dbType);
                    sbTemp = dbOperateBase.Generate();
                    generateFile("DAL", t.TabPascalName, sbTemp);
                }
                if (sbTemp.ToString() == string.Empty) return;
                openGenerateFile("Success in generate DataAccess Layer Files!", "C:\\GeneFile\\DAL\\");
            }
        }

        private void BLLGenerate()
        {
            var sbTemp = new StringBuilder();
            if (rbPreview.Checked)
            {
                DbOperateBase dbOperateBase = new BLLControl(this.listCheckedTables[0].TabPascalName, this.prefixNameSpace, this.prefixClass,
                                             this.prefixModel, this.checkedListBox1);
                sbTemp = dbOperateBase.Generate();
                rtbPreview.Text = sbTemp.ToString();
                tabControl1.SelectedIndex = 1;
            }
            else //Generate File
            {
                foreach (Table t in this.listCheckedTables)
                {
                    DbOperateBase dbOperateBase = new BLLControl(this.listCheckedTables[0].TabPascalName, this.prefixNameSpace, this.prefixClass,
                                                   this.prefixModel, this.checkedListBox1);
                    sbTemp = dbOperateBase.Generate();
                    generateFile("BLL", t.TabPascalName, sbTemp);
                }
                if (sbTemp.ToString() == string.Empty) return;
                openGenerateFile("Success in generate Business Layer Files!", "C:\\GeneFile\\BLL\\");
            }
        }

        private void ModelGenerate()
        {
            var sbTemp = new StringBuilder();

            if (rbPreview.Checked)
            {
                if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;

                ModelControl model = new ModelControl(this.listCheckedTables[0].TableName, this.listCheckedTables[0].TabPascalName,
                                               this.prefixNameSpace, this.prefixModel, isPascal, dbType);
                sbTemp = model.Generate();
                rtbPreview.Text = sbTemp.ToString();
                tabControl1.SelectedIndex = 1;
            }
            else //Generate File
            {
                foreach (Table t in this.listCheckedTables)
                {
                    if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;

                    ModelControl model = new ModelControl(this.listCheckedTables[0].TableName, this.listCheckedTables[0].TabPascalName,
                                                   this.prefixNameSpace, this.prefixModel, isPascal, dbType);
                    sbTemp = model.Generate();
                    generateFile("Model", t.TabPascalName, sbTemp);
                }
                if (sbTemp.ToString() == string.Empty) return;
                openGenerateFile("Success in generate Model Layer Files!", "C:\\GeneFile\\Model\\");
            }
        }

        private void FormGenerate()
        {
            //var sbTemp = new StringBuilder();

            //if (rbPreview.Checked)
            //{
            //    if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;

            //    sbTemp = FormControl.Generate(this.listCheckedTables[0].TableName, this.listCheckedTables[0].TabPascalName,
            //                                  this.prefixModel, isPascal, dbType);
            //    rtbPreview.Text = sbTemp.ToString();
            //    tabControl1.SelectedIndex = 1;
            //}
            //else //Generate File
            //{
            //    foreach (Table t in this.listCheckedTables)
            //    {
            //        if (!isPascalComplete(this.listCheckedTables[0].TableName)) return;
            //        sbTemp = FormControl.Generate(t.TableName, t.TabPascalName, this.prefixModel, isPascal, dbType);

            //        if (sbTemp.ToString() == string.Empty) break;
            //        generateFile("Form", t.TabPascalName, sbTemp); // 生成cs文件
            //    }

            //    if (sbTemp.ToString() == string.Empty) return;

            //    openGenerateFile("Success in generate UI Layer Files!", "C:\\GeneFile\\Form\\");
            //}
        }
        #endregion

        private void getPrefixValue()
        {
            this.prefixNameSpace = this.txtNameSpacePrefix.Text.Trim();
            this.prefixClass = this.txtClassPrefix.Text.Trim();
            this.prefixModel = this.txtModelPrefix.Text.Trim();
        }

        /// <summary>
        /// IsTableAllColumnHasRelativePascalForm
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private bool isPascalComplete(string tableName)
        {
            DataTable dtColumns = ColumnControl.GetTableColumnsByTableName(tableName, dbType);
            bool isAllColumnHasRelativePascalForm = ColumnControl.GetColRelaPascalName(dtColumns); //get relative pascal Columns
            if (this.rbPascal.Checked && !isAllColumnHasRelativePascalForm)
            {
                MessageBox.Show(ConstantUtil.NoRelaPascalCol);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Open File
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="direcPath"></param>
        private void openGenerateFile(string msg, string direcPath)
        {
            MessageBox.Show(msg);
            Process.Start("explorer.exe ", direcPath);
        }

        /// <summary>
        /// Generate Files
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tableName"></param>
        /// <param name="sbTemp"> </param>
        private void generateFile(string type, string tableName, StringBuilder sbTemp)
        {
            const string direcPath = "C:\\GeneFile";
            if (!Directory.Exists(direcPath))
            {
                Directory.CreateDirectory(direcPath);
            }

            if (!Directory.Exists(direcPath + "\\" + type))
            {
                Directory.CreateDirectory(direcPath + "\\" + type);
            }
            string filePath = direcPath + "\\" + type + "\\" + txtClassPrefix.Text + tableName + ".cs";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fs = File.Open(filePath, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(sbTemp.ToString());
                    sw.Flush();
                }
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Change DataBase Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbOracle_CheckedChanged(object sender, EventArgs e)
        {
            dbType = rbOracle.Checked ? EnumDbType.Oracle : EnumDbType.SqlServer;
            if (rbOracle.Checked)
            {
                this.rbPascal.Checked = true;
                this.rbOri.Checked = false;
            }
            else
            {
                this.rbOri.Checked = true;
                this.rbPascal.Checked = false;
            }
            ConfigUtil.SwithDb(dbType);
            XmlControl.CreateCorrespondingXmlWhenNotExist();
            Dictionary<string, string> dic = XmlControl.ReadXml();

            dgvDataTables.DataSource = TableControl.GetTables(dic, string.Empty, dbType);
            dgvDataTables.AutoGenerateColumns = false;
        }

        private void dgvDataTables_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            DataTable dtColumns = ColumnControl.GetColumnAndPascalNameFromXml(
                this.dgvDataTables.Rows[e.RowIndex].Cells["Table_Name"].Value.ToString(), dbType);

            this.dgvColumns.DataSource = dtColumns;
            this.dgvColumns.AutoGenerateColumns = false;
        }

        private void dgvColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        /// <summary>
        /// Filter Table Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterTable_KeyUp(object sender, KeyEventArgs e)
        {
            XmlControl.CreateCorrespondingXmlWhenNotExist();
            Dictionary<string, string> dic = XmlControl.ReadXml();

            dgvDataTables.DataSource = TableControl.GetTables(dic, txtFilterTable.Text.Trim().ToLower(), dbType);
            dgvDataTables.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Check All DataTables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbAllTable_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDataTables.Rows.Count; i++)
            {
                dgvDataTables.Rows[i].Cells["ckbCheck"].Value = ckbAllTable.Checked;
            }
        }


        /// <summary>
        /// Select Generate Layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbModel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBLL.Checked)
            {
                txtClassPrefix.Text = "Bll";
            }
            else if (rbDAL.Checked)
            {
                txtClassPrefix.Text = "Dal";
            }
            else if (this.rbIDAL.Checked)
            {
                txtClassPrefix.Text = "IDal";
            }
        }

        /// <summary>
        /// Checked Methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbMethod_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, ckbMethod.Checked);
            }
        }
        #endregion
    }
}