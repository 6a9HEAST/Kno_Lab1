using Ninject;
using System.Reflection;
using MongoDB.Driver;

namespace КПО_1
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Start();
        }

        public static void Start()
        {
            IKernel kernel = new StandardKernel(new MyModule());
            Form1 form1 = kernel.Get<Form1>();
            Application.Run(mainForm: form1);
        }
    }

}