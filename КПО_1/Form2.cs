using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using КПО_1.Model;

namespace КПО_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            FillTable();
            FillCombobox();
            FillChangeBox();
            dataGridView1.ReadOnly = false;
        }

        private void FillTable()
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var models = context.Models.Select(e => new { e.ModelId, e.Name }).ToList();
                dataGridView1.DataSource = models;
            }
        }
        private void FillChangeBox()
        {
            Change_combobox.SelectedItem = null;
            Change_combobox.Items.Clear();
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var models = context.Models.ToList();
                foreach (var model in models)
                {
                    Change_combobox.Items.Add(model.ModelId);
                }
            }
        }

        private void delete_but_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedItem.ToString();
            if (name != null)
                using (CarInsuranceContext context = new CarInsuranceContext())
                {

                    var model = context.Models.FirstOrDefault(m => m.Name == name);
                    if (model != null)
                    {

                        context.Models.Remove(model);
                        context.SaveChanges();
                    }
                }
            FillCombobox();
            FillTable();


        }

        private void add_but_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var model = new Model
                {
                    Name = name
                };
                context.Models.Add(model);
                context.SaveChanges();
                textBox1.Text = "";

            }
            FillCombobox();
            FillTable();
        }
        private void FillCombobox()
        {
            comboBox1.SelectedItem = null;
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int modelId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ModelId"].Value);
                string newName = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                UpdateModel(modelId, newName);
            }
        }

        public void UpdateModel(int modelId, string newName)
        {
            using (var context = new CarInsuranceContext())
            {
                var model = context.Models.Find(modelId);

                if (model != null)
                {
                    model.Name = newName;
                    context.SaveChanges();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Change_but_Click(object sender, EventArgs e)
        {
            string id = Change_combobox.SelectedItem.ToString();
            string name = Change_textBox.Text;
            if ((id!=null)&&(name!=null))
                UpdateModel(Convert.ToInt32(id), name);
            FillTable();
            FillCombobox();
        }
    }
}
