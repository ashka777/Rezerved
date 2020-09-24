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

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button_Stop.Enabled = false;
            textBox_From1.Enabled = true;
            numericUpDown1.Enabled = true;
            textBox_From2.Enabled = false;
            numericUpDown2.Enabled = false;
            textBox_From3.Enabled = false;
            numericUpDown3.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = Convert.ToInt32(numericUpDown_timer.Value * 1000 * 60);
            if (textBox_From1.Enabled == true)
            {
                Copy(textBox_From1.Text, textBox_To.Text, numericUpDown1);
            }
            if (textBox_From2.Enabled == true)
            {
                Copy(textBox_From2.Text, textBox_To.Text, numericUpDown2);
            }
            if (textBox_From3.Enabled == true)
            {
                Copy(textBox_From3.Text, textBox_To.Text, numericUpDown3);
            }
        }
        public void Copy(string Dir_from, string Dir_to, NumericUpDown numDay)
        {
            try
            {
                DirectoryInfo dir_inf = new DirectoryInfo(Dir_from);
                foreach (DirectoryInfo dir in dir_inf.GetDirectories())
                {
                    string folder_from = Dir_from + "\\" + dir.Name;
                    string folder_to = Dir_to + "\\" + dir.Name;
                    if (Directory.Exists(Dir_to + "\\" + dir.Name) != true)
                    {
                        Directory.CreateDirectory(Dir_to + "\\" + dir.Name);
                    }
                    Copy_Dir(folder_from, folder_to, numDay);
                }
            }
            catch (Exception ex)
            {
                label_Action.Text = (ex.Message);
            }
        }

        public void Copy_Dir(string folder_from, string folder_to, NumericUpDown numDay)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder_from);
                DirectoryInfo[] x = di.GetDirectories();
                foreach (DirectoryInfo dir_2 in x)
                {
                    int c = x.Length;
                    int a = Convert.ToInt16(dir_2.Name);
                    if (a >= c)  //if (a >=c-2)
                    {
                        Directory.CreateDirectory(folder_to + "\\" + a.ToString().PadLeft(2, '0'));
                        DirectoryInfo dr = new DirectoryInfo(folder_from + "\\" + a.ToString().PadLeft(2, '0') + "\\");
                        foreach (FileInfo fi in dr.GetFiles("*.csv"))
                        {
                            for (int val = 0; val < numDay.Value; val++)
                            {
                                DateTime tt = DateTime.Today.AddDays(-val);
                                if (fi.CreationTime.Date == tt)
                                {
                                    fi.CopyTo(folder_to + "\\" + a.ToString().PadLeft(2, '0') + "\\" + fi.Name, true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                label_Action.Text = (ex.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                if (textBox_From1.Enabled == true)
                {
                    Copy(textBox_From1.Text, textBox_To.Text, numericUpDown1);
                }
                if (textBox_From2.Enabled == true)
                {
                    Copy(textBox_From2.Text, textBox_To.Text, numericUpDown2);
                }
                if (textBox_From3.Enabled == true)
                {
                    Copy(textBox_From3.Text, textBox_To.Text, numericUpDown3);
                }
            }
            timer1.Start();

            label_Action.Text = "Копирование файлов запущено!";
            button_Stop.Enabled = true;
            button_Start.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label_Action.Text = "Копирование файлов остановленно.";
            button_Stop.Enabled = false;
            button_Start.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа создана сотрудником IT отдела для отдела разработки ПО,\n при поддержке программиста Владимира =) !");
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox_From1.Enabled = false;
                numericUpDown1.Enabled = false;
            }
            else
            {
                textBox_From1.Enabled = true;
                numericUpDown1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                textBox_From2.Enabled = false;
                numericUpDown2.Enabled = false;
            }
            else
            {
                textBox_From2.Enabled = true;
                numericUpDown2.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                textBox_From3.Enabled = false;
                numericUpDown3.Enabled = false;
            }
            else
            {
                textBox_From3.Enabled = true;
                numericUpDown3.Enabled = true;
            }
        }        
    }
}

