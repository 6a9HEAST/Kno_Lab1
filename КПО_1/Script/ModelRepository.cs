using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace КПО_1
{
    

    public interface IModelRepository
    {
        void Create(string name);
        void Delete(string name);
        void UpdateTable(DataGridView datagridview);

    }
    internal class ModelRepository : IModelRepository
    {
        private readonly CarInsuranceContext _context;
        public ModelRepository(CarInsuranceContext context)
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
        public void UpdateTable(DataGridView datagridview)
        {
                var models = _context.Models.Select(e => new { e.ModelId, e.Name }).ToList();
                datagridview.DataSource = models;
        }
    }
}
