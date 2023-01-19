using ComaComApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComaComApp_
{
    public class Firma2Service
    {
        public List<DzienPracy> FirmaList(string filePath)
        {
            List<DzienPracy> workDayList = new List<DzienPracy>();
            List<Firma2> firmaList = new List<Firma2>();
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Firma2 firma = new Firma2();
                    firma.EmployeeCode = Convert.ToString(values[0]);
                    firma.Date = Convert.ToDateTime(values[1]);
                    if (values[2] != "") { firma.Time = TimeSpan.Parse(values[2]); }
                    firma.InOut = Convert.ToString(values[3]);
                    firmaList.Add(firma);
                }

                List<Firma2> firmaNoDupesList = firmaList.DistinctBy(p => new { p.EmployeeCode, p.Date, p.InOut }).ToList();
                List<Firma2> employeeNoDupesList = firmaList.DistinctBy(p => new { p.EmployeeCode, p.Date }).ToList();

                foreach (var item in employeeNoDupesList)
                {
                    DzienPracy workDay = new DzienPracy();
                    workDay.KodPracownika = item.EmployeeCode;
                    workDay.Data = item.Date;
                    var godzinaWejscia = firmaNoDupesList.FirstOrDefault(x => x.EmployeeCode == item.EmployeeCode && x.Date == item.Date && x.InOut == "WE");

                    if (godzinaWejscia != null)
                    {
                        workDay.GodzinaWejscia = godzinaWejscia.Time;
                    }

                    var godzinaWyjscia = firmaNoDupesList.FirstOrDefault(x => x.EmployeeCode == item.EmployeeCode && x.Date == item.Date && x.InOut == "WY");

                    if (godzinaWyjscia != null)
                    {
                        workDay.GodzinaWyjscia = godzinaWyjscia.Time;
                    }

                    workDayList.Add(workDay);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

            return workDayList;
        }
    }
}
