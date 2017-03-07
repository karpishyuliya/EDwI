using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1_EDwI
{
    public partial class Form1 : Form
    {
        string HTMLtext;
        string modifyText;

        public Form1()
        {
            InitializeComponent();
        }

        // Get button
        private void button1_Click(object sender, EventArgs e)
        {
            HTMLtext = "";
            string Url = textBox1.Text;
            if(Url!=null)
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                HTMLtext = sr.ReadToEnd();
                modifyText = "";

                sr.Close();
                myResponse.Close();

            }

        }

        // Save button
        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|html files (*.html)|*.html";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = "save Type: "+saveFileDialog1.FilterIndex.ToString()+"\n";
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter writer = new StreamWriter(myStream);
                    // Save without tags
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        modifyText = Regex.Replace(HTMLtext, "<[^>]+>", " ");// regular expression removing tags
                        modifyText = Regex.Replace(modifyText, @"[^\w\s]", ""); // regular expression removing punctuation
                        modifyText = modifyText.ToLower(); // replacing all uppercase to lowercase

                        richTextBox1.Text += modifyText; // write to window
                    }
                    // Save as html file
                    else if (saveFileDialog1.FilterIndex == 2)
                    {
                        var result = HTMLtext.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in result)
                        {
                            richTextBox1.Text += line; // write to window
                            writer.WriteLine(line); // write to file
                        }
                    }  
                    myStream.Close();
                }
            }
            //richTextBox1.Text += HTMLtext;
        }

        

        
    }
}
