using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        List<string> checkedList = new List<string>();
        List<string> processlist = new List<string>();
        List<string> devicenameList = new List<string>();
        public iosSyslogger()
        {

            InitializeComponent();

            //Allow data grid t
            ContextMenuStrip mnu = new ContextMenuStrip();
            ToolStripMenuItem mnuCopy = new ToolStripMenuItem("Copy");
            ToolStripMenuItem mnuCut = new ToolStripMenuItem("Cut");
            ToolStripMenuItem mnuPaste = new ToolStripMenuItem("Paste");
            mnu.Items.AddRange(new ToolStripItem[] { mnuCopy, mnuCut, mnuPaste });
            dataGridView1.ContextMenuStrip = mnu;


            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            this.clearSearchBtn.Click += new System.EventHandler(this.clearSearchBtn_Click);
            this.fixScroll.Click += new System.EventHandler(this.fixScroll_Click);
            this.devicename.SelectedIndexChanged += new System.EventHandler(this.devicename_SelectedIndexChanged);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.processlistname.SelectedIndexChanged += new System.EventHandler(this.processlistname_SelectedIndexChanged);
           
        }
        public String uuidnameText
        {
            get { return null; }
            set
            {
                if (value == null) return;
                uuidname.AppendText(value);
            }
        }
        public String DeviceNameText
        {
            get { return null; }
            set
            {
                if (value == null) return;
                if (value.Contains("DeviceName:"))
                {
                    string[] words = value.Split(' ');
                    words = words.Where(w => w != words[0]).ToArray();
                    string deviceName="";
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (i < words.Length - 1)
                            deviceName += words[i] + "-";
                        else
                            deviceName += words[i];
                    }
                    devicenameList.Add(deviceName);
                    devicename.Items.Add(deviceName);
                    //devicename.Items.Add("Test");
                    //devicename.AppendText("\n");

                }

            }
        }
        

        public string TextBoxText
        {
            get { return null; }
            set
            {
                int index = 0;
                int previous = 0;
                bool highlightNew = false;
                bool withinCheckNew = false;
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                string[] loglevels = { "<Notice>:", "<Debug>:", "<Info>:", "<Warning>:", "<Error>:", "<Critical>:", "<Alert>:", "<Emergency>:"};
                
                if (month.Any(e => value.StartsWith(e))) //Start with one of the month code
                {
                    addtRow = 0;
                    dataGridView1.Rows[rowNumber].Cells[5].Value = "0";
                    dataGridView1.Rows.Add();
                    //richTextBox1.AppendText(value + Environment.NewLine);

                    int midIndex=0;

                    string[] words = value.Split(' ');
                    foreach (string loglevel in loglevels) {
                        if (Array.IndexOf(words, loglevel) != -1)
                            midIndex = Array.IndexOf(words, loglevel);
                    }
                   // MessageBox.Show(midIndex.ToString());

                    if (value.Contains("<Notice>:"))
                    {
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.ForestGreen;
                    }
                    else if (value.Contains("<Debug>:"))
                    {

                    }
                    else if (value.Contains("<Info>:"))
                    {

                    }
                    else if (value.Contains("<Warning>:"))
                    {
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (value.Contains("<Error>:"))
                    {
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (value.Contains("<Critical>:"))
                    {
                        dataGridView1.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (value.Contains("<Alert>:"))
                    {

                    }
                    else if (value.Contains("<Emergency>:"))
                    {

                    }
                    if (!(devicenameList.Any(e => words.Contains(e))))
                    {
                        previous++;
                        foreach (string word in words)
                        {
                            dataGridView1.Rows[rowNumber-previous].Cells[4].Value += word +"";
                        }
                   }

                    foreach (string word in words)
                    {
                        if (textBox2.Text != "" && word.Contains(textBox2.Text))
                        {
                            highlightNew = true;
                        }
                        if (month.Any(e => word.StartsWith(e)))
                        {
                            firstRowf = true;
                        }

                        if (firstRowf == true && index < midIndex - 2) //Date
                            dataGridView1.Rows[rowNumber].Cells[0].Value += word + " ";
                        else if (firstRowf == true && devicenameList.Any(e => word.Contains(e))) //Device
                            dataGridView1.Rows[rowNumber].Cells[1].Value += word + " ";
                        else if (firstRowf == true && index == midIndex - 1) //Process
                        {
                            dataGridView1.Rows[rowNumber].Cells[2].Value += word + " ";
                            if (!(processlist.Contains(word)))
                            {
                                processlist.Add(word);
                                processlistname.Items.Add(word);
                            }
                        }
                        else if (firstRowf == true && (loglevels.Any(e => word.Contains(e)))) //LogLevel
                            dataGridView1.Rows[rowNumber].Cells[3].Value += word + " ";
                        else if (firstRowf == true && index > midIndex)//Log
                        {
                            dataGridView1.Rows[rowNumber].Cells[4].Value += word + " ";
                            dataGridView1.Rows[rowNumber].Cells[5].Value = 0;
                        }
                        index++;
                    }
                    //if (dataGridView1.Rows[rowNumber].Cells[4].Value.ToString().Contains("Configuration Notice:"))
                    //{
                       // configurationNotice = true;
                   // }
                    if (highlightNew == true)
                    {
                        dataGridView1.Rows[rowNumber].Selected = true;
                        dataGridView1.Rows[rowNumber].Visible = true;
                    }
                    else if (highlightNew == false && textBox2.Text != "")
                    {
                        dataGridView1.Rows[rowNumber].Visible = false;
                        dataGridView1.Rows[rowNumber].Selected = false;
                    }
                    //MessageBox.Show(checkedListBox1.CheckedItems.Count.ToString());
                    if (withinCheckNew == false&& checkedListBox1.CheckedItems.Count!=0)
                    {
                        dataGridView1.Rows[rowNumber].Visible = false;
                        dataGridView1.Rows[rowNumber].Selected = false;
                    }
                    firstRowf = false;
                    rowNumber++;
                    scrollToBottom();
                    //dataGridView1.Rows[0].Cells[4].Value = value;
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
                    dataGridView1.Rows[rowNumber].Cells[4].Value = value.ToString();
                    dataGridView1.Rows[rowNumber - addtRow ].Cells[5].Value = addtRow;
                    dataGridView1.Rows[rowNumber].Cells[5].Value = "0";
                    rowNumber++;
                    scrollToBottom();
                    //MessageBox.Show(value); Need to solve multi line problem
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string search = textBox2.Text; 
            highLight(search);   
        }
        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            clearHighlight();   
        }
        private void fixScroll_Click(object sender, EventArgs e)
        {
            if (fixScroll.Checked)
                fixScrollCheck = true;
            else
                fixScrollCheck = false;
        }
        private void clearHighlight()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                    dataGridView1.Rows[i].Selected = false;
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
                    { return; }

                    else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(term))
                    {
                        
                        string multirow = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        int count = Convert.ToInt32(multirow);
                        if (count > 0)
                        {
                            int z = 0;
                           // MessageBox.Show(count.ToString());
                            for (z = 0; z <= count; z++)
                            {
                                
                                dataGridView1.Rows[i+z].Selected = true;
                                dataGridView1.Rows[i+z].Visible = true;
                            }
                            i=i+z;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Selected = true;
                            dataGridView1.Rows[i].Visible = true;
                            i++;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                        dataGridView1.Rows[i].Selected = false;
                        i++;
                    }
                    

                }
            }
        }

        private void showByProcess()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Visible = false;
                dataGridView1.Rows[i].Selected = false;
            }
            foreach (string term in processlist)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[2] == null)
                        return;
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term) )
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                }
            }
            processlist.Clear();

        }
        private void showByLogLevel()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Visible = false;
                dataGridView1.Rows[i].Selected = false;
            }
            foreach (string term in checkedList)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value == null)
                        return;
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term))
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                }
            }
            checkedList.Clear();

        }

        private void showBydeviceName()
        {
            for (int i = 0; i<dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].Visible = false;
                dataGridView1.Rows[i].Selected = false;
            }
            foreach (string term in devicenameList)
            {
                for (int i = 0; i<dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value == null)
                        return;
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(term))
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                }
            }
            devicenameList.Clear();

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

        private void processlistname_SelectedIndexChanged(object sender, EventArgs Args)
        {
            if (processlistname.CheckedItems.Count == 0)
            {
                clearHighlight();
            }
            else
            {
                foreach (int indexChecked in processlistname.CheckedIndices)
                {
                    // The indexChecked variable contains the index of the item.
                    string selectedName = processlistname.Items[indexChecked].ToString();
                    //MessageBox.Show(selectedName);

                    processlist.Add(selectedName);
                    //MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" +
                    // devicename.GetItemCheckState(indexChecked).ToString() + ".");
                }
                showByProcess();
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                clearHighlight();
            }
            else
            {
                foreach (int indexChecked in checkedListBox1.CheckedIndices)
                {
                    // The indexChecked variable contains the index of the item.
                    string selectedName = checkedListBox1.Items[indexChecked].ToString();
                    //MessageBox.Show(selectedName);

                    checkedList.Add(selectedName);
                    //MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" +
                    // devicename.GetItemCheckState(indexChecked).ToString() + ".");
                }
                showByLogLevel();
            }


        }
        private void devicename_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (devicename.SelectedItems.Count > 0)
            //{
            // MessageBox.Show(devicename.SelectedIndex.ToString());
            //string selectedName = devicename.GetItemText(devicename.SelectedIndex);
            //highLight(selectedName);
            //}
            if (devicename.CheckedItems.Count == 0)
            {
                clearHighlight();
            }
            else
            {
                foreach (int indexChecked in devicename.CheckedIndices)
                {
                    // The indexChecked variable contains the index of the item.
                    string selectedName = devicename.Items[indexChecked].ToString();
                    //MessageBox.Show(selectedName);

                    devicenameList.Add(selectedName);
                    //MessageBox.Show("Index#: " + indexChecked.ToString() + ", is checked. Checked state is:" +
                                   // devicename.GetItemCheckState(indexChecked).ToString() + ".");
                }
                showBydeviceName();
            }
            
        }
    }

}
