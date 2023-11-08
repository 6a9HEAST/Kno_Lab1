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
        Form2 form2;
        private DataTable dataTable = new DataTable();
        public static bool changed = false;
        private DataTable selectedTableData = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }


        private void note_Click(object sender, EventArgs e) //кнопка справка
        {

            LoadDataFromDatabase();
            form2 = new Form2();
            form2.ShowDialog();
            if (changed)
            {
                //SaveDataToDatabase();
                PopulateComboBox2();
            }
            changed = false;
        }

        private void LoadDataFromDatabase() //загружает таблицу models для справочника в datatable
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var models = context.Models.ToList();
                dataTable.Clear();
                dataTable = ConvertToDataTable(models);
                
            }

        }
        
        private DataTable ConvertToDataTable(List<Model> models)
        {
            DataTable dt = new DataTable("Models");
            dt.Columns.Add("model_id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            foreach (var model in models)
            {
                // Создание новой строки в DataTable
                DataRow row = dt.NewRow();
                row["model_id"] = model.ModelId;
                row["name"] = model.Name;
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTableComboBox1();
            comboBox2.Visible = false;//
            label2.Visible = false;//при выборе vehicle появляется комбобокс и надпись
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            button1.Visible = false;//при выборе contract поялвется выбор даты для сортировки,и кнопка подтвердить
            PopulateComboBox2();
        }

        private void PopulateComboBox2()// заполнение комбобокса с моделями машин
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
                    comboBox1.Items.Remove("Models");
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//при выборе модели таблица сортируется 
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                string selectedBrand = comboBox2.SelectedItem.ToString();

                var vehicles = context.Vehicles
                                    .Where(v => v.Brand == selectedBrand).Select(e => new { e.VehicleId, e.StateImplementationNumber, e.Brand, e.Mileage, e.OwnerDrivingExperience, e.ModelId, e.OwnerId })
                                    .ToList();

                dataGridView1.DataSource = vehicles;
            }
        }

        private void button1_Click(object sender, EventArgs e)//по нажатию кнопки выполение хранимой в базе данных процедуры
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var filterDate = dateTimePicker1.Value;

                var results = context.Contracts.FromSqlRaw("EXEC FilterDataByDate @FilterDate", new Microsoft.Data.SqlClient.SqlParameter("FilterDate", filterDate)).ToList();

                dataGridView1.DataSource = results;
            }
        }
    }
}