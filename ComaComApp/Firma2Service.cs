
namespace ComaComApp
{
    public class Firma2Service
    {
        public List<DzienPracy> FirmaList(string filePath)
        {

            List<DzienPracy> values = File.ReadAllLines(filePath)
                                               .Select(v => FromCsv(v))
                                               .ToList();
            var t = values;
            var workList = values.GroupBy(x => new { x.KodPracownika, x.Data }).Select(x => new DzienPracy
            {
                KodPracownika = x.FirstOrDefault().KodPracownika,
                Data = x.FirstOrDefault().Data,
                GodzinaWejscia = x.Where(r => !string.IsNullOrEmpty(r.GodzinaWejscia.ToString())).Select(r => r.GodzinaWejscia).FirstOrDefault(),
                GodzinaWyjscia = x.Where(r => !string.IsNullOrEmpty(r.GodzinaWyjscia.ToString())).Select(r => r.GodzinaWyjscia).FirstOrDefault(),
            }).ToList();

            return workList;
        }

        public static DzienPracy FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            DzienPracy workDay = new DzienPracy();
            workDay.KodPracownika = Convert.ToString(values[0]);
            workDay.Data = Convert.ToDateTime(values[1]);

            if (values[3] == "WE")
            {
                workDay.GodzinaWejscia = TimeSpan.Parse(values[2]);
            }
            else
            {
                workDay.GodzinaWyjscia = TimeSpan.Parse(values[2]);
            }

            return workDay;
        }
    }
}
