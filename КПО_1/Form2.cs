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
        public Form2(ref DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
        }

        private void save_Click(object sender, EventArgs e)
        {
            Form1.changed = true;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
