using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Management;
using Newtonsoft.Json;

namespace IosSysLogger
{
    public partial class iosSyslogger : Form
    {
        int start = 0;
        int indexOfSearchText = 0;
        int addtRow = 0;
        bool firstRowf = false;
        bool fixScrollCheck = false;
        bool filterApplied = true;
        String Filter = "";

        DataView filteredView = new DataView();
        DataTable logParserView = new DataTable();
        DataTable jsonView = new DataTable();

        List<string> selectedLoglevel = new List<string>();
        List<string> processlist = new List<string>();
        List<string> devicenameList = new List<string>();
        Dictionary<string, string> totalSelected = new Dictionary<string, string>();

        List<string> chkdevice = new List<string>();
        List<string> chkprocess = new List<string>();
        List<string> chkloglevel = new List<string>();

        public iosSyslogger()
        {
            InitializeComponent();
            var watcher = new ManagementEventWatcher();

            //Allow data copy here 
            ContextMenuStrip mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Copy");
            mnu.Items.AddRange(new ToolStripItem[] { mnuCopy });
            dataGridView1.ContextMenuStrip = mnu;
            mnuCopy.Click += new EventHandler(copyMnu_Click);

            

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                logParserView.Columns.Add(col.Name);
                col.DataPropertyName = col.Name;
            }

            dataGridView1.DataSource = logParserView;
            //Check boxes handler
            this.devicename.SelectedIndexChanged += new System.EventHandler(this.checkbox_SelectedIndexChanged);
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
            devicename.Items.Clear();
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



