using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.EntityFrameworkCore;

namespace КПО_1
{
    public partial class Form1 : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        private DataTable dataTable = new DataTable();
        public static bool changed = false;
        private DataTable selectedTableData = new DataTable();
        private IForm_1_Script form1Script;
        IModelRepository _modelRepository;

        public Form1() : this(null) // Добавьте стандартный конструктор
        {
        }

        public Form1(IModelRepository modelRepository)
        {
            InitializeComponent();
            form1Script = new Form1Script();
            _modelRepository = modelRepository;
        }


        private void note_Click(object sender, EventArgs e) //кнопка справка
        {
            form1Script.LoadDataFromDatabase(ref dataTable);
            form1Script.OpenForm2(_modelRepository);
            form1Script.PopulateComboBox2(ref comboBox2);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            form1Script.PopulateComboBox1(ref comboBox1);
            comboBox2.Visible = false;//
            label2.Visible = false;//при выборе vehicle появляется комбобокс и надпись
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            button1.Visible = false;//при выборе contract поялвется выбор даты для сортировки,и кнопка подтвердить
            form1Script.PopulateComboBox2(ref comboBox2);
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)// при выборе таблицы она сразу загружается
        {
            string selectedTableName = comboBox1.SelectedItem.ToString();
            if (selectedTableName == "vehicle")
            {
                comboBox2.Visible = true;
                label2.Visible = true;
            }
            else
            {
                comboBox2.Visible = false;
                label2.Visible = false;
            }
            if (selectedTableName == "contract")
            {
                dateTimePicker1.Visible = true;
                label3.Visible = true;
                button1.Visible = true;
            }
            else
            {
                label3.Visible = false;
                dateTimePicker1.Visible = false;
                button1.Visible = false;
            }
            form1Script.LoadDataFromTable(selectedTableName,ref dataGridView1);
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//при выборе модели таблица сортируется 
        {
            var selectedModel = comboBox2.SelectedItem.ToString();
            form1Script.SortModels(selectedModel,ref dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)//по нажатию кнопки выполение хранимой в базе данных процедуры
        {
            var filterDate = dateTimePicker1.Value;
            form1Script.FilterByDate(filterDate,ref dataGridView1);
        }

    }
}