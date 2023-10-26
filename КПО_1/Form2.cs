using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using КПО_1.Model;

namespace КПО_1
{
    public partial class Form2 : Form
    {
        public Form2(ref DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
            FillCombobox();
        }
        private void SaveDataToDatabase() //сохраняет datatable в models базы данных
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int modelid = (int)row.Cells["model_id"].Value;
                    string newName = row.Cells["name"].Value.ToString();

                    var model = context.Models.Find(modelid);
                    if (model != null)
                    {
                        model.Name = newName;
                    }
                }

                context.SaveChanges(); // Сохранение всех изменений в базе данных
            }
        }

        private void delete_but_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedIndex;
        }

        private void add_but_Click(object sender, EventArgs e)
        {

        }
        private void FillCombobox()
        {
            comboBox1.Items.Clear();
            using (CarInsuranceContext context = new CarInsuranceContext()) 
            {
                
                var models = context.Models.ToList();
                foreach (var model in models) 
                { 
                comboBox1.Items.Add(model.Name);
                }
            }
        }
    }
}
