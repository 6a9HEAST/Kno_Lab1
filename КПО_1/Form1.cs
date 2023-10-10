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
            //CreateStoredProcedure();
        }


        private void note_Click(object sender, EventArgs e)
        {

            LoadDataFromDatabase();
            form2 = new Form2(ref dataTable);
            form2.ShowDialog();
            if (changed)
            {
                SaveDataToDatabase();
                PopulateComboBox2();
            }
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
            PopulateTableComboBox1();
            comboBox2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            button1.Visible = false;
            PopulateComboBox2();
        }

        private void PopulateComboBox2()
        {
            comboBox2.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Создаем SQL-запрос для извлечения уникальных значений из столбца
                    string query = $"SELECT DISTINCT {"name"} FROM {"Models"}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Добавляем каждое уникальное значение в ComboBox
                            comboBox2.Items.Add(reader["name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при заполнении ComboBox: " + ex.Message);
                }
            }
        }
        private void PopulateTableComboBox1()
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
                    comboBox1.Items.Remove("Models");
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
            LoadDataFromTable(selectedTableName);
        }
        private void LoadDataFromTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //try
                //{
                //    connection.Open();

                //    string query = $"SELECT * FROM {tableName}";

                //    selectedTableData.Clear();

                //    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                //    {
                //        adapter.Fill(selectedTableData);
                //    }

                //    dataGridView1.DataSource = selectedTableData;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Ошибка при загрузке данных из таблицы: " + ex.Message);
                //}
                connection.Open();

                string sqlQuery = $"SELECT * FROM {tableName}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;


            }
        }
        private void CreateStoredProcedure()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-запрос для создания хранимой процедуры
                    string createProcedureQuery = @"
USE Car_insurance;
CREATE PROCEDURE FilterDataByDate
    @FilterDate DATE
AS
BEGIN
   
    SELECT *
    FROM contract
    WHERE conslusion_date > @FilterDate;
END;

                    ";

                    using (SqlCommand command = new SqlCommand(createProcedureQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Хранимая процедура создана успешно.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании хранимой процедуры: " + ex.Message);
                }
            }
        }


    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM vehicle WHERE brand = @brandparam";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@brandparam", comboBox2.SelectedItem.ToString());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        // Теперь у вас есть результат выборки данных с применением параметров
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
{
                connection.Open();

                using (SqlCommand command = new SqlCommand("FilterDataByDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Если есть параметры, добавьте их сюда
                    command.Parameters.AddWithValue("@FilterDate", dateTimePicker1.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource= dataTable;
                        // Теперь у вас есть результат выполнения хранимой процедуры
                    }
                }
            }
        }
    }
}