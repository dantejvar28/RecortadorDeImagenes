// See https://aka.ms/new-console-template for more information

using RecortadorDeImagenes;
using System.Drawing;
using System.Drawing.Imaging;

ManejoImagen manejoImagen;

Imagen imagenATrabajar;

Console.WriteLine("Bienvenido al Recortador de Imagenes");
Console.WriteLine("Ingrese la Ruta de la imagen a Recortar: ");
string ?rutaImagen= Console.ReadLine();

imagenATrabajar = new Imagen(new Bitmap(rutaImagen));
manejoImagen=new ManejoImagen(imagenATrabajar);
manejoImagen.DefinirLimitesImagen();

Bitmap MapaBitNuevaImagen = manejoImagen.BitmapRecortado();

int tamanoLado = manejoImagen.ladoSize(MapaBitNuevaImagen,0.75);
int[] posicionInicial = manejoImagen.PosicionInicial(MapaBitNuevaImagen, tamanoLado);
Imagen recorte = new(MapaBitNuevaImagen);

Bitmap imagenFinal =manejoImagen.EscalarBitmap(manejoImagen.GenerarImagenFinal(tamanoLado, posicionInicial, recorte),1200,1200);

Console.WriteLine("Indique la ruta para guardar");

string ?filePath=Console.ReadLine(); 

imagenFinal.Save(filePath,ImageFormat.Jpeg);

