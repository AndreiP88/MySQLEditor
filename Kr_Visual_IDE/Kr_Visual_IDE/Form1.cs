/*
 *                                                  Vendor:  javavirys
 *      mail:    mailto:javavirys@mail.ru                                                 web:     http://srcblog.ru        *
*/ 

using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace Kr_Visual_IDE
{
    public partial class Form1 : Form
    {
        DataGridView dbDataGridView;
        ConnectionForm auth;

        MySqlConnection conn;
        MySqlDataAdapter MyData;

        public Form1()
        {
            InitializeComponent();
            dbDataGridView = new DataGridView();
            SetupDataGridView();
            auth = new ConnectionForm(this);

        }

        private void SetupDataGridView()
        {
            Controls.Add(dbDataGridView);
           
            dbDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dbDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dbDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(dbDataGridView.Font, FontStyle.Bold);

            dbDataGridView.Name = "dbDataGridView";
            dbDataGridView.Location = new Point(8, 8);
            dbDataGridView.Size = new Size(500, 250);
            dbDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dbDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dbDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dbDataGridView.GridColor = Color.Black;
            dbDataGridView.RowHeadersVisible = false;

            dbDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dbDataGridView.MultiSelect = false;

            dbDataGridView.ContextMenuStrip = contextMenuStrip1;

            Form1_Resize(null, null);
        }

        public void LoadMySql(string serverName,// Адрес сервера (для локальной базы пишите "localhost")
            string userName, // Имя пользователя
            string dbName,//Имя базы данных
            int port, // Порт для подключения
            string password,
            string _table)
        {
            string connStr;
            string strTable;

            DataTable table;

            connStr = "Database="+dbName+";Data Source=" + serverName + ";User Id=" + userName + ";Password=" + password;
            conn = new MySqlConnection(connStr);
            strTable = _table;
            string sql = "SELECT * FROM " + strTable; // Строка запроса
            conn.Open();
            MyData = new MySqlDataAdapter(sql,conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(MyData);
            MyData.InsertCommand = builder.GetInsertCommand();
            MyData.UpdateCommand = builder.GetUpdateCommand();
            MyData.DeleteCommand = builder.GetDeleteCommand();
            table = new DataTable();
            MyData.Fill(table);
            UpdateGrid(table);
        }

        public void UpdateGrid(DataTable bsource)
        {
            dbDataGridView.DataSource = bsource;
            dbDataGridView.AutoResizeColumnHeadersHeight();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            auth.ShowDialog(this);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                dbDataGridView.Columns.Clear();
            }catch(Exception ex){
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            dbDataGridView.Left = 1;
            dbDataGridView.Top = 25;
            dbDataGridView.Width = this.Width - 20;
            dbDataGridView.Height = this.Height - 32;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyData.Update((DataTable)dbDataGridView.DataSource);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dbDataGridView.Rows.Remove(dbDataGridView.SelectedRows[0]);
            }catch(Exception ex){
            
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (dbDataGridView.RowCount > 0 && dbDataGridView.CurrentRow.Index >= 0)
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                }
                else
                    contextMenuStrip1.Items[0].Enabled = false;
            }catch(Exception ex){
            
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SearchForm(this).Show();
        }

        public void searchRow(string text)
        {
            for(int i = 0; i < dbDataGridView.ColumnCount; i++)
                for (int j = 0; j < dbDataGridView.RowCount; j++)
                {
                    string value = "" + dbDataGridView[i, j].Value;
                    if (value == text)
                    {
                        dbDataGridView.ClearSelection();
                        dbDataGridView.Rows[j].Cells[i].Selected = true;
                        dbDataGridView.CurrentCell = dbDataGridView.Rows[j].Cells[i];
                        return;
                    }
                }
        }
    }
}
