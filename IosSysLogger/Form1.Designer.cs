namespace IosSysLogger
{
    partial class iosSyslogger
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchTxtBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fontcolor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.loglevelCheckBox = new System.Windows.Forms.CheckedListBox();
            this.clearSearchBtn = new System.Windows.Forms.Button();
            this.fixScroll = new System.Windows.Forms.CheckBox();
            this.devicename = new System.Windows.Forms.CheckedListBox();
            this.processlistname = new System.Windows.Forms.CheckedListBox();
            this.highlightBtn = new System.Windows.Forms.Button();
            this.DevicenameLabel = new System.Windows.Forms.Label();
            this.ProcessLabel = new System.Windows.Forms.Label();
            this.loglevelLabel = new System.Windows.Forms.Label();
            this.savedatagrid = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.highlightTextBox = new System.Windows.Forms.TextBox();
            this.clearData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchTxtBox
            // 
            this.searchTxtBox.Location = new System.Drawing.Point(748, 57);
            this.searchTxtBox.Name = "searchTxtBox";
            this.searchTxtBox.Size = new System.Drawing.Size(200, 21);
            this.searchTxtBox.TabIndex = 1;
            // 
            // searchBtn
            // 
            this.searchBtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Location = new System.Drawing.Point(748, 97);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(90, 36);
            this.searchBtn.TabIndex = 2;
            this.searchBtn.Text = "search";
            this.searchBtn.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.device,
            this.Process,
            this.LogLevel,
            this.Log,
            this.ctr,
            this.fontcolor});
            this.dataGridView1.Location = new System.Drawing.Point(3, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1266, 282);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // date
            // 
            this.date.FillWeight = 50.9684F;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            // 
            // device
            // 
            this.device.FillWeight = 47.36657F;
            this.device.HeaderText = "Device";
            this.device.Name = "device";
            this.device.Width = 93;
            // 
            // Process
            // 
            this.Process.FillWeight = 46.5834F;
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.Width = 91;
            // 
            // LogLevel
            // 
            this.LogLevel.FillWeight = 48.57244F;
            this.LogLevel.HeaderText = "LogLevel";
            this.LogLevel.Name = "LogLevel";
            this.LogLevel.Width = 96;
            // 
            // Log
            // 
            this.Log.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Log.FillWeight = 306.5092F;
            this.Log.HeaderText = "Log";
            this.Log.Name = "Log";
            // 
            // ctr
            // 
            this.ctr.HeaderText = "ctr";
            this.ctr.Name = "ctr";
            this.ctr.Visible = false;
            // 
            // fontcolor
            // 
            this.fontcolor.HeaderText = "fontcolor";
            this.fontcolor.Name = "fontcolor";
            this.fontcolor.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1017, 266);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(10, 10);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // loglevelCheckBox
            // 
            this.loglevelCheckBox.BackColor = System.Drawing.Color.White;
            this.loglevelCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loglevelCheckBox.ColumnWidth = 5;
            this.loglevelCheckBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loglevelCheckBox.ForeColor = System.Drawing.SystemColors.Desktop;
            this.loglevelCheckBox.FormattingEnabled = true;
            this.loglevelCheckBox.Items.AddRange(new object[] {
            "Debug",
            "Info",
            "Notice",
            "Warning",
            "Error",
            "Critical",
            "Alert",
            "Emergency"});
            this.loglevelCheckBox.Location = new System.Drawing.Point(500, 57);
            this.loglevelCheckBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.loglevelCheckBox.Name = "loglevelCheckBox";
            this.loglevelCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.loglevelCheckBox.Size = new System.Drawing.Size(188, 172);
            this.loglevelCheckBox.TabIndex = 6;
            this.loglevelCheckBox.SelectedIndexChanged += new System.EventHandler(this.loglevelCheckBox_SelectedIndexChanged);
            // 
            // clearSearchBtn
            // 
            this.clearSearchBtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clearSearchBtn.FlatAppearance.BorderSize = 0;
            this.clearSearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearSearchBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearSearchBtn.Location = new System.Drawing.Point(1112, 176);
            this.clearSearchBtn.Name = "clearSearchBtn";
            this.clearSearchBtn.Padding = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.clearSearchBtn.Size = new System.Drawing.Size(90, 53);
            this.clearSearchBtn.TabIndex = 7;
            this.clearSearchBtn.Text = "clear filter";
            this.clearSearchBtn.UseVisualStyleBackColor = false;
            // 
            // fixScroll
            // 
            this.fixScroll.AutoSize = true;
            this.fixScroll.Font = new System.Drawing.Font("휴먼모음T", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.fixScroll.Location = new System.Drawing.Point(964, 221);
            this.fixScroll.Name = "fixScroll";
            this.fixScroll.Size = new System.Drawing.Size(87, 21);
            this.fixScroll.TabIndex = 8;
            this.fixScroll.Text = "fix scroll";
            this.fixScroll.UseVisualStyleBackColor = true;
            // 
            // devicename
            // 
            this.devicename.BackColor = System.Drawing.Color.White;
            this.devicename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.devicename.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.devicename.ForeColor = System.Drawing.SystemColors.Desktop;
            this.devicename.FormattingEnabled = true;
            this.devicename.Location = new System.Drawing.Point(124, 57);
            this.devicename.Name = "devicename";
            this.devicename.Size = new System.Drawing.Size(120, 172);
            this.devicename.TabIndex = 9;
            // 
            // processlistname
            // 
            this.processlistname.BackColor = System.Drawing.Color.White;
            this.processlistname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.processlistname.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processlistname.ForeColor = System.Drawing.SystemColors.Desktop;
            this.processlistname.FormattingEnabled = true;
            this.processlistname.HorizontalScrollbar = true;
            this.processlistname.Location = new System.Drawing.Point(281, 57);
            this.processlistname.Name = "processlistname";
            this.processlistname.Size = new System.Drawing.Size(195, 172);
            this.processlistname.TabIndex = 10;
            // 
            // highlightBtn
            // 
            this.highlightBtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.highlightBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.highlightBtn.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highlightBtn.Location = new System.Drawing.Point(748, 193);
            this.highlightBtn.Name = "highlightBtn";
            this.highlightBtn.Size = new System.Drawing.Size(90, 36);
            this.highlightBtn.TabIndex = 12;
            this.highlightBtn.Text = "highlight";
            this.highlightBtn.UseVisualStyleBackColor = false;
            // 
            // DevicenameLabel
            // 
            this.DevicenameLabel.AutoSize = true;
            this.DevicenameLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DevicenameLabel.Location = new System.Drawing.Point(120, 21);
            this.DevicenameLabel.Name = "DevicenameLabel";
            this.DevicenameLabel.Size = new System.Drawing.Size(108, 19);
            this.DevicenameLabel.TabIndex = 13;
            this.DevicenameLabel.Text = "Device Name";
            // 
            // ProcessLabel
            // 
            this.ProcessLabel.AutoSize = true;
            this.ProcessLabel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessLabel.Location = new System.Drawing.Point(278, 22);
            this.ProcessLabel.Name = "ProcessLabel";
            this.ProcessLabel.Size = new System.Drawing.Size(80, 18);
            this.ProcessLabel.TabIndex = 14;
            this.ProcessLabel.Text = "Processes";
            this.ProcessLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // loglevelLabel
            // 
            this.loglevelLabel.AutoSize = true;
            this.loglevelLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loglevelLabel.Location = new System.Drawing.Point(496, 21);
            this.loglevelLabel.Name = "loglevelLabel";
            this.loglevelLabel.Size = new System.Drawing.Size(90, 19);
            this.loglevelLabel.TabIndex = 15;
            this.loglevelLabel.Text = "Log Label";
            // 
            // savedatagrid
            // 
            this.savedatagrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.savedatagrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.savedatagrid.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savedatagrid.Location = new System.Drawing.Point(1112, 28);
            this.savedatagrid.Name = "savedatagrid";
            this.savedatagrid.Size = new System.Drawing.Size(90, 23);
            this.savedatagrid.TabIndex = 16;
            this.savedatagrid.Text = "save ";
            this.savedatagrid.UseVisualStyleBackColor = false;
            // 
            // load
            // 
            this.load.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.load.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.load.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(1112, 68);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(90, 23);
            this.load.TabIndex = 17;
            this.load.Text = "load";
            this.load.UseVisualStyleBackColor = false;
            // 
            // highlightTextBox
            // 
            this.highlightTextBox.Location = new System.Drawing.Point(748, 149);
            this.highlightTextBox.Name = "highlightTextBox";
            this.highlightTextBox.Size = new System.Drawing.Size(200, 21);
            this.highlightTextBox.TabIndex = 18;
            // 
            // clearData
            // 
            this.clearData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clearData.FlatAppearance.BorderSize = 0;
            this.clearData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearData.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearData.Location = new System.Drawing.Point(1112, 107);
            this.clearData.Name = "clearData";
            this.clearData.Padding = new System.Windows.Forms.Padding(0, 1, 1, 1);
            this.clearData.Size = new System.Drawing.Size(90, 53);
            this.clearData.TabIndex = 19;
            this.clearData.Text = "clear data";
            this.clearData.UseVisualStyleBackColor = false;
            // 
            // iosSyslogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1275, 576);
            this.Controls.Add(this.clearData);
            this.Controls.Add(this.highlightTextBox);
            this.Controls.Add(this.load);
            this.Controls.Add(this.savedatagrid);
            this.Controls.Add(this.loglevelLabel);
            this.Controls.Add(this.ProcessLabel);
            this.Controls.Add(this.DevicenameLabel);
            this.Controls.Add(this.highlightBtn);
            this.Controls.Add(this.processlistname);
            this.Controls.Add(this.devicename);
            this.Controls.Add(this.fixScroll);
            this.Controls.Add(this.clearSearchBtn);
            this.Controls.Add(this.loglevelCheckBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.searchTxtBox);
            this.Name = "iosSyslogger";
            this.Opacity = 0.97D;
            this.Text = "iosSysLogger";
            this.Load += new System.EventHandler(this.iosSyslogger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox searchTxtBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckedListBox loglevelCheckBox;
        private System.Windows.Forms.Button clearSearchBtn;
        private System.Windows.Forms.CheckBox fixScroll;
        private System.Windows.Forms.CheckedListBox devicename;
        private System.Windows.Forms.CheckedListBox processlistname;
        private System.Windows.Forms.Button highlightBtn;
        private System.Windows.Forms.Label DevicenameLabel;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.Label loglevelLabel;
        private System.Windows.Forms.Button savedatagrid;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn device;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Log;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctr;
        private System.Windows.Forms.DataGridViewTextBoxColumn fontcolor;
        private System.Windows.Forms.TextBox highlightTextBox;
        private System.Windows.Forms.Button clearData;
    }
}

