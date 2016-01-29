using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kr_Visual_IDE
{
    public partial class SearchForm : Form
    {
        Form1 form;
        public SearchForm(Form1 _form)
        {
            InitializeComponent();
            form = _form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.searchRow(this.textBox1.Text);
        }
    }
}
