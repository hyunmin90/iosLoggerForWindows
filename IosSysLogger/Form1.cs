using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Management;

namespace IosSysLogger
{
    public partial class iosSyslogger : Form
    {
        int start = 0;
        int indexOfSearchText = 0;
        int rowNumber = 0;
        int addtRow = 0;
        bool firstRowf = false;
        bool configurationNotice = false;
        bool fixScrollCheck = false;
        List<string> selectedLoglevel = new List<string>();
        List<string> processlist = new List<string>();
        List<string> devicenameList = new List<string>();
        Dictionary<string, string> totalSelected = new Dictionary<string, string>();

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

        private void Save()
        {
            string currentPath = System.Environment.CurrentDirectory;


            DataTable dt = new DataTable();
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                DataColumn column = new DataColumn(dataGridView1.Columns[i - 1].HeaderText);
                dt.Columns.Add(column);
            }
            int columnCount = dataGridView1.Columns.Count;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                DataRow dataRow = dt.NewRow();
                for (int i = 0; i < columnCount; i++)
                {
                    dataRow[i] = dr.Cells[i].Value;
                }
                dt.Rows.Add(dataRow);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter xmlSave = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8);
                ds.WriteXml(xmlSave);
                xmlSave.Close();
            }

        }

        private void loadXML()
        {
            DataSet dataSet = new DataSet();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML-File | *.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataSet.ReadXml(openFileDialog.FileName);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
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


        public string insertLogText
        {
            get { return null; }
            set
            {
                if (value == null) return;
                int index = 0;
                int previous = 0;
                bool searchedText = false;
                bool highLighted = false;
                bool withinCheckNew = false;
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                string[] loglevels = { "<Notice>:", "<Debug>:", "<Info>:", "<Warning>:", "<Error>:", "<Critical>:", "<Alert>:", "<Emergency>:" };
                if (month.Any(e => value.StartsWith(e))) //Start with one of the month code
                {
                    dataGridView1.Rows.Add();
                    addtRow = 0;
                    //row init
                    dataGridView1.Rows[rowNumber].Cells[6].Value = "";
                    dataGridView1.Rows[rowNumber].Cells[5].Value = "0";
                    dataGridView1.Rows[rowNumber].Cells[0].Value = "";
                    dataGridView1.Rows[rowNumber].Cells[1].Value = "";
                    dataGridView1.Rows[rowNumber].Cells[2].Value = "";
                    dataGridView1.Rows[rowNumber].Cells[3].Value = "";
                    dataGridView1.Rows[rowNumber].Cells[4].Value = "";
                    

                    int midIndex = 0;

                    string[] words = value.Split(' ');
                    foreach (string loglevel in loglevels)
                    {
                        if (Array.IndexOf(words, loglevel) != -1)
                            midIndex = Array.IndexOf(words, loglevel);
                    }

                    if (value.Contains("<Notice>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "ForestGreen";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.ForestGreen;
                    }
                    else if (value.Contains("<Debug>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "Orange";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (value.Contains("<Info>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "Orange";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.AntiqueWhite;
                    }
                    else if (value.Contains("<Warning>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "Orange";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (value.Contains("<Error>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "DarkRed";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (value.Contains("<Critical>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "DarkRed";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (value.Contains("<Alert>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "DarkRed";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (value.Contains("<Emergency>:"))
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "IndianRed";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.IndianRed;
                    }
                    else
                    {
                        dataGridView1.Rows[rowNumber].Cells[6].Value = "Black";
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Black;

                    }
                    if (!(devicenameList.Any(e => words.Contains(e))))
                    {
                        previous++;
                        foreach (string word in words)
                        {
                            dataGridView1.Rows[rowNumber - previous].Cells[4].Value += word + "";
                        }
                    }
                    int switchindex = 0;
                    foreach (string word in words)
                    {
                        if (searchTxtBox.Text != "" && word.Contains(searchTxtBox.Text))
                        {
                            searchedText = true;
                        }
                        if ((highlightTextBox.Text != "" && word.Contains(highlightTextBox.Text)))
                        {
                            highLighted = true;
                        }
                        if (month.Any(e => word.StartsWith(e)))
                        {
                            firstRowf = true;
                        }

                        if (firstRowf == true && index < midIndex - 2) //Date
                            dataGridView1.Rows[rowNumber].Cells[0].Value += word + " ";
                        else if (firstRowf == true && devicenameList.Any(e => word.Contains(e))) //Device
                        {
                            dataGridView1.Rows[rowNumber].Cells[1].Value += word + " ";
                            switchindex = 1;
                        }
                        else if (firstRowf == true && index == midIndex - 1) //Process
                        {
                            switchindex = 2;
                            dataGridView1.Rows[rowNumber].Cells[2].Value += word + " ";
                            if (!(processlist.Contains(word)))
                            {
                                processlist.Add(word);
                                processlistname.Items.Add(word);
                            }
                        }
                        else if (firstRowf == true && (loglevels.Any(e => word.Contains(e)))) //LogLevel
                        {

                            switchindex = 3;
                            dataGridView1.Rows[rowNumber].Cells[3].Value += word + " ";
                        }
                        else if (firstRowf == true && index > midIndex)//Log
                        {
                            switchindex = 4;
                            dataGridView1.Rows[rowNumber].Cells[4].Value += word + " ";
                            dataGridView1.Rows[rowNumber].Cells[5].Value = 0;
                        }
                        index++;
                    }
                    if (highLighted == true)
                    {
                        switch (switchindex)
                        {
                            case 1:
                                dataGridView1.Rows[rowNumber].Cells[1].Style.BackColor = Color.Yellow;
                                dataGridView1.Rows[rowNumber].Visible = true;
                                break;
                            case 2:
                                dataGridView1.Rows[rowNumber].Cells[2].Style.BackColor = Color.Yellow;
                                dataGridView1.Rows[rowNumber].Visible = true;
                                break;
                            case 3:
                                dataGridView1.Rows[rowNumber].Cells[3].Style.BackColor = Color.Yellow;
                                dataGridView1.Rows[rowNumber].Visible = true;
                                break;
                            case 4:
                                dataGridView1.Rows[rowNumber].Cells[4].Style.BackColor = Color.Yellow;
                                dataGridView1.Rows[rowNumber].Visible = true;
                                break;
                        }
                    }
                    if (searchedText == true && dataGridView1.Rows[rowNumber].Visible == true)
                    {
                        dataGridView1.Rows[rowNumber].Visible = true;
                    }
                    else if (searchedText == false && (searchTxtBox.Text != ""))
                    {
                        dataGridView1.Rows[rowNumber].Visible = false;
                        dataGridView1.Rows[rowNumber].Selected = false;
                    }
                    if (withinCheckNew == false && totalSelected.Count > 0 && dataGridView1.Rows[rowNumber].Visible!=false) //Need to fix this very soon For performance reason
                    {
                        foreach (KeyValuePair<string, string> entry in totalSelected)
                        {

                            if (entry.Value == "devicename" && dataGridView1.Rows[rowNumber].Cells[1].Value != null)
                            {

                                if (dataGridView1.Rows[rowNumber].Cells[1].Value.ToString().Trim().Equals(entry.Key.Trim()))
                                    dataGridView1.Rows[rowNumber].Visible = true;
                                else
                                {
                                    dataGridView1.Rows[rowNumber].Visible = false;
                                    break;
                                }
                            }
                            else if (entry.Value == "process" && dataGridView1.Rows[rowNumber].Cells[2].Value != null)
                            {
                                if (dataGridView1.Rows[rowNumber].Cells[2].Value.ToString().Trim().Equals(entry.Key.Trim()))
                                    dataGridView1.Rows[rowNumber].Visible = true;
                                else
                                {
                                    dataGridView1.Rows[rowNumber].Visible = false;
                                    break;
                                }
                            }
                            else if (entry.Value == "loglevel" && dataGridView1.Rows[rowNumber].Cells[3].Value != null)
                            {
                                if (dataGridView1.Rows[rowNumber].Cells[3].Value.ToString().Trim().Equals(entry.Key.Trim()))
                                    dataGridView1.Rows[rowNumber].Visible = true;
                                else
                                {
                                    dataGridView1.Rows[rowNumber].Visible = false;
                                    break;
                                }
                            }
                            // do something with entry.Value or entry.Key
                        }

                      

                        

                    }

                    firstRowf = false;
                    rowNumber++;
                    scrollToBottom();
                }
                else //If current row contain multiple line, this logic counts on the multi row and record such lines of log. 
                {
                    dataGridView1.Rows.Add();
                    if (rowNumber == 0) addtRow = 0;
                    else
                        addtRow++;
                    dataGridView1.Rows[rowNumber].Cells[0].Value = " ";
                    dataGridView1.Rows[rowNumber].Cells[1].Value = " ";
                    dataGridView1.Rows[rowNumber].Cells[2].Value = " ";
                    dataGridView1.Rows[rowNumber].Cells[3].Value = " ";
                    dataGridView1.Rows[rowNumber].Cells[6].Value = "Black";
                    dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[rowNumber].Cells[4].Value = value.ToString();
                    dataGridView1.Rows[rowNumber - addtRow].Cells[5].Value = addtRow;
                    dataGridView1.Rows[rowNumber].Cells[5].Value = "0";
                    if (dataGridView1.Rows[rowNumber - addtRow].Visible == false)
                        dataGridView1.Rows[rowNumber].Visible = false;
                    rowNumber++;
                    scrollToBottom();
                }

                //For filtering
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
            Save();
        }
        private void loadBtn_click(object sender, EventArgs e)
        {

            //ON LOAD BUTTON NEED TO CLEAR EVERYTHING OUT !!! KILL ALL THE PROCESS (BC ITS GONNA GET MESSSY)
            dataGridView1.Rows.Clear();
            loadXML();
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string search = searchTxtBox.Text;
            if (search == "")
                return;
            else
                searchResult(search);
        }
        private void clearDataBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            rowNumber = 0;
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

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.White;
                dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.White;
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.White;
                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.White;
                dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.White;
                dataGridView1.Rows[i].Visible = true;
            }
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
                    if (dataGridView1.Rows[i].Visible == false)
                    {
                        i++;
                        continue;
                    }
                    else if (dataGridView1.Rows[i].Visible == true && dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(term))
                    {

                        string multirow = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        int count = Convert.ToInt32(multirow);
                        if (count > 0)
                        {
                            int z = 0;
                            for (z = 0; z <= count; z++)
                            {

                                dataGridView1.Rows[i + z].Visible = true;
                            }
                            i = i + z;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Visible = true;
                            i++;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                        i++;
                    }
                }
            }
        }


        

        private void ShowTotal()
        {
            
            
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Visible = false;
                int count1,count2,count3=0;
                count1 = 1;
                count2 = 1;
                count3 = 1;
                int z = 0;

                foreach (KeyValuePair<string, string> entry in totalSelected)
                {

                    if (entry.Value == "devicename" && dataGridView1.Rows[i].Cells[1].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().Equals(entry.Key.Trim()))
                        {
                            string multirow1 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            int counts = Convert.ToInt32(multirow1);
                            if (counts > 0)
                            {
                                for (z = 0; z < counts; z++)
                                {

                                    dataGridView1.Rows[i + z].Visible = true;
                                }
                               
                            }
                            else
                            {
                                dataGridView1.Rows[i].Visible = true;
                            }
                        }
                        else if (devicename.CheckedItems.Count > 1&&count1!= devicename.CheckedItems.Count)
                        {
                            count1++;
                            continue;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Visible = false;
                            break;
                        }
                    }
                    else if (entry.Value == "process" && dataGridView1.Rows[i].Cells[2].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().Equals(entry.Key.Trim()))
                        {
                            string multirow1 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            int counts = Convert.ToInt32(multirow1);
                            if (counts > 0)
                            {
                                for (z = 0; z < counts; z++)
                                {

                                    dataGridView1.Rows[i + z].Visible = true;
                                }
                               
                            }
                            else
                            {
                                dataGridView1.Rows[i].Visible = true;
                            }
                        }
                        else if (processlistname.CheckedItems.Count > 1 && count2 != processlistname.CheckedItems.Count)
                        {
                            count2++;
                            continue;
                        }
                        else 
                        {
                            dataGridView1.Rows[i].Visible = false;
                            break;
                        }
                    }
                    else if (entry.Value == "loglevel" && dataGridView1.Rows[i].Cells[3].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[3].Value.ToString().Trim().Contains(entry.Key.Trim()))
                        {
                            string multirow1 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            int counts = Convert.ToInt32(multirow1);
                            if (counts > 0)
                            {
                                for (z = 0; z < counts; z++)
                                {

                                    dataGridView1.Rows[i + z].Visible = true;
                                }
                                
                            }
                            else
                            {
                                dataGridView1.Rows[i].Visible = true;
                            }
                            continue;
                        }
                        else if (loglevelCheckBox.CheckedItems.Count > 1 && count3 != loglevelCheckBox.CheckedItems.Count)
                        {
                            count3++;
                            continue;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Visible = false;
                            break;
                        }
                    }
                    // do something with entry.Value or entry.Key
                }
                string multirow = dataGridView1.Rows[i].Cells[5].Value.ToString();
                int count = Convert.ToInt32(multirow);
                if (count > 0&& dataGridView1.Rows[i].Visible==false)
                {
                    for (int k = 0; k <= count; k++)
                    {

                        dataGridView1.Rows[i + k].Visible = false;
                    }

                }

                i = i + z;

                


            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkbox_SelectedIndexChanged(object sender, EventArgs Args)
        {
            if (processlistname.CheckedItems.Count == 0 && devicename.CheckedItems.Count == 0 && loglevelCheckBox.CheckedItems.Count == 0)
            {
                totalSelected.Clear();
                clearFilter();
            }
            else
            {
                totalSelected.Clear();
                foreach (int indexChecked in processlistname.CheckedIndices)
                {
                    totalSelected.Add(processlistname.Items[indexChecked].ToString(), "process");
                }
                foreach (int indexChecked in devicename.CheckedIndices)
                {
                    totalSelected.Add(devicename.Items[indexChecked].ToString(), "devicename");
                }
                foreach (int indexChecked in loglevelCheckBox.CheckedIndices)
                {
                    totalSelected.Add(loglevelCheckBox.Items[indexChecked].ToString(), "loglevel");
                }
                ShowTotal();
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
