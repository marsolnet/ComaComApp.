
namespace ComaComApp
{
    public class Firma1Service
    {
        public List<DzienPracy> FirmaList(string filePath)
        {

            List<DzienPracy> values = File.ReadAllLines(filePath)
                                               .Select(v => FromCsv(v)).DistinctBy(p => new { p.KodPracownika, p.Data })
                                               .ToList();
            return values;
        }

        public static DzienPracy FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            DzienPracy workDay = new DzienPracy();
            workDay.KodPracownika = Convert.ToString(values[0]);
            workDay.Data = Convert.ToDateTime(values[1]);
            workDay.GodzinaWejscia = TimeSpan.Parse(values[2]);
            workDay.GodzinaWyjscia = TimeSpan.Parse(values[3]);
            return workDay;
            
        }
    }
}
