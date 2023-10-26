using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using КПО_1.Model;

namespace КПО_1
{

    internal class EF
    {
        CarInsuranceContext context = new CarInsuranceContext();
        public void Models_Create(Model.Model model)
        {
            context.Models.Add(model);
            context.SaveChanges();
        }
        public Model.Model Models_Read(int id)
        {
            return context.Models.Find(id);
        }
        public void Models_Update(Model.Model entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public void Models_Delete(int id)
        {
            var entity = context.Models.Find(id);
            if (entity != null)
            {
                context.Models.Remove(entity);
                context.SaveChanges();
            }
        }


    }
}
