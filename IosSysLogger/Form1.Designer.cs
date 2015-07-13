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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctr = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(740, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 21);
            this.textBox2.TabIndex = 1;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(946, 15);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 2;
            this.searchBtn.Text = "search";
            this.searchBtn.UseVisualStyleBackColor = true;
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
            this.ctr});
            this.dataGridView1.Location = new System.Drawing.Point(3, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1024, 202);
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
            this.loglevelCheckBox.Location = new System.Drawing.Point(505, 45);
            this.loglevelCheckBox.Name = "loglevelCheckBox";
            this.loglevelCheckBox.Size = new System.Drawing.Size(130, 180);
            this.loglevelCheckBox.TabIndex = 6;
            // 
            // clearSearchBtn
            // 
            this.clearSearchBtn.Location = new System.Drawing.Point(946, 74);
            this.clearSearchBtn.Name = "clearSearchBtn";
            this.clearSearchBtn.Size = new System.Drawing.Size(75, 36);
            this.clearSearchBtn.TabIndex = 7;
            this.clearSearchBtn.Text = "clear search";
            this.clearSearchBtn.UseVisualStyleBackColor = true;
            // 
            // fixScroll
            // 
            this.fixScroll.AutoSize = true;
            this.fixScroll.Location = new System.Drawing.Point(740, 65);
            this.fixScroll.Name = "fixScroll";
            this.fixScroll.Size = new System.Drawing.Size(72, 16);
            this.fixScroll.TabIndex = 8;
            this.fixScroll.Text = "fix scroll";
            this.fixScroll.UseVisualStyleBackColor = true;
            // 
            // devicename
            // 
            this.devicename.FormattingEnabled = true;
            this.devicename.Location = new System.Drawing.Point(101, 45);
            this.devicename.Name = "devicename";
            this.devicename.Size = new System.Drawing.Size(120, 84);
            this.devicename.TabIndex = 9;
            // 
            // processlistname
            // 
            this.processlistname.FormattingEnabled = true;
            this.processlistname.HorizontalScrollbar = true;
            this.processlistname.Location = new System.Drawing.Point(300, 45);
            this.processlistname.Name = "processlistname";
            this.processlistname.Size = new System.Drawing.Size(138, 180);
            this.processlistname.TabIndex = 10;
            // 
            // highlightBtn
            // 
            this.highlightBtn.Location = new System.Drawing.Point(946, 45);
            this.highlightBtn.Name = "highlightBtn";
            this.highlightBtn.Size = new System.Drawing.Size(75, 23);
            this.highlightBtn.TabIndex = 12;
            this.highlightBtn.Text = "highlight";
            this.highlightBtn.UseVisualStyleBackColor = true;
            // 
            // DevicenameLabel
            // 
            this.DevicenameLabel.AutoSize = true;
            this.DevicenameLabel.Location = new System.Drawing.Point(101, 15);
            this.DevicenameLabel.Name = "DevicenameLabel";
            this.DevicenameLabel.Size = new System.Drawing.Size(81, 12);
            this.DevicenameLabel.TabIndex = 13;
            this.DevicenameLabel.Text = "Device Name";
            // 
            // ProcessLabel
            // 
            this.ProcessLabel.AutoSize = true;
            this.ProcessLabel.Location = new System.Drawing.Point(300, 15);
            this.ProcessLabel.Name = "ProcessLabel";
            this.ProcessLabel.Size = new System.Drawing.Size(66, 12);
            this.ProcessLabel.TabIndex = 14;
            this.ProcessLabel.Text = "Processes";
            this.ProcessLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // loglevelLabel
            // 
            this.loglevelLabel.AutoSize = true;
            this.loglevelLabel.Location = new System.Drawing.Point(505, 13);
            this.loglevelLabel.Name = "loglevelLabel";
            this.loglevelLabel.Size = new System.Drawing.Size(61, 12);
            this.loglevelLabel.TabIndex = 15;
            this.loglevelLabel.Text = "Log Label";
            // 
            // savedatagrid
            // 
            this.savedatagrid.Location = new System.Drawing.Point(946, 116);
            this.savedatagrid.Name = "savedatagrid";
            this.savedatagrid.Size = new System.Drawing.Size(75, 23);
            this.savedatagrid.TabIndex = 16;
            this.savedatagrid.Text = "save ";
            this.savedatagrid.UseVisualStyleBackColor = true;
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(946, 146);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 23);
            this.load.TabIndex = 17;
            this.load.Text = "load";
            this.load.UseVisualStyleBackColor = true;
            // 
            // iosSyslogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 496);
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
            this.Controls.Add(this.textBox2);
            this.Name = "iosSyslogger";
            this.Text = "iosSysLogger";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckedListBox loglevelCheckBox;
        private System.Windows.Forms.Button clearSearchBtn;
        private System.Windows.Forms.CheckBox fixScroll;
        private System.Windows.Forms.CheckedListBox devicename;
        private System.Windows.Forms.CheckedListBox processlistname;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn device;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Log;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctr;
        private System.Windows.Forms.Button highlightBtn;
        private System.Windows.Forms.Label DevicenameLabel;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.Label loglevelLabel;
        private System.Windows.Forms.Button savedatagrid;
        private System.Windows.Forms.Button load;
    }
}

