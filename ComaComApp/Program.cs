using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ComaComApp_;
using ComaComApp;

class Program
{
    public static void Main(string[] args)
    {

        var filePathDirectory = @"c:\storage";
        string[] files = Directory.GetFiles(@filePathDirectory, "*.csv");

        if (files.Count() == 0)
        {
            Console.WriteLine($"Prosimy o zamieszczenie plików CSV w katalogu: C:\\storage ");
        }
        else
        {
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    var values = System.IO.File.ReadLines(file).Skip(1).First().Split(';');
                    if (values.Length > 4)
                    {
                        Firma1Service firma1Service = new Firma1Service();
                        var firma1 = firma1Service.FirmaList(file);
                        Console.WriteLine($"Firma1: liczba pozycji:\t{firma1.Count}");
                    }
                    else
                    {
                        Firma2Service firma2Service = new Firma2Service();
                        var firma2 = firma2Service.FirmaList(file);
                        Console.WriteLine($"Firma2: liczba pozycji:\t{firma2.Count}");
                    }
                }
            }
        }
        
    } 
}

