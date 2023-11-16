using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.EntityFrameworkCore;
using Ninject;

namespace КПО_1
{
    public interface IForm_1_Script
    {
        public void LoadDataFromDatabase(ref DataTable dataTable);
        public void OpenForm2(IModelRepository _modelRepository);
        public void PopulateComboBox2(ref System.Windows.Forms.ComboBox comboBox);
        public void PopulateComboBox1(ref System.Windows.Forms.ComboBox comboBox);
        public void LoadDataFromTable(string tableName, ref DataGridView dataGridView);
        public void SortModels(string selected_brand, ref DataGridView dataGridView);
        public void FilterByDate(DateTime date, ref DataGridView dataGridView);
    }
    public  class Form1Script:IForm_1_Script {

        private string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        public void OpenForm2(IModelRepository _modelRepository)
        {
            

            Form2 form2 = new Form2(_modelRepository);
            form2.ShowDialog();
            
        }
        public void LoadDataFromDatabase(ref DataTable dataTable) //загружает таблицу models для справочника в datatable
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var models = context.Models.ToList();
                dataTable.Clear();
                dataTable = ConvertToDataTable(models);

            }

        }

        public DataTable ConvertToDataTable(List<Model> models)
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

        public void PopulateComboBox2(ref System.Windows.Forms.ComboBox comboBox)// заполнение комбобокса с моделями машин
        {
            comboBox.Items.Clear();
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
                            comboBox.Items.Add(reader["name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при заполнении ComboBox: " + ex.Message);
                }
            }
        }
        public void PopulateComboBox1(ref System.Windows.Forms.ComboBox comboBox)// заполнение комбобокса со всеми таблицаами кроме системной и models
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
                        comboBox.Items.Add(tableName);
                    }
                    comboBox.Items.Remove("sysdiagrams");
                    comboBox.Items.Remove("Models");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении списка таблиц: " + ex.Message);
                }
            }
        }

        public void LoadDataFromTable(string tableName,ref DataGridView dataGridView)//загрузка указанной таблицы
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM {tableName}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView.DataSource = dataTable;


            }
        }

        public void SortModels(string selected_brand, ref DataGridView dataGridView)
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {
                var vehicles = context.Vehicles
                                    .Where(v => v.Brand == selected_brand).Select(e => new { e.VehicleId, e.StateImplementationNumber, e.Brand, e.Mileage, e.OwnerDrivingExperience, e.ModelId, e.OwnerId })
                                    .ToList();

                dataGridView.DataSource = vehicles;
            }
        }

        public void FilterByDate(DateTime date, ref DataGridView dataGridView)
        {
            using (CarInsuranceContext context = new CarInsuranceContext())
            {

                var results = context.Contracts.FromSqlRaw("EXEC FilterDataByDate @FilterDate", new Microsoft.Data.SqlClient.SqlParameter("FilterDate", date)).ToList();

                dataGridView.DataSource = results;
            }
        }
    }
}
