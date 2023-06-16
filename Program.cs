// See https://aka.ms/new-console-template for more information

using RecortadorDeImagenes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

ManejoImagen manejoImagen;
ListadorDeImagenes listadorDeImagenes;
Imagen imagenATrabajar;


Console.Clear();
Console.WriteLine("Bienvenido al Recortador de Imagenes");
Console.WriteLine("Ingrese La ruta de la carpeta con las imagenes a recortar: ");
string root = Console.ReadLine();
listadorDeImagenes = new ListadorDeImagenes(root);
List<string> rutasDeImagenes = listadorDeImagenes.rutasImagenes();
Console.WriteLine("Ingrese la ruta de la carpeta donde desea guardar las nuevas imagenes: ");
string? carpetaDeSalida = Console.ReadLine();
Console.WriteLine("");
RecortarImagenes(rutasDeImagenes, carpetaDeSalida);

void RecortarImagenes(List<string> paths,string outputPath)
{
    Console.WriteLine("Procesando Imágenes: 0%");
    int totalImages=paths.Count;
    int imageNumber = 0;
    foreach (string path in paths)
    {
        Imagen imagen = new Imagen(new Bitmap(path));
        ManejoImagen manejo=new ManejoImagen(imagen);
        manejo.DefinirLimitesImagen();
        
        Bitmap MapaNuevaImagen = manejo.BitmapRecortado();
        int ladoSize = manejo.ladoSize(MapaNuevaImagen, 0.75);
        int[]initialPosition=manejo.PosicionInicial(MapaNuevaImagen, ladoSize);
        
        Imagen imagenRecortada = new(MapaNuevaImagen);
        Bitmap imagenFinal = manejo.EscalarBitmap(manejo.GenerarImagenFinal(ladoSize, initialPosition, imagenRecortada), 1200, 1200);
        string imagePath = outputPath+"\\" + imageNumber + ".jpg";
        imageNumber++;
        imagenFinal.Save(imagePath,ImageFormat.Jpeg);
        
        
        Console.WriteLine("Procesando Imágenes: "+Convert.ToInt32((Convert.ToDouble(imageNumber)/Convert.ToDouble(totalImages))*100)+"%");
    }
}

Console.WriteLine("Se Guardaron todas estas imágenes: ");
ListadorDeImagenes listaImagenesSalida = new ListadorDeImagenes(carpetaDeSalida);
List<string> rutasNuevasImagenes = listaImagenesSalida.rutasImagenes();

foreach(string ruta in rutasNuevasImagenes)
{
    Console.WriteLine(ruta);
}
Console.WriteLine("");
Console.WriteLine("Presione una tecla para finalizar: ");
Console.ReadLine();
Environment.Exit(0);
/*
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

foreach(string ruta in rutasDeImagenes)
{
    Console.WriteLine(ruta);
}
*/
