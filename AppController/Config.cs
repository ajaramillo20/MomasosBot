using System.ComponentModel;

namespace AppController
{
    public static class Config
    {
        public static string AdminId
        {
            get => Properties.Settings.Default.AdminId;
            set => Properties.Settings.Default.AdminId = value;
        }

        public static string TelegramKey
        {
            get => Properties.Settings.Default.TelegramKey;
            set => Properties.Settings.Default.TelegramKey = value;
        }

        public static string RutaDescargas
        {
            get => Properties.Settings.Default.RutaArchivos;
            set => Properties.Settings.Default.RutaArchivos = value;
        }

        public static string Cloud
        {
            get => Properties.Settings.Default.Cloud;
            set => Properties.Settings.Default.Cloud = value;
        }

        public static string ApiKey
        {
            get => Properties.Settings.Default.ApiKey;
            set => Properties.Settings.Default.ApiKey = value;
        }

        public static string ApiSecret
        {
            get => Properties.Settings.Default.ApiSecret;
            set => Properties.Settings.Default.ApiSecret = value;
        }

        public static int ThumnailHeight
        {
            get => Properties.Settings.Default.ThumbnailHeight;
            set => Properties.Settings.Default.ThumbnailHeight = value;
        }

        public static int ThumbnailWidth
        {
            get => Properties.Settings.Default.ThumbnailWidth;
            set => Properties.Settings.Default.ThumbnailWidth = value;
        }

        public static string Extension
        {
            get => Properties.Settings.Default.Extension;
            set => Properties.Settings.Default.Extension = value;
        }

        public static void Iniciar()
        {
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private static void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
