using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace RecortadorDeImagenes
{
    public class ListadorDeImagenes
    {
        private string RutaCarpeta { get; set; }

        private string[] extensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        public ListadorDeImagenes(string rutaCarpeta) 
        {
            RutaCarpeta = rutaCarpeta;
        }

        public List<string> rutasImagenes()
        {
            List<string> roots = new List<string>();
            string[] files = Directory.EnumerateFiles(RutaCarpeta,"*.*",SearchOption.TopDirectoryOnly)
                .Where(f=>extensions.Contains(Path.GetExtension(f),StringComparer.OrdinalIgnoreCase)).ToArray();  

            foreach(string file in files) 
            {
                roots.Add(file);
            }

            return roots;
        }

      

    }
}
