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
        }

        private void FillTable()
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var models = context.Models.Select(e => new { e.ModelId, e.Name }).ToList();
                dataGridView1.DataSource= models;
            }
        }

        private void delete_but_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedItem.ToString();
            if (name!=null)
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
            comboBox1.SelectedItem=null;
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
