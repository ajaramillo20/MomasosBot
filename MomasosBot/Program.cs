using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomasosBot
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IniciarEventos();
            Application.Run(new FrmConfiguracion());
        }

        private static void IniciarEventos()
        {
            MomasosBotController.Controller.Iniciar();
            AppController.Config.Iniciar();
            CloudinaryApp.CloudController.Iniciar
            (
                cloud: AppController.Config.Cloud,
                apiKey: AppController.Config.ApiKey,
                apiSecert: AppController.Config.ApiSecret
            );
        }
    }
}
