using System;
using System.Drawing;

namespace RecortadorDeImagenes
{
    public class Imagen
    {
        private Bitmap ImagenAEscalar { get; set; }
        private string ImagenName { get; set; } = string.Empty;

        public Imagen(Bitmap imagenAEscalar)
        {

            ImagenAEscalar = imagenAEscalar;
        }

        public Color[,] ConvertirImagenArray()
        {
            int ancho = ImagenAEscalar.Width;
            int alto = ImagenAEscalar.Height;
            Color[,] arrayColores = new Color[ancho, alto];

            for (int i = 0; i < ancho; i++)
            {
                for (int j = 0; j < alto; j++)
                {
                    Color color = ImagenAEscalar.GetPixel(i, j);
                    arrayColores[i, j] = color;
                }
            }

            return arrayColores;
    
        }
    }
}
