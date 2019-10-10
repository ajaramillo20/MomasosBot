using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMomasosBot
{
    class Program
    {
        static void Main(string[] args)
        {
            GetConfig();
            IniciarEventos();
            while (true)
            {
                ShowOptions();
                var opc = Console.ReadLine();

                switch (opc)
                {
                    case "1":
                        Console.WriteLine($"Ingrese Telegram Key:");
                        var telegramKey= Console.ReadLine();
                        AppController.Config.TelegramKey = telegramKey;
                        GetConfig();
                        break;
                    
                }
            }
        }

        private static void ShowOptions()
        {
            Console.WriteLine("1.Set Telegram Key");
        }

        private static void GetConfig()
        {
            var telegramKey = AppController.Config.TelegramKey;
            var rutaDescargas = AppController.Config.RutaDescargas;
            var cloud = AppController.Config.Cloud;
            var apiKey = AppController.Config.ApiKey;
            var apiSecret = AppController.Config.ApiSecret;
            var adminId = AppController.Config.AdminId;
            var limiteCaracteres = AppController.Config.LimiteCaracteres;
            Console.Clear();
            Console.WriteLine($"Telegram Key: {telegramKey}");
            Console.WriteLine($"Ruta descargas: {rutaDescargas}");
            Console.WriteLine($"Cloud: {cloud}");
            Console.WriteLine($"Api Key: {apiKey}");
            Console.WriteLine($"Api secret: {apiSecret}");
            Console.WriteLine($"Admin id: {adminId}");
            Console.WriteLine($"Limite caracteres: {limiteCaracteres}");
        }

        private static void IniciarEventos()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
