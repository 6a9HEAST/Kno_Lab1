using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КПО_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            LoadColumnsIntoComboBox(Form1.dataTable);
        }

        private void save_Click(object sender, EventArgs e)
        {
            FilterDataTable();
            Form1.changed = true;
            this.Close();   
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void LoadColumnsIntoComboBox(DataTable dataTable)
        {
            // Очистите ComboBox перед загрузкой столбцов
            comboBox1.Items.Clear();

            // Пройдитесь по всем столбцам DataTable и добавьте их в ComboBox
            foreach (DataColumn column in dataTable.Columns)
            {
                comboBox1.Items.Add(column.ColumnName);
            }
        }
        public void FilterDataTable()
        {
            DataTable filteredTable = new DataTable();
            filteredTable = Form1.dataTable.Clone();
            string columnName=comboBox1.Text;
            string filterValue = textBox1.Text.ToString();
            foreach (DataRow row in Form1.dataTable.Rows)
            {
                if (row[columnName].ToString() == filterValue)
                {
                    filteredTable.ImportRow(row);
                }
            }
            Form1.dataTable= filteredTable;
        }
    }
}
