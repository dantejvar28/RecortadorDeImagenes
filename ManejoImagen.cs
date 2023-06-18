using System;
using RecortadorDeImagenes;
using System.Drawing;
using System.Drawing.Imaging;


namespace RecortadorDeImagenes
{
    public class ManejoImagen
    {
        
        Color[,] arrayDeColores;
        int[] positionNorth = new int[2];
        int[] positionSouth = new int[2];
        int[] positionWest = new int[2];
        int[] positionEast = new int[2];

        Color[,] arrayColoresRecortado;

        
        public Imagen ImagenIngresada { get; private set; }
        public ManejoImagen(Imagen imagenIngresada)
        {
            
            ImagenIngresada= imagenIngresada;

        }

        //--------------------
        #region Metodos de Recorte
        public void DefinirLimitesImagen()
        {
            arrayDeColores = ImagenIngresada.ConvertirImagenArray();

            // Recorrer filas en ambos sentidos
            // Recorrido de norte a sur
            for (int i = 0; i < arrayDeColores.GetLength(0); i++)
            {
                for (int j = 0; j < arrayDeColores.GetLength(1); j++)
                {
                    if (arrayDeColores[i, j].Name != "ffffffff")
                    {
                        positionNorth[0] = i;
                        positionNorth[1] = j;
                        break;
                    }
                }
                if (positionNorth[0] != 0)
                    break;
            }


            // Recorrido de sur a norte
            for (int i = arrayDeColores.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < arrayDeColores.GetLength(1); j++)
                {
                    if (arrayDeColores[i, j].Name != "ffffffff")
                    {
                        positionSouth[0] = i;
                        positionSouth[1] = j;
                        break;
                    }
                }
                if (positionSouth[0] != 0)
                    break;
            }

            // Recorrer columnas en ambos sentidos

            // Recorrido de oeste a este
            for (int i = 0; i < arrayDeColores.GetLength(1); i++)
            {
                for (int j = 0; j < arrayDeColores.GetLength(0); j++)
                {
                    if (arrayDeColores[j, i].Name != "ffffffff")
                    {
                        positionWest[0] = j;
                        positionWest[1] = i;
                        break;
                    }
                }
                if (positionWest[0] != 0)
                    break;
            }

            // Recorrido de este a oeste
            for (int i = arrayDeColores.GetLength(1) - 1; i >= 0; i--)
            {
                for (int j = 0; j < arrayDeColores.GetLength(0); j++)
                {
                    if (arrayDeColores[j, i].Name != "ffffffff")
                    {
                        positionEast[0] = j;
                        positionEast[1] = i;
                        break;
                    }
                }
                if (positionEast[0] != 0)
                    break;
            }

        }
        #endregion
        //----------------------

        #region Recortar Bordes
        // Recortar Bordes ----- 
        public Bitmap BitmapRecortado()
        {
            arrayColoresRecortado = new Color[(positionSouth[0] + 1) - positionNorth[0], (positionEast[1] + 1) - positionWest[1]];

            int initialRowPosition = positionNorth[0];
            int initialColumnPosition = positionWest[1];

            for (int i = 0; i < arrayColoresRecortado.GetLength(0); i++)
            {
                for (int j = 0; j < arrayColoresRecortado.GetLength(1); j++)
                {
                    arrayColoresRecortado[i, j] = arrayDeColores[initialRowPosition + i, initialColumnPosition + j];
                }
            }
            int ancho = arrayColoresRecortado.GetLength(0);
            int largo = arrayColoresRecortado.GetLength(1);
            Bitmap MapaBitNuevaImagen = new Bitmap(ancho, largo);

            for (int i = 0; i < arrayColoresRecortado.GetLength(0); i++)
            {
                for (int j = 0; j < arrayColoresRecortado.GetLength(1); j++)
                {
                    MapaBitNuevaImagen.SetPixel(i, j, arrayColoresRecortado[i, j]);
                }
            }

            return MapaBitNuevaImagen;
        }
        #endregion


        #region Finalizar Imagen
        //--- Establecer Medidas de Nueva Imagen

        public int ladoSize(Bitmap recorte,double porcentajeDeOcupacion)
        {
            int totalPixelesRecorte=recorte.Width*recorte.Height;

            int totalPixelesNuevaImagen = Convert.ToInt32((totalPixelesRecorte * 1) /porcentajeDeOcupacion);

            int lado = Convert.ToInt32(Math.Sqrt(totalPixelesNuevaImagen));

            if(lado<recorte.Width)
            {
                lado = Convert.ToInt32( recorte.Width*1.1);
            }
            if(lado<recorte.Height)
            {
                lado = Convert.ToInt32(recorte.Height * 1.1);
            }

            return lado;
        }

        // Determinar la posición para colocar al centro la imagen
        public int[] PosicionInicial(Bitmap recorte, int lado)
        {
            
            int[] posiciones = new int[2];
            posiciones[0] = (lado - recorte.Width) / 2;
            posiciones[1]= (lado - recorte.Height) /2;


            return posiciones;

        }

        // Colocar la imagen recortada en el centro y rellenar los espacios con color Blanco
        public Bitmap GenerarImagenFinal(int lado, int[] posicionInicial, Imagen imagen)
        {
            Color[,] array = new Color[lado, lado];
            Color[,] arrayRecorte = imagen.ConvertirImagenArray();

            Bitmap imagenSalida=new Bitmap(lado,lado);

            int colum = posicionInicial[1];
            int row = posicionInicial[0];

            // Colocar recorte en el centro
            for (int i = 0; i < arrayRecorte.GetLength(0); i++)
            {
                for (int j = 0; j < arrayRecorte.GetLength(1); j++)
                {
                    array[row + i, colum + j] = arrayRecorte[i, j];
                }
            }

            // Rellenar con blanco los puntos sin color

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i,j]== Color.Empty)
                    {
                        array[i, j] = Color.White;
                    }
                    imagenSalida.SetPixel(i, j, array[i,j]);
                }
            }

            return imagenSalida;
            
        }

        public Bitmap EscalarBitmap(Bitmap bitmapOriginal, int nuevoAncho, int nuevoAlto)
        {
            Bitmap bitmapEscalado = new Bitmap(nuevoAncho, nuevoAlto);

            using (Graphics graphics = Graphics.FromImage(bitmapEscalado))
            {
                graphics.DrawImage(bitmapOriginal, 0, 0, nuevoAncho, nuevoAlto);
            }

            return bitmapEscalado;
        }

        #endregion

        // -- Convertir Imagenes

        

    }
}
