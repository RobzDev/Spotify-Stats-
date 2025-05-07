using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Stats
{
    public  class b64
    {
        public static string ImageToBase64(System.Drawing.Image image)
        {
            if (image == null)
                return null;

            using (var clonedImage = new Bitmap(image))
            using (var ms = new MemoryStream())
            {
                try
                {
                    clonedImage.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al convertir imagen: {ex.Message}");
                    return null;
                }
            }
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Usar MemoryStream sin using para evitar problemas GDI+
                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);

                //save the image to the documents folder
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string imagePath = Path.Combine(documentsPath, "Spotify_Stats", "userImage.png");
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(fileStream);
                }

                return System.Drawing.Image.FromStream(ms, true); // El stream debe permanecer abierto
            }
            catch
            {
                return null; // O devuelve una imagen por defecto
            }
        }




        public static void SetCircularProfilePicture(PictureBox pb, System.Drawing.Image originalImage)
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
    }
}
