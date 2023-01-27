using ComaComApp;

class Program
{
    public static void Main(string[] args)
    {
       
        try
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
                            try
                            {
                                Firma1Service firma1Service = new Firma1Service();
                                var firma1 = firma1Service.FirmaList(file);
                                Console.WriteLine($"Firma1: liczba pozycji:\t{firma1.Count}");
                            }
                            catch
                            {
                                Console.WriteLine($"Firma1: Błąd - plik posiada puste pola w wymaganych kolumnach.");
                            }
                        }
                        else
                        {
                            try
                            {
                                Firma2Service firma2Service = new Firma2Service();
                                var firma2 = firma2Service.FirmaList(file);
                                Console.WriteLine($"Firma2: liczba pozycji:\t{firma2.Count}");
                            }
                            catch
                            {
                                Console.WriteLine($"Firma2: Błąd - plik posiada puste pola w wymaganych kolumnach.");
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine($"Prosimy o zamieszczenie plików CSV w katalogu: C:\\storage ");
        }

    }
}

