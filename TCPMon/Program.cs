using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPMon.Blaze;
using VD.Blaze.Interpreter.Environment;

namespace TCPMon
{
    internal static class Program
    {
        public static ModuleEnv InternalModule;
        // Utils.CreateLibraries(internal_module);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InternalModule = new ModuleEnv();
            Utils.CreateLibraries(InternalModule);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
