using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Ninject.Modules;


namespace КПО_1
{
    public class MyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CarInsuranceContext>().ToSelf().InTransientScope();
            Bind<IModelRepository>().To<NoSqlModelRepository>();
        }
    }
}

