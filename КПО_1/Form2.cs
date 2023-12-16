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
            UpdateTable();
            UpdateCombobox();

        }
        ~Form2()
        {

        }

        private void delete_but_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedItem.ToString();
            if (name != null)
                _repository.Delete(name);
            UpdateCombobox();
            UpdateTable();

        }

        private void add_but_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            _repository.Create(name);
            textBox1.Text = "";
            UpdateCombobox();
            UpdateTable();
        }

        private void UpdateTable()
        {
            DataTable dt = _repository.ReturnTable();
            dataGridView1.DataSource = dt;
        }

        private void UpdateCombobox()
        {
            comboBox1.SelectedItem = null;
            comboBox1.Items.Clear();
            comboBoxForUpdate.SelectedItem = null;
            comboBoxForUpdate.Items.Clear();
            var names = _repository.ReturnNames();
            foreach (var name in names)
            {
                comboBox1.Items.Add(name);
                comboBoxForUpdate.Items.Add(name);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _repository.Update(comboBoxForUpdate.SelectedItem.ToString(),textBoxForUpdate.Text.ToString());
            UpdateTable();
            UpdateCombobox();
            textBoxForUpdate.Text = "";
        }
    }
}
