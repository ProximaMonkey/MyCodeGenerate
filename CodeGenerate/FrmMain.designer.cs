namespace CodeGenerate
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFilterTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSqlServer = new System.Windows.Forms.RadioButton();
            this.rbOracle = new System.Windows.Forms.RadioButton();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSelect = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPascalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbMethod = new System.Windows.Forms.CheckBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tpPreview = new System.Windows.Forms.TabPage();
            this.rtbPreview = new System.Windows.Forms.RichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbSave = new System.Windows.Forms.RadioButton();
            this.rbPreview = new System.Windows.Forms.RadioButton();
            this.btnSaveConvert = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbIDAL = new System.Windows.Forms.RadioButton();
            this.rbDAL = new System.Windows.Forms.RadioButton();
            this.rbBLL = new System.Windows.Forms.RadioButton();
            this.rbModel = new System.Windows.Forms.RadioButton();
            this.rbForm = new System.Windows.Forms.RadioButton();
            this.btnGeneWord = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtModelPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClassPrefix = new System.Windows.Forms.TextBox();
            this.txtNameSpacePrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ckbAllTable = new System.Windows.Forms.CheckBox();
            this.dgvDataTables = new System.Windows.Forms.DataGridView();
            this.ckbCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Table_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabPascalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbPascal = new System.Windows.Forms.RadioButton();
            this.rbOri = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.ckbLogStrConcat = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSelect.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tpPreview.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTables)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilterTable
            // 
            this.txtFilterTable.Location = new System.Drawing.Point(8, 38);
            this.txtFilterTable.Name = "txtFilterTable";
            this.txtFilterTable.Size = new System.Drawing.Size(227, 21);
            this.txtFilterTable.TabIndex = 0;
            this.txtFilterTable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilterTable_KeyUp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter TableName：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSqlServer);
            this.groupBox1.Controls.Add(this.rbOracle);
            this.groupBox1.Location = new System.Drawing.Point(311, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 53);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DataBase";
            // 
            // rbSqlServer
            // 
            this.rbSqlServer.AutoSize = true;
            this.rbSqlServer.Checked = true;
            this.rbSqlServer.Location = new System.Drawing.Point(93, 20);
            this.rbSqlServer.Name = "rbSqlServer";
            this.rbSqlServer.Size = new System.Drawing.Size(77, 16);
            this.rbSqlServer.TabIndex = 1;
            this.rbSqlServer.TabStop = true;
            this.rbSqlServer.Text = "SqlServer";
            this.rbSqlServer.UseVisualStyleBackColor = true;
            // 
            // rbOracle
            // 
            this.rbOracle.AutoSize = true;
            this.rbOracle.Location = new System.Drawing.Point(6, 21);
            this.rbOracle.Name = "rbOracle";
            this.rbOracle.Size = new System.Drawing.Size(59, 16);
            this.rbOracle.TabIndex = 0;
            this.rbOracle.Text = "Oracle";
            this.rbOracle.UseVisualStyleBackColor = true;
            this.rbOracle.CheckedChanged += new System.EventHandler(this.rbOracle_CheckedChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(1153, 113);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(126, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate Code";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSelect);
            this.tabControl1.Controls.Add(this.tpPreview);
            this.tabControl1.Location = new System.Drawing.Point(529, 170);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(753, 439);
            this.tabControl1.TabIndex = 5;
            // 
            // tpSelect
            // 
            this.tpSelect.Controls.Add(this.groupBox5);
            this.tpSelect.Controls.Add(this.groupBox4);
            this.tpSelect.Location = new System.Drawing.Point(4, 22);
            this.tpSelect.Name = "tpSelect";
            this.tpSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tpSelect.Size = new System.Drawing.Size(745, 413);
            this.tpSelect.TabIndex = 0;
            this.tpSelect.Text = "Select";
            this.tpSelect.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvColumns);
            this.groupBox5.Location = new System.Drawing.Point(8, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(546, 434);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToResizeRows = false;
            this.dgvColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Name,
            this.ColPascalName,
            this.ColComments});
            this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvColumns.Location = new System.Drawing.Point(3, 17);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowTemplate.Height = 23;
            this.dgvColumns.Size = new System.Drawing.Size(540, 414);
            this.dgvColumns.TabIndex = 0;
            this.dgvColumns.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvColumns_DataError);
            // 
            // Column_Name
            // 
            this.Column_Name.DataPropertyName = "Column_Name";
            this.Column_Name.HeaderText = "ColumnName";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.Width = 90;
            // 
            // ColPascalName
            // 
            this.ColPascalName.DataPropertyName = "PascalName";
            this.ColPascalName.HeaderText = "PascalColumnName";
            this.ColPascalName.Name = "ColPascalName";
            this.ColPascalName.Width = 126;
            // 
            // ColComments
            // 
            this.ColComments.DataPropertyName = "comments";
            this.ColComments.HeaderText = "Description";
            this.ColComments.Name = "ColComments";
            this.ColComments.ReadOnly = true;
            this.ColComments.Width = 96;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbMethod);
            this.groupBox4.Controls.Add(this.checkedListBox1);
            this.groupBox4.Location = new System.Drawing.Point(560, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 434);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Methods";
            // 
            // ckbMethod
            // 
            this.ckbMethod.AutoSize = true;
            this.ckbMethod.Checked = true;
            this.ckbMethod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMethod.Location = new System.Drawing.Point(7, 17);
            this.ckbMethod.Name = "ckbMethod";
            this.ckbMethod.Size = new System.Drawing.Size(78, 16);
            this.ckbMethod.TabIndex = 1;
            this.ckbMethod.Text = "Check All";
            this.ckbMethod.UseVisualStyleBackColor = true;
            this.ckbMethod.CheckedChanged += new System.EventHandler(this.ckbMethod_CheckedChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 43);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(176, 388);
            this.checkedListBox1.TabIndex = 0;
            // 
            // tpPreview
            // 
            this.tpPreview.Controls.Add(this.rtbPreview);
            this.tpPreview.Location = new System.Drawing.Point(4, 22);
            this.tpPreview.Name = "tpPreview";
            this.tpPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tpPreview.Size = new System.Drawing.Size(745, 413);
            this.tpPreview.TabIndex = 1;
            this.tpPreview.Text = "Preview";
            this.tpPreview.UseVisualStyleBackColor = true;
            // 
            // rtbPreview
            // 
            this.rtbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPreview.Location = new System.Drawing.Point(3, 3);
            this.rtbPreview.Name = "rtbPreview";
            this.rtbPreview.Size = new System.Drawing.Size(739, 407);
            this.rtbPreview.TabIndex = 0;
            this.rtbPreview.Text = "";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbSave);
            this.groupBox8.Controls.Add(this.rbPreview);
            this.groupBox8.Location = new System.Drawing.Point(828, 106);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(151, 59);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Generate Type";
            // 
            // rbSave
            // 
            this.rbSave.AutoSize = true;
            this.rbSave.Location = new System.Drawing.Point(87, 21);
            this.rbSave.Name = "rbSave";
            this.rbSave.Size = new System.Drawing.Size(47, 16);
            this.rbSave.TabIndex = 1;
            this.rbSave.Text = "Save";
            this.rbSave.UseVisualStyleBackColor = true;
            // 
            // rbPreview
            // 
            this.rbPreview.AutoSize = true;
            this.rbPreview.Checked = true;
            this.rbPreview.Location = new System.Drawing.Point(6, 21);
            this.rbPreview.Name = "rbPreview";
            this.rbPreview.Size = new System.Drawing.Size(65, 16);
            this.rbPreview.TabIndex = 0;
            this.rbPreview.TabStop = true;
            this.rbPreview.Text = "Preview";
            this.rbPreview.UseVisualStyleBackColor = true;
            // 
            // btnSaveConvert
            // 
            this.btnSaveConvert.Location = new System.Drawing.Point(1153, 142);
            this.btnSaveConvert.Name = "btnSaveConvert";
            this.btnSaveConvert.Size = new System.Drawing.Size(126, 23);
            this.btnSaveConvert.TabIndex = 10;
            this.btnSaveConvert.Text = "Save Relationship";
            this.btnSaveConvert.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbIDAL);
            this.groupBox3.Controls.Add(this.rbDAL);
            this.groupBox3.Controls.Add(this.rbBLL);
            this.groupBox3.Controls.Add(this.rbModel);
            this.groupBox3.Location = new System.Drawing.Point(531, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 59);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Generate Layer";
            // 
            // rbIDAL
            // 
            this.rbIDAL.AutoSize = true;
            this.rbIDAL.Location = new System.Drawing.Point(176, 21);
            this.rbIDAL.Name = "rbIDAL";
            this.rbIDAL.Size = new System.Drawing.Size(47, 16);
            this.rbIDAL.TabIndex = 4;
            this.rbIDAL.Text = "IDAL";
            this.rbIDAL.UseVisualStyleBackColor = true;
            // 
            // rbDAL
            // 
            this.rbDAL.AutoSize = true;
            this.rbDAL.Location = new System.Drawing.Point(125, 21);
            this.rbDAL.Name = "rbDAL";
            this.rbDAL.Size = new System.Drawing.Size(41, 16);
            this.rbDAL.TabIndex = 2;
            this.rbDAL.Text = "DAL";
            this.rbDAL.UseVisualStyleBackColor = true;
            this.rbDAL.CheckedChanged += new System.EventHandler(this.rbModel_CheckedChanged);
            // 
            // rbBLL
            // 
            this.rbBLL.AutoSize = true;
            this.rbBLL.Location = new System.Drawing.Point(72, 21);
            this.rbBLL.Name = "rbBLL";
            this.rbBLL.Size = new System.Drawing.Size(41, 16);
            this.rbBLL.TabIndex = 1;
            this.rbBLL.Text = "BLL";
            this.rbBLL.UseVisualStyleBackColor = true;
            this.rbBLL.CheckedChanged += new System.EventHandler(this.rbModel_CheckedChanged);
            // 
            // rbModel
            // 
            this.rbModel.AutoSize = true;
            this.rbModel.Checked = true;
            this.rbModel.Location = new System.Drawing.Point(13, 21);
            this.rbModel.Name = "rbModel";
            this.rbModel.Size = new System.Drawing.Size(53, 16);
            this.rbModel.TabIndex = 0;
            this.rbModel.TabStop = true;
            this.rbModel.Text = "Model";
            this.rbModel.UseVisualStyleBackColor = true;
            this.rbModel.CheckedChanged += new System.EventHandler(this.rbModel_CheckedChanged);
            // 
            // rbForm
            // 
            this.rbForm.AutoSize = true;
            this.rbForm.Location = new System.Drawing.Point(400, 1);
            this.rbForm.Name = "rbForm";
            this.rbForm.Size = new System.Drawing.Size(35, 16);
            this.rbForm.TabIndex = 3;
            this.rbForm.Text = "UI";
            this.rbForm.UseVisualStyleBackColor = true;
            this.rbForm.Visible = false;
            // 
            // btnGeneWord
            // 
            this.btnGeneWord.Location = new System.Drawing.Point(1177, 12);
            this.btnGeneWord.Name = "btnGeneWord";
            this.btnGeneWord.Size = new System.Drawing.Size(102, 23);
            this.btnGeneWord.TabIndex = 7;
            this.btnGeneWord.Text = "Generate Word";
            this.btnGeneWord.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtModelPrefix);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtClassPrefix);
            this.groupBox2.Controls.Add(this.txtNameSpacePrefix);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(639, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 99);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Prefix";
            // 
            // txtModelPrefix
            // 
            this.txtModelPrefix.Location = new System.Drawing.Point(89, 70);
            this.txtModelPrefix.Name = "txtModelPrefix";
            this.txtModelPrefix.Size = new System.Drawing.Size(427, 21);
            this.txtModelPrefix.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Model：";
            // 
            // txtClassPrefix
            // 
            this.txtClassPrefix.Location = new System.Drawing.Point(89, 43);
            this.txtClassPrefix.Name = "txtClassPrefix";
            this.txtClassPrefix.Size = new System.Drawing.Size(427, 21);
            this.txtClassPrefix.TabIndex = 3;
            // 
            // txtNameSpacePrefix
            // 
            this.txtNameSpacePrefix.Location = new System.Drawing.Point(89, 13);
            this.txtNameSpacePrefix.Name = "txtNameSpacePrefix";
            this.txtNameSpacePrefix.Size = new System.Drawing.Size(427, 21);
            this.txtNameSpacePrefix.TabIndex = 2;
            this.txtNameSpacePrefix.Text = "CodeGenerate.Demo";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Class：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "NameSpace：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtFilterTable);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.ckbAllTable);
            this.groupBox6.Controls.Add(this.dgvDataTables);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(511, 597);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "DataTable";
            // 
            // ckbAllTable
            // 
            this.ckbAllTable.AutoSize = true;
            this.ckbAllTable.Location = new System.Drawing.Point(227, 13);
            this.ckbAllTable.Name = "ckbAllTable";
            this.ckbAllTable.Size = new System.Drawing.Size(78, 16);
            this.ckbAllTable.TabIndex = 4;
            this.ckbAllTable.Text = "Check All";
            this.ckbAllTable.UseVisualStyleBackColor = true;
            this.ckbAllTable.CheckedChanged += new System.EventHandler(this.ckbAllTable_CheckedChanged);
            // 
            // dgvDataTables
            // 
            this.dgvDataTables.AllowUserToAddRows = false;
            this.dgvDataTables.AllowUserToResizeRows = false;
            this.dgvDataTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDataTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ckbCheck,
            this.Table_Name,
            this.TabPascalName,
            this.comments});
            this.dgvDataTables.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDataTables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDataTables.Location = new System.Drawing.Point(3, 73);
            this.dgvDataTables.Name = "dgvDataTables";
            this.dgvDataTables.RowTemplate.Height = 23;
            this.dgvDataTables.Size = new System.Drawing.Size(505, 521);
            this.dgvDataTables.TabIndex = 3;
            this.dgvDataTables.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDataTables_CellMouseClick);
            // 
            // ckbCheck
            // 
            this.ckbCheck.DataPropertyName = "ckbCheck";
            this.ckbCheck.FillWeight = 60.9137F;
            this.ckbCheck.HeaderText = "";
            this.ckbCheck.Name = "ckbCheck";
            this.ckbCheck.Width = 5;
            // 
            // Table_Name
            // 
            this.Table_Name.DataPropertyName = "TableName";
            this.Table_Name.FillWeight = 113.0288F;
            this.Table_Name.HeaderText = "TableName";
            this.Table_Name.Name = "Table_Name";
            this.Table_Name.ReadOnly = true;
            this.Table_Name.Width = 84;
            // 
            // TabPascalName
            // 
            this.TabPascalName.DataPropertyName = "PascalName";
            this.TabPascalName.FillWeight = 113.0288F;
            this.TabPascalName.HeaderText = "PascalTableName";
            this.TabPascalName.Name = "TabPascalName";
            this.TabPascalName.Width = 120;
            // 
            // comments
            // 
            this.comments.DataPropertyName = "comments";
            this.comments.FillWeight = 113.0288F;
            this.comments.HeaderText = "Description";
            this.comments.Name = "comments";
            this.comments.ReadOnly = true;
            this.comments.Width = 96;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbPascal);
            this.groupBox7.Controls.Add(this.rbOri);
            this.groupBox7.Location = new System.Drawing.Point(993, 106);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(154, 59);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Display Type";
            // 
            // rbPascal
            // 
            this.rbPascal.AutoSize = true;
            this.rbPascal.Checked = true;
            this.rbPascal.Location = new System.Drawing.Point(86, 21);
            this.rbPascal.Name = "rbPascal";
            this.rbPascal.Size = new System.Drawing.Size(59, 16);
            this.rbPascal.TabIndex = 1;
            this.rbPascal.TabStop = true;
            this.rbPascal.Text = "Pascal";
            this.rbPascal.UseVisualStyleBackColor = true;
            // 
            // rbOri
            // 
            this.rbOri.AutoSize = true;
            this.rbOri.Location = new System.Drawing.Point(6, 21);
            this.rbOri.Name = "rbOri";
            this.rbOri.Size = new System.Drawing.Size(77, 16);
            this.rbOri.TabIndex = 0;
            this.rbOri.Text = "Primitive";
            this.rbOri.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.ckbLogStrConcat);
            this.groupBox9.Location = new System.Drawing.Point(533, 1);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(100, 72);
            this.groupBox9.TabIndex = 12;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "IsLog";
            // 
            // ckbLogStrConcat
            // 
            this.ckbLogStrConcat.Location = new System.Drawing.Point(6, 20);
            this.ckbLogStrConcat.Name = "ckbLogStrConcat";
            this.ckbLogStrConcat.Size = new System.Drawing.Size(88, 44);
            this.ckbLogStrConcat.TabIndex = 1;
            this.ckbLogStrConcat.Text = "IsContain Log String";
            this.ckbLogStrConcat.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 609);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.rbForm);
            this.Controls.Add(this.btnSaveConvert);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGeneWord);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSelect.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tpPreview.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataTables)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPreview;
        private System.Windows.Forms.RichTextBox rtbPreview;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClassPrefix;
        private System.Windows.Forms.TextBox txtNameSpacePrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbBLL;
        private System.Windows.Forms.RadioButton rbModel;
        private System.Windows.Forms.RadioButton rbDAL;
        private System.Windows.Forms.Button btnGeneWord;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.Button btnSaveConvert;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvDataTables;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbPascal;
        private System.Windows.Forms.RadioButton rbOri;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbSave;
        private System.Windows.Forms.RadioButton rbPreview;
        private System.Windows.Forms.TabPage tpSelect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbMethod;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.CheckBox ckbAllTable;
        private System.Windows.Forms.TextBox txtModelPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox ckbLogStrConcat;
        private System.Windows.Forms.RadioButton rbForm;
        private System.Windows.Forms.RadioButton rbSqlServer;
        private System.Windows.Forms.RadioButton rbOracle;
        private System.Windows.Forms.RadioButton rbIDAL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn Table_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn TabPascalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn comments;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPascalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColComments;
    }
}