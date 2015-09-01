using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DataLayer.Models;

namespace Form115Console
{
    class Program
    {

        //private const string PicDir = @"C:\Users\Sarta\Desktop\Form115-master\Form115\Areas\Admin\Uploads";
        //private static readonly Form115Entities db = new Form115Entities();

        //static void Main(string[] args)
        //{
        //    ajoutPhotos();
        //    Console.Write("\n\nPress any key to continue . . . ");
        //    Console.ReadKey(true);
        //}

        //private static int ajoutPhotos()
        //{

        //    var list = db.Hotels.Where(p => p.Photo == null).ToList();
        //    Console.WriteLine(list.Count);

        //    var listPic = Directory.GetFiles(PicDir).Select(x => new FileInfo(x).Name).ToList();

        //    var stackPic = new Stack<string>();
        //    foreach (var item in listPic)
        //    {
        //        stackPic.Push(item);
        //    }

        //    foreach (var _hotel in list)
        //    {

        //        _hotel.Photo = stackPic.Pop();
        //        Console.Write(" {0} -", _hotel.Photo);



        //        Console.WriteLine();


        //    }
        //    db.SaveChanges();
        //    return list.Count;



        //}
    }
}
