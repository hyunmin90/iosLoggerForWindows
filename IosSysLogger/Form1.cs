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
        bool firstRowf = false;
        bool configurationNotice = false;
        bool fixScrollCheck = false;
        public iosSyslogger()
        {
            
            InitializeComponent();
            
            string currentPath = System.Environment.CurrentDirectory;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            this.clearSearchBtn.Click += new System.EventHandler(this.clearSearchBtn_Click);
            this.fixScroll.Click += new System.EventHandler(this.fixScroll_Click);

        }

        public string TextBoxText
        {
            get { return null; }
            set
            {
                int index = 0;
                bool highlightNew = false;
                string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                if (month.Any(e => value.StartsWith(e))) //Start with one of the month code
                {

                    dataGridView1.Rows.Add();
                    //richTextBox1.AppendText(value + Environment.NewLine);

                    string[] words = value.Split(' ');
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

                            if (firstRowf == true && index < 4) //Date
                                dataGridView1.Rows[rowNumber].Cells[0].Value += word + " ";
                            else if (firstRowf == true && index == 4) //Device
                                dataGridView1.Rows[rowNumber].Cells[1].Value += word + " ";
                            else if (firstRowf == true && index == 5) //Process
                                dataGridView1.Rows[rowNumber].Cells[2].Value += word + " ";
                            else if (firstRowf == true && (index == 6 || word.Contains(">:"))) //LogLevel
                                dataGridView1.Rows[rowNumber].Cells[3].Value += word + " ";
                            else if (firstRowf == true && index > 6)//Log
                                dataGridView1.Rows[rowNumber].Cells[4].Value += word + " ";
                            index++;
                        }
                    if (dataGridView1.Rows[rowNumber].Cells[4].Value.ToString().Contains("Configuration Notice:"))
                    {
                        configurationNotice = true;
                    }
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
                    firstRowf = false;
                    rowNumber++;
                    scrollToBottom();

                    //dataGridView1.Rows[0].Cells[4].Value = value;
                }
                else
                {
                    //MessageBox.Show(value); Need to solve multi line problem
                }
                
                


            }

        }

        private void scrollToBottom()
        {
            if(fixScrollCheck != true)
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
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
                if (dataGridView1.Rows[i].Cells[3] == null)
                { return; }
                
                    dataGridView1.Rows[i].Selected = false;
                    dataGridView1.Rows[i].Visible = true;
                

                
            }
        }
        private void highLight(string term)
        {
            
            if (term != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3] == null)
                    { return; }
                    else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(term) || dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(term))
                    {
                        dataGridView1.Rows[i].Selected = true;
                        dataGridView1.Rows[i].Visible = true;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                        dataGridView1.Rows[i].Selected = false;
                    }


                }
            }


        }

        

        // Reset the richtextbox when user changes the search string
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            start = 0;
            indexOfSearchText = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
