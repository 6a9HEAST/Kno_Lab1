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
        private DataTable dataTable = new DataTable();
        public static bool changed = false;
        private DataTable selectedTableData = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void note_Click(object sender, EventArgs e)
        {
            
            LoadDataFromDatabase();
            form2 = new Form2(ref dataTable);
            form2.ShowDialog();
            if (changed) SaveDataToDatabase();
            changed = false;
        }

        private void LoadDataFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Models";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    dataTable.Clear(); 
                    adapter.Fill(dataTable);
                }
            }
        }
        private void SaveDataToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Models"; 

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                {
                    adapter.Update(dataTable); 
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTableComboBox();
        }

        private void PopulateTableComboBox()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTableName = comboBox1.SelectedItem.ToString();

            LoadDataFromTable(selectedTableName);
        }
        private void LoadDataFromTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM {tableName}";

                    selectedTableData.Clear();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(selectedTableData);
                    }

                    dataGridView1.DataSource = selectedTableData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных из таблицы: " + ex.Message);
                }
            }
        }
    }
}