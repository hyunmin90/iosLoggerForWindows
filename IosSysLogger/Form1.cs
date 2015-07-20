using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace IosSysLogger
{
    public partial class iosSyslogger : Form
    {
        int start = 0;
        int indexOfSearchText = 0;
        int addtRow = 0;
        bool firstRowf = false;
        bool fixScrollCheck = false;
        bool filterApplied = false;
        String Filter = "";
        String selectedTab = "";

        DataView filteredView = new DataView();
        DataTable logParserView = new DataTable();
        DataTable jsonView = new DataTable();

        Dictionary<string,DataTable> dataTables = new Dictionary<string,DataTable>();
        Dictionary<string, DataTable> jsonTables = new Dictionary<string,DataTable>();
        Dictionary<string,DataGridView> dataGrids = new Dictionary<string,DataGridView>();
        Dictionary<string,DataView> filteredTable = new Dictionary<string,DataView>();
        Dictionary<string, TabPage> tabpages = new Dictionary<string, TabPage>();
         
        List<string> selectedLoglevel = new List<string>();
        List<string> processlist = new List<string>();
        List<string> devicenameList = new List<string>();
        Dictionary<string, string> totalSelected = new Dictionary<string, string>();

        List<string> chkdevice = new List<string>();
        List<string> chkprocess = new List<string>();
        List<string> chkloglevel = new List<string>();
        ContextMenuStrip mnu;
        public iosSyslogger()
        {
            InitializeComponent();
            

            var watcher = new ManagementEventWatcher();

            //Allow data copy here 
            mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Copy");
            mnu.Items.AddRange(new ToolStripItem[] { mnuCopy });

            //dataGridView1.ContextMenuStrip = mnu;
            mnuCopy.Click += new EventHandler(copyMnu_Click);

           // foreach (DataGridViewColumn col in dataGridView1.Columns)
            //{
           //     logParserView.Columns.Add(col.Name);
            //    col.DataPropertyName = col.Name;
           // }



           // dataGridView1.DataSource = logParserView;
            //Check boxes handler
            //this.devicename.SelectedIndexChanged += new System.EventHandler(this.checkbox_SelectedIndexChanged);
            this.loglevelCheckBox.SelectedIndexChanged += new System.EventHandler(this.checkbox_SelectedIndexChanged);
            this.processlistname.SelectedIndexChanged += new System.EventHandler(this.checkbox_SelectedIndexChanged);
            //Button Click Handler
            this.highlightBtn.Click += new System.EventHandler(this.highlightBtn_Click);
            this.clearSearchBtn.Click += new System.EventHandler(this.clearSearchBtn_Click);
            this.fixScroll.Click += new System.EventHandler(this.fixScroll_Click);
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            this.savedatagrid.Click += new System.EventHandler(this.saveBtn_Click);
            this.load.Click += new System.EventHandler(this.loadBtn_click);
            this.clearData.Click += new System.EventHandler(this.clearDataBtn_Click);

        }

        public void clearDeviceName()
        {
            //devicename.Items.Clear();
        }


        public DataTable LoadJson()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON-FILE | *.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog.FileName))
                {
                    string json = file.ReadToEnd();
                    var table = JsonConvert.DeserializeObject<DataTable>(json);
                    return table;
                }

            }
            else
                return null;
            
        }



        private void saveToJson(string devicename)
        {
            string json = JsonConvert.SerializeObject(dataTables[devicename]);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON-File | *.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName, true))
                {
                    file.WriteLine(json);
                }
            }
        }
        private void loadFromJSON()
        {

            jsonView = LoadJson();
           // foreach (DataGridViewColumn col in dataGridView1.Columns)
            //{
            //    jsonView.Columns.Add(col.Name);
            //    col.DataPropertyName = col.Name;
          //  }

            //dataGridView1.DataSource = jsonView;
        }


        public void clearDevicenameList()
        {
            this.devicenameList.Clear();
        }
        public String insertDeviceName
        {
            get { return null; }
            set
            {
                bool flag1 = false;
                if (value == null) return;
                if (value.Contains("DeviceName:"))
                {
                    string[] words = value.Split(' ');
                    words = words.Where(w => w != words[0]).ToArray();
                    string deviceName = "";
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (i < words.Length - 1)
                            deviceName += words[i] + "-";
                        else
                            deviceName += words[i];
                    }
                    foreach (string name in devicenameList)
                    {
                        if (name == deviceName)
                        {
                            flag1 = true;
                        }
                    }
                    if(flag1==false) devicenameList.Add(deviceName);
                    //if(flag1==false)devicename.Items.Add(deviceName); 
                    addTabcontrol(deviceName);

                }
              
                
            }
        }

        private void addTabcontrol(string devicename)
        {


            DataGridView DatagridView = new DataGridView();
            
            TabPage myTabPage = new TabPage(devicename); //Create each tab
            DataTable tabview = new DataTable();
            DatagridView.Columns.Add("Date", "Date");
            DatagridView.Columns.Add("Device", "Device");
            DatagridView.Columns.Add("Process", "Process");
            DatagridView.Columns.Add("LogLevel", "LogLevel");
            DatagridView.Columns.Add("Log", "Log");

            DatagridView.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DatagridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //DatagridView.Height = 480;
            DatagridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewColumn col in DatagridView.Columns)
            {
                tabview.Columns.Add(col.Name);
                col.DataPropertyName = col.Name;
            }
            
            DatagridView.Anchor = (AnchorStyles.Top | AnchorStyles.Right|AnchorStyles.Bottom|AnchorStyles.Left);
            DatagridView.MaximumSize= new System.Drawing.Size(tabControl1.Width-20, tabControl1.Height-30);
            DatagridView.BringToFront();
            DatagridView.CellFormatting += dataGridView_CellFormatting;
            DatagridView.ScrollBars = ScrollBars.Vertical;
            DatagridView.DataSource = tabview;
            DatagridView.ContextMenuStrip = mnu;
            //add it to the list.
            dataGrids.Add(devicename, DatagridView);
            dataTables.Add(devicename, tabview);
            tabpages.Add(devicename, myTabPage);
            
            myTabPage.Controls.Add(DatagridView);
            myTabPage.AutoScroll = true;
            tabControl1.TabPages.Add(myTabPage);

        }

       
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "LogLevel")
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString().Contains("<Notice>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.ForestGreen;
                    }
                    else if (e.Value.ToString().Contains("<Debug>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (e.Value.ToString().Contains("<Info>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.AntiqueWhite;
                    }
                    else if (e.Value.ToString().Contains("<Warning>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (e.Value.ToString().Contains("<Error>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Critical>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Alert>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Emergency>:"))
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.IndianRed;
                    }
                    else
                    {
                        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;

                    }

                }
            }
           
        }
        public void insertToDataSource(string value, string devicename)
        {
            
                if (value == null)  return;
                int index = 0;
                int previous = 0;
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                string[] loglevels = { "<Notice>:", "<Debug>:", "<Info>:", "<Warning>:", "<Error>:", "<Critical>:", "<Alert>:", "<Emergency>:" };
                int rowNumber = 0;



                if (month.Any(e => value.StartsWith(e))) //Start with one of the month code
                {
                    dataTables[devicename].Rows.Add();
                    addtRow = 0;
                    //row init
                    rowNumber= dataTables[devicename].Rows.Count-1;
                    int dataGDrowNo = dataTables[devicename].Rows.Count - 1;
                    dataTables[devicename].Rows[rowNumber][0] = "";
                    dataTables[devicename].Rows[rowNumber][1] = "";
                    dataTables[devicename].Rows[rowNumber][2] = "";
                    dataTables[devicename].Rows[rowNumber][3] = "";
                    dataTables[devicename].Rows[rowNumber][4] = "";


                    int midIndex = 0;

                    string[] words = value.Split(' ');
                    foreach (string loglevel in loglevels)
                    {
                        if (Array.IndexOf(words, loglevel) != -1)
                            midIndex = Array.IndexOf(words, loglevel);
                    }
                   
                    if (!(devicenameList.Any(e => words.Contains(e))))
                    {
                        previous++;
                        foreach (string word in words)
                        {
                            dataTables[devicename].Rows[rowNumber - previous][4] += word + "";
                        }
                    }
                    int newline = 0;
                    foreach (string word in words)
                    {
                        
                        if (month.Any(e => word.StartsWith(e)))
                        {
                            firstRowf = true;
                        }

                        if (firstRowf == true && index < midIndex - 2) //Date
                            dataTables[devicename].Rows[rowNumber][0] += word + " ";
                        else if (firstRowf == true && devicenameList.Any(e => word.Contains(e))) //Device
                        {
                            dataTables[devicename].Rows[rowNumber][1] += word + " ";
                        }
                        else if (firstRowf == true && index == midIndex - 1) //Process
                        {
                            dataTables[devicename].Rows[rowNumber][2] += word + " ";
                            if (!(processlist.Contains(word)))
                            {
                                processlist.Add(word);
                                processlistname.Items.Add(word);
                            }
                        }
                        else if (firstRowf == true && (loglevels.Any(e => word.Contains(e)))) //LogLevel
                        {

                            dataTables[devicename].Rows[rowNumber][3] += word + " ";
                        }
                        else if (firstRowf == true && index > midIndex)//Log
                        {
                            dataTables[devicename].Rows[rowNumber][4] += word + " ";

                        }
                        index++;
                    }
                    firstRowf = false;
                    scrollToBottom();
                }
                else //If current row contain multiple line, this logic counts on the multi row and record such lines of log. 
                {
                    rowNumber = dataTables[devicename].Rows.Count - 1;
                    
                        addtRow++;

                if (rowNumber - addtRow > 0)
                {
                    dataTables[devicename].Rows[rowNumber][4] += Environment.NewLine + value.ToString();
                }
                    scrollToBottom();
                }
            
        }

        public static string EscapeLikeValue(string searchterm)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < searchterm.Length; i++)
            {
                char c = searchterm[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private void scrollToBottom()
        {
            if (dataGrids.ContainsKey(tabControl1.TabPages[tabControl1.SelectedIndex].Text) == false) return;

            if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].FirstDisplayedScrollingRowIndex == 0 || dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].FirstDisplayedScrollingRowIndex == -1)
                return;
            if (fixScrollCheck != true)
            {
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].FirstDisplayedScrollingRowIndex = dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].RowCount - 1;
            }
        }

        

        private void highlightBtn_Click(object sender, EventArgs e)
        {
            string search = highlightTextBox.Text;
            if (search == "")
                return;
            else
                highLight(search);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveToJson(tabControl1.TabPages[tabControl1.SelectedIndex].Text);
        }
        private void loadBtn_click(object sender, EventArgs e)
        {
            loadFromJSON();
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string search = EscapeLikeValue(searchTxtBox.Text);
            searchResult(search);
        }
        private void clearDataBtn_Click(object sender, EventArgs e)
        {
            dataTables[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Clear(); //Clear all virtual table.
        }
        private void copyMnu_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^{c}");
        }
        private void addProcess_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Add Process", "add Process", "Default", -1, -1);
            if (input.Length > 0)
            {
                processlistname.Items.Add(input);
            }
            
        }

        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            searchTxtBox.Text = "";
            highlightTextBox.Text = "";
            clearFilter();
        }
        private void fixScroll_Click(object sender, EventArgs e)
        {
            if (fixScroll.Checked)
                fixScrollCheck = true;
            else
                fixScrollCheck = false;
        }
        private void clearFilter()
        {
            totalSelected.Clear();
            filterApplied = false;
            //foreach (int i in devicename.CheckedIndices)
           // {
            //    devicename.SetItemCheckState(i, CheckState.Unchecked);
           // }
            foreach (int i in processlistname.CheckedIndices)
            {
                processlistname.SetItemCheckState(i, CheckState.Unchecked);
            }
            foreach (int i in loglevelCheckBox.CheckedIndices)
            {
                loglevelCheckBox.SetItemCheckState(i, CheckState.Unchecked);
            }
            for (int i = 0; i < dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows.Count; i++)
            {
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.White;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.White;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.White;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.White;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.White;

            }
            dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].DataSource = dataTables[tabControl1.TabPages[tabControl1.SelectedIndex].Text];
        }
        private void highLight(string term)
        {
            
            if (term != null)
            {
                int i = 0;
                while (i < dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows.Count - 1)
                {
                    if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[3] == null)
                    {
                        i++;
                        continue;
                    }

                    else if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[2].Value.ToString().Contains(term) || dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[3].Value.ToString().Contains(term) || dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[4].Value.ToString().Contains(term))
                    {
                        if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[2].Value.ToString().Contains(term))
                        {
                            dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Yellow;
                        }

                        if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[3].Value.ToString().Contains(term))
                            dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Yellow;
                        if (dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[4].Value.ToString().Contains(term))
                            dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Yellow;

                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }


        private void searchResult(string term)
        {
           string FilterRow = "";
            DataView dv = new DataView(dataTables[tabControl1.TabPages[tabControl1.SelectedIndex].Text]);
            if (filterApplied == true)
            {
                FilterRow = "( "+Filter+") "+" AND "+"( Date LIKE  '*" + term + "*'" + "or Device LIKE  '*" + term + "*'or  Process LIKE  '*" + term + "*' or LogLevel LIKE  '*" + term + "*' or Log LIKE  '*" + term + "*' )";
                dv.RowFilter = FilterRow;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].DataSource = dv;
            }
            else
            {
                
                FilterRow = "Date LIKE  '*" + term + "*'" + "or Device LIKE  '*" + term + "*'or  Process LIKE  '*" + term + "*' or LogLevel LIKE  '*" + term + "*' or Log LIKE  '*" + term + "*'";
                dv.RowFilter = FilterRow;
                dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].DataSource = dv;
            }
        }

        private void TotalcheckBox()
        {
            DataView dv = new DataView(dataTables[tabControl1.TabPages[tabControl1.SelectedIndex].Text]);
            filteredView = new DataView(dataTables[tabControl1.TabPages[tabControl1.SelectedIndex].Text]);
            int count = totalSelected.Count;
            int tempCounter1 = 1;
            int tempCounte2 = 1;
            int tempCounter3 = 1;
            bool deviceV, processV, logV ;
            deviceV = false;
            processV = false;
            logV = false;
            String Filter1 ="";
            String Filter2 = "";
            String Filter3 = "";
            String trimming = "";
            foreach (string term in chkdevice)
            {
                deviceV = true;
                if (tempCounter1!= chkdevice.Count)
                    Filter1 += "Device LIKE '*" + term + "*' OR ";
                else
                    Filter1 += "Device LIKE '*" + term + "*'";
                tempCounter1++;
            }
        
            foreach (string term in chkprocess)
            {
                if (term.IndexOf('[') != -1)
                {
                    trimming = term.Remove(term.IndexOf('['));
                }
                else
                    trimming = term;

                processV = true;
                if (tempCounte2 != chkprocess.Count)
                    Filter2 += "Process LIKE '*" + trimming + "*' OR ";
                else
                    Filter2 += "Process LIKE '*" + trimming + "*'";
                tempCounte2++;

            }
            foreach (string term in chkloglevel)
            {
                logV = true;
                if (tempCounter3 != chkloglevel.Count)
                    Filter3 += "LogLevel LIKE '*" + term + "*' OR ";
                else
                    Filter3 += "LogLevel LIKE '*" + term + "*'";
                tempCounter3++;
            }

            if (deviceV == true && processV == false && logV == false) Filter = Filter1;
            else if (deviceV == false && processV == true && logV == false) Filter = Filter2;
            else if (deviceV == false && processV == false && logV == true) Filter = Filter3;
            else if (deviceV == true && processV == true && logV == false) Filter = "("+ Filter1 + ")"+" AND " + "(" + Filter2+")";
            else if (deviceV == false && processV == true && logV == true) Filter = "("+ Filter2 + ")"+" AND " + "("+ Filter3 +")";
            else if (deviceV == true && processV == false && logV == true) Filter = "(" + Filter1 + ")"+" AND " +"(" + Filter3+")";
            else if (deviceV == true && processV == true && logV == true) Filter = "("+Filter1 +")"+ " AND " +"("+ Filter2 + ")"+" AND " +"(" +Filter3+")";

           // MessageBox.Show(Filter);
            dv.RowFilter = Filter;
            filteredView.RowFilter = Filter;
            dataGrids[tabControl1.TabPages[tabControl1.SelectedIndex].Text].DataSource = dv;
        }

      

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void checkbox_SelectedIndexChanged(object sender, EventArgs Args)
        {
            if (processlistname.CheckedItems.Count == 0 && loglevelCheckBox.CheckedItems.Count == 0)
            {
                filterApplied = false;
                chkprocess.Clear();
                chkdevice.Clear();
                chkloglevel.Clear();
                clearFilter();
            }
            else
            {
                filterApplied = true;
                chkprocess.Clear();
                chkdevice.Clear();
                chkloglevel.Clear();
                foreach (int indexChecked in processlistname.CheckedIndices)
                {
                    chkprocess.Add(processlistname.Items[indexChecked].ToString());
                }
                //foreach (int indexChecked in devicename.CheckedIndices)
                //{
                 //   chkdevice.Add(devicename.Items[indexChecked].ToString());
                //}
                foreach (int indexChecked in loglevelCheckBox.CheckedIndices)
                {
                    chkloglevel.Add(loglevelCheckBox.Items[indexChecked].ToString());
                }
                TotalcheckBox();
            }
        }

       

        private void loglevelCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void iosSyslogger_Load(object sender, EventArgs e)
        {
 


        }

  
        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
          
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
