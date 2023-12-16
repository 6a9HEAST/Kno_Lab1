using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Ninject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace КПО_1
{
    

    public interface IModelRepository
    {
        void Create(string name);
        void Delete(string name);
        DataTable ReturnTable();
        void Update (string oldName, string newName);
        List<string> ReturnNames();

    }
    internal class SqlModelRepository : IModelRepository
    {
        private readonly CarInsuranceContext _context;
        public SqlModelRepository(CarInsuranceContext context)
        {
            _context = context;
        }
        public void Create(string name)
        {
                var model = new Model
                {
                    Name = name
                };
                _context.Models.Add(model);
                _context.SaveChanges();

        }
        public void Delete(string name)
        {
                var model = _context.Models.FirstOrDefault(m => m.Name == name);
                if (model != null)
                {

                    _context.Models.Remove(model);
                    _context.SaveChanges();
                }
        }
        public DataTable ReturnTable()
            {
                var models = _context.Models.Select(e => new { e.ModelId, e.Name }).ToList();
            
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("ModelId", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
            
                foreach (var model in models)
                {
                   dataTable.Rows.Add(model.ModelId, model.Name);
                }

                return dataTable;
        }

        public List<string> ReturnNames()
        {
            var models = _context.Models.ToList();
            List<string> names = new List<string>();
            foreach (var model in models)
               {
                  names.Add(model.Name);
               }
            return names;
        }

        public void Update(string oldName, string newName)
        {

        }
    }

    internal class NoSqlModelRepository : IModelRepository
    {
        private readonly IMongoCollection<NoSqlModel> _collection;
        private readonly IMongoDatabase database;
        private IMongoClient _client = new MongoClient("mongodb://localhost:27017");
        [Inject]
        public NoSqlModelRepository() 
        {
            
            IMongoDatabase database = _client.GetDatabase("CarInsurance");
            _collection = database.GetCollection<NoSqlModel>("model");
        }

        public void Create(string name)
        {
            var model = new NoSqlModel
            {
                Name = name
            };
            _collection.InsertOne(model);
        }
        public void Delete(string name)
        {
            var filter = Builders<NoSqlModel>.Filter.Eq("Name", name);
            _collection.DeleteOne(filter);
        }
        public DataTable ReturnTable()
        {
            var models = _collection.Find(new BsonDocument()).ToList();

            DataTable dataTable = new DataTable();

            // Добавляем колонки в DataTable (замените на реальные поля вашего документа)
            dataTable.Columns.Add("ModelId", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));

            // Заполняем DataTable данными из MongoDB
            foreach (var model in models)
            {
                dataTable.Rows.Add(model.Id.ToString(), model.Name);
            }

            return dataTable;
        }

        public List<string> ReturnNames()
        {
            var names = _collection.AsQueryable().Select(model => model.Name).ToList();
            return names;
        }

        public void Update(string oldName, string newName)
        {
            var filter = Builders<NoSqlModel>.Filter.Eq("Name", oldName);
            var update = Builders<NoSqlModel>.Update.Set("Name", newName);

            _collection.UpdateOne(filter, update);
        }
    }

    internal class NoSqlModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }

    }
}
