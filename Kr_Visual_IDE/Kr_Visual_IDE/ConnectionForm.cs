/*
 *                                                  Vendor:  javavirys
 *      mail:    mailto:javavirys@mail.ru                                                 web:     http://srcblog.ru        *
*/ 

using System;
using System.Windows.Forms;

namespace Kr_Visual_IDE
{
    public partial class ConnectionForm : Form
    {
        Form1 form;
        public ConnectionForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
            textBox1.Text = "localhost";
            textBox2.Text = "test1";
            textBox3.Text = "test1";
            textBox4.Text = "3306";
            textBox5.Text = "0000";
            textBox6.Text = "telephone_subscribers";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                form.LoadMySql(textBox1.Text, textBox2.Text, textBox3.Text, Int32.Parse(textBox4.Text), textBox5.Text,
                    textBox6.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
