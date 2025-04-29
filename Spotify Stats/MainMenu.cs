
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Spotify_Stats.UserData;

namespace Spotify_Stats
{
    public partial class MainMenu : Form
    {


        public MainMenu()
        {
            InitializeComponent();


            var settings = Properties.Settings.Default;
            string accessToken = settings.AccessToken;

            SetupUserProfileUI(accessToken);

        }

        UserData ud = new UserData();
        System.Drawing.Image profileImage;



        private async Task SetupUserProfileUI(string accessToken)
        {
            try
            {
                // Obtener datos del perfil
                UserProfile profile = await ud.GetUserProfileData(accessToken);

                // Actualizar etiqueta con el nombre
                lblUsername.Text = profile.DisplayName ?? "Usuario de Spotify";

                // Si hay imagen de perfil disponible, descargarla y mostrarla
                if (!string.IsNullOrEmpty(profile.ProfileImageUrl))
                {
                    profileImage = await ud.DownloadProfileImage(profile.ProfileImageUrl);
                    if (profileImage != null)
                    {
                        // Configurar la imagen en un PictureBox circular
                        pboxUserPhoto.Image = profileImage;

                        // Si quieres hacer la imagen circular (común en interfaces modernas)
                        SetCircularProfilePicture(pboxUserPhoto, profileImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar perfil: {ex.Message}");
            }
        }

        public void SetCircularProfilePicture(PictureBox pb, System.Drawing.Image originalImage)
        {
            if (originalImage == null || pb == null)
                return;

            // Crear un nuevo bitmap cuadrado basado en el tamaño más pequeño
            int sideLength = Math.Min(originalImage.Width, originalImage.Height);
            Bitmap squareImage = new Bitmap(sideLength, sideLength);

            using (Graphics g = Graphics.FromImage(squareImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Dibujar la imagen recortándola como cuadrado en el centro
                Rectangle destRect = new Rectangle(0, 0, sideLength, sideLength);
                Rectangle srcRect = new Rectangle(
                    (originalImage.Width - sideLength) / 2,
                    (originalImage.Height - sideLength) / 2,
                    sideLength,
                    sideLength
                );

                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
            }

            // Ahora crear una imagen circular
            Bitmap circularImage = new Bitmap(sideLength, sideLength);
            using (Graphics g = Graphics.FromImage(circularImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, sideLength, sideLength);
                    g.SetClip(path);
                    g.DrawImage(squareImage, 0, 0);
                }
            }

            // Ajustar el PictureBox
            pb.Image = circularImage;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.Transparent;

            // Opcional: recortar el PictureBox también
            System.Drawing.Drawing2D.GraphicsPath pathCircle = new System.Drawing.Drawing2D.GraphicsPath();
            pathCircle.AddEllipse(0, 0, pb.Width, pb.Height);
            pb.Region = new Region(pathCircle);
        }

        private void Logout()
        {
            // 1. Limpiar los tokens almacenados
            Properties.Settings.Default.AccessToken = string.Empty;
            Properties.Settings.Default.RefreshToken = string.Empty;
            Properties.Settings.Default.TokenExpiration = DateTime.MinValue;
            Properties.Settings.Default.Save();





        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Logout();


            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Application.Exit();
        }
    }
}
