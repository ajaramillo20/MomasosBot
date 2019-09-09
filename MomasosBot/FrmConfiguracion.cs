using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomasosBot
{
    public partial class FrmConfiguracion : MetroForm
    {
        public FrmConfiguracion()
        {
            InitializeComponent();
        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            GetConfiguracion();
        }

        private void _btnGuardar_Click(object sender, EventArgs e)
        {
            AppController.Config.RutaDescargas = _txtRutaDescargas.Text;
            AppController.Config.TelegramKey = _txtTelegramKey.Text;
            AppController.Config.Cloud = _txtCloud.Text;
            AppController.Config.ApiSecret = _txtApiSecret.Text;
            AppController.Config.ApiKey = _txtApiKey.Text;
            AppController.Config.ThumnailHeight = int.Parse(_txtHeigth.Text);
            AppController.Config.ThumbnailWidth = int.Parse(_txtWidth.Text);
            AppController.Config.AdminId = _txtAdminId.Text;
            AppController.Config.Extension = _txtExtension.Text;
            AppController.Config.LimiteCaracteres = (int)_txtLimiteCaracteres.Value;
            GetConfiguracion();
        }

        private void GetConfiguracion()
        {
            _txtRutaDescargas.Text = AppController.Config.RutaDescargas;
            _txtTelegramKey.Text = AppController.Config.TelegramKey;
            _txtCloud.Text = AppController.Config.Cloud;
            _txtApiKey.Text = AppController.Config.ApiKey;
            _txtApiSecret.Text = AppController.Config.ApiSecret;
            _txtHeigth.Text = AppController.Config.ThumnailHeight.ToString();
            _txtWidth.Text = AppController.Config.ThumbnailWidth.ToString();
            _txtAdminId.Text = AppController.Config.AdminId;
            _txtExtension.Text = AppController.Config.Extension;
            _txtLimiteCaracteres.Value = AppController.Config.LimiteCaracteres;
        }

        private void _btnSeleccionaRuta_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                _txtRutaDescargas.Text = fd.SelectedPath;
            }
        }

        private void _btnReiniciar_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void _btnGuardar_Click_1(object sender, EventArgs e)
        {

        }
    }

}
