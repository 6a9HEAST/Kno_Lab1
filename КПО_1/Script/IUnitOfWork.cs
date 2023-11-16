using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КПО_1
{
    interface IUnitOfWork : IDisposable
    {
        IModelRepository model_repository { get; }

        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarInsuranceContext _context;
        public IModelRepository model_repository { get; private set; }
        public UnitOfWork(CarInsuranceContext context)
        {
            _context = context;
            model_repository = new ModelRepository(_context);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
