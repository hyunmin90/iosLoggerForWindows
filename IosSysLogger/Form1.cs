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
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            
            string currentPath = System.Environment.CurrentDirectory;


            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = currentPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;




            /*string[] fileContents;

            try
            {
                fileContents = File.ReadAllLines(currentPath + @"\syslog.txt");

                foreach (string line in fileContents)
                {
                    textBox1.AppendText(line+"\n");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
            Console.ReadLine();*/

        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            textBox1.AppendText("hello ah");
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string TextBoxText
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
