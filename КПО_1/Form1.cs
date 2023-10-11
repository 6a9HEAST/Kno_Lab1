using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace КПО_1
{
    public partial class Form1 : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        Form2 form2;
        public static DataTable dataTable = new DataTable();
        public static bool changed = false;
        private DataTable selectedTableData = new DataTable();
        string selectedtable;

        public Form1()
        {
            InitializeComponent();
        }




        private void LoadDataFromDatabase() //загружает таблицу models для справочника в datatable
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                selectedtable = comboBox1.SelectedItem.ToString();
                string query = "SELECT * FROM " + selectedtable;
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    dataTable.Clear();
                    adapter.Fill(dataTable);
                }
            }
        }
        private void SaveDataToDatabase() //сохраняет datatable в models базы данных
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM " + selectedtable;

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    adapter.Update(dataTable);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTableComboBox1();
        }

        private void PopulateTableComboBox1()// заполнение комбобокса со всеми таблицаами кроме системной и models
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    DataTable tableSchema = connection.GetSchema("Tables");

                    foreach (DataRow row in tableSchema.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        comboBox1.Items.Add(tableName);
                    }
                    comboBox1.Items.Remove("sysdiagrams");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении списка таблиц: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)// при выборе таблицы она сразу загружается
        {
            string selectedTableName = comboBox1.SelectedItem.ToString();
            changed = false;
            LoadDataFromTable(selectedTableName);
        }
        private void LoadDataFromTable(string tableName)//загрузка указанной таблицы
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM {tableName}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;


            }
        }

        private void raw_sql_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            form2 = new Form2();
            form2.ShowDialog();
            dataGridView1.DataSource = dataTable;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (!changed) { SaveDataToDatabase(); }
        }
    }
}