using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DataLayer.Models;

namespace Form115Console
{
    class Program
    {

        private const string PicDir = @"C:\Users\etudiant\Documents\GitHub\Form115\Form115\Areas\Admin\Uploads";
        private static readonly Form115Entities db = new Form115Entities();
        private const string Descrip = "Dans ce luxueux hôtel à proximité de la plage, vous profiterez pleinement de votre voyage dans cette ville paradisiaque..";


        static void Main(string[] args)
        {
            ajoutPhotosEtDescription();
            Console.Write("\n\nPress any key to continue . . . ");
            Console.ReadKey(true);
        }

        // Ajouter de la description et une photo aux hôtels 
        //si on veut rajouter plusieurs photos, faut il créer une table photo??

        private static int ajoutPhotosEtDescription()
        {

            var list = db.Hotels.Where(h => h.Photo == null || h.Description == null).ToList();
            Console.WriteLine(list.Count);

            var listPic = Directory.GetFiles(PicDir).Select(x => new FileInfo(x).Name).ToList();
            Console.WriteLine(listPic.Count);
            var stackPic = new Stack<string>();
            var cpt = 0;
            foreach (var item in listPic)
            {
                stackPic.Push(item);
            }

            foreach (var hotel in list)
            {
                hotel.Description = Descrip;
                hotel.Photo = stackPic.Pop();
                Console.Write(" {0} -", cpt++);
                Console.WriteLine();

            }
            db.SaveChanges();
            return list.Count;

        }
    }
}