        private void saveToJson()
        {
            string json = JsonConvert.SerializeObject(logParserView);
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

            dataGridView1.DataSource = jsonView;
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
                    if(flag1==false)devicename.Items.Add(deviceName); 
                    
                }
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "LogLevel")
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString().Contains("<Notice>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.ForestGreen;
                    }
                    else if (e.Value.ToString().Contains("<Debug>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (e.Value.ToString().Contains("<Info>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.AntiqueWhite;
                    }
                    else if (e.Value.ToString().Contains("<Warning>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (e.Value.ToString().Contains("<Error>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Critical>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Alert>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (e.Value.ToString().Contains("<Emergency>:"))
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.IndianRed;
                    }
                    else
                    {
                        this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;

                    }

                }
            }
           
        }
        public string insertToDataSource
        {
            get { return null; }
            set
            {
                if (value == null) return;
                int index = 0;
                int previous = 0;
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                string[] loglevels = { "<Notice>:", "<Debug>:", "<Info>:", "<Warning>:", "<Error>:", "<Critical>:", "<Alert>:", "<Emergency>:" };
                int rowNumber = 0;
                if (month.Any(e => value.StartsWith(e))) //Start with one of the month code
                {
                    logParserView.Rows.Add();
                    addtRow = 0;
                    //row init
                    rowNumber=logParserView.Rows.Count-1;
                    int dataGDrowNo = dataGridView1.Rows.Count - 1;
                    logParserView.Rows[rowNumber][0] = "";
                    logParserView.Rows[rowNumber][1] = "";
                    logParserView.Rows[rowNumber][2] = "";
                    logParserView.Rows[rowNumber][3] = "";
                    logParserView.Rows[rowNumber][4] = "";
                    logParserView.Rows[rowNumber][5] = "0";
                    logParserView.Rows[rowNumber][6] = "";


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
                            logParserView.Rows[rowNumber - previous][4] += word + "";
                        }
                    }
                    int switchindex = 0;
                    foreach (string word in words)
                    {
                        
                        if (month.Any(e => word.StartsWith(e)))
                        {
                            firstRowf = true;
                        }

                        if (firstRowf == true && index < midIndex - 2) //Date
                            logParserView.Rows[rowNumber][0] += word + " ";
                        else if (firstRowf == true && devicenameList.Any(e => word.Contains(e))) //Device
                        {
                            logParserView.Rows[rowNumber][1] += word + " ";
                            switchindex = 1;
                        }
                        else if (firstRowf == true && index == midIndex - 1) //Process
                        {
                            switchindex = 2;
                            logParserView.Rows[rowNumber][2] += word + " ";
                            if (!(processlist.Contains(word)))
                            {
                                processlist.Add(word);
                                processlistname.Items.Add(word);
                            }
                        }
                        else if (firstRowf == true && (loglevels.Any(e => word.Contains(e)))) //LogLevel
                        {

                            switchindex = 3;
                            logParserView.Rows[rowNumber][3] += word + " ";
                        }
                        else if (firstRowf == true && index > midIndex)//Log
                        {
                            switchindex = 4;
                            logParserView.Rows[rowNumber][4] += word + " ";
                            logParserView.Rows[rowNumber][5] = 0;

                        }
                        index++;
                    }
                    firstRowf = false;
                    scrollToBottom();
                }
                else //If current row contain multiple line, this logic counts on the multi row and record such lines of log. 
                {
                    logParserView.Rows.Add();
                    rowNumber = logParserView.Rows.Count - 1;
                    if (rowNumber == 0) addtRow = 0;
                    else
                        addtRow++;
                    logParserView.Rows[rowNumber][0] = " ";
                    logParserView.Rows[rowNumber][1] = " ";
                    logParserView.Rows[rowNumber][2] = " ";
                    logParserView.Rows[rowNumber][3] = " ";
                    logParserView.Rows[rowNumber][4] = value.ToString();
                    logParserView.Rows[rowNumber][6] = "Black";
                    logParserView.Rows[rowNumber - addtRow][5] = addtRow;
                    logParserView.Rows[rowNumber][5] = "0";
                    scrollToBottom();
                }
            }
        }

        

        private void scrollToBottom()
        {
            if (fixScrollCheck != true)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
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
            saveToJson();
        }
        private void loadBtn_click(object sender, EventArgs e)
        {
            loadFromJSON();
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string search = searchTxtBox.Text;
            searchResult(search);
        }
        private void clearDataBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
        private void copyMnu_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^{c}");
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
            foreach (int i in devicename.CheckedIndices)
            {
                devicename.SetItemCheckState(i, CheckState.Unchecked);
            }
            foreach (int i in processlistname.CheckedIndices)
            {
                processlistname.SetItemCheckState(i, CheckState.Unchecked);
            }
            foreach (int i in loglevelCheckBox.CheckedIndices)
            {
                loglevelCheckBox.SetItemCheckState(i, CheckState.Unchecked);
            }
            dataGridView1.DataSource = logParserView;
        }
        private void highLight(string term)
        {

            if (term != null)
            {
                int i = 0;
                while (i < dataGridView1.Rows.Count - 1)
                {
                    if (dataGridView1.Rows[i].Cells[3] == null)
                    {
                        i++;
                        continue;
                    }

                    else if (dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(term))
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term))
                        {
                            dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Yellow;
                        }

                        if (dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term))
                            dataGridView1.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Yellow;
                        if (dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(term))
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Yellow;

                        string multirow = dataGridView1.Rows[i].Cells[5].Value.ToString();
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
            if (filterApplied == true)
            {
                filteredView.RowFilter = "( "+Filter+") "+" AND "+"( Date LIKE  '*" + term + "*'" + "or Device LIKE  '*" + term + "*'or  Process LIKE  '*" + term + "*' or LogLevel LIKE  '*" + term + "*' or Log LIKE  '*" + term + "*' )";
                MessageBox.Show(filteredView.RowFilter);
                dataGridView1.DataSource = filteredView;
            }
            else
            {
                DataView dv = new DataView(logParserView);
                dv.RowFilter = "Date LIKE  '*" + term + "*'" + "or Device LIKE  '*" + term + "*'or  Process LIKE  '*" + term + "*' or LogLevel LIKE  '*" + term + "*' or Log LIKE  '*" + term + "*'";
                dataGridView1.DataSource = dv;
            }
        }

        private void TotalcheckBox()
        {
            DataView dv = new DataView(logParserView);
            filteredView = new DataView(logParserView);
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
                processV = true;
                if (tempCounte2 != chkprocess.Count)
                    Filter2 += "Process LIKE '*" + term.Remove(term.IndexOf('[')) + "*' OR ";
                else
                    Filter2 += "Process LIKE '*" + term.Remove(term.IndexOf('[')) + "*'";
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
            dataGridView1.DataSource = dv;
        }

      

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void checkbox_SelectedIndexChanged(object sender, EventArgs Args)
        {
            if (processlistname.CheckedItems.Count == 0 && devicename.CheckedItems.Count == 0 && loglevelCheckBox.CheckedItems.Count == 0)
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
                foreach (int indexChecked in devicename.CheckedIndices)
                {
                    chkdevice.Add(devicename.Items[indexChecked].ToString());
                }
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
    }

}
