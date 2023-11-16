using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace КПО_1
{
    public partial class Form2 : Form
    {
        public bool changed = false;
        IModelRepository _repository;
        public Form2(IModelRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            _repository.UpdateTable(dataGridView1);
            FillCombobox();
            
        }
        ~Form2()
        {
            
        }

        private void delete_but_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedItem.ToString();
            if (name!=null)
                _repository.Delete(name);
            FillCombobox();
            _repository.UpdateTable(dataGridView1);


        }

        private void add_but_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            _repository.Create(name);
            textBox1.Text = "";
            FillCombobox();
            _repository.UpdateTable(dataGridView1);
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
