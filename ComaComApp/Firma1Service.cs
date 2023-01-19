using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComaComApp_
{
    public class Firma1Service
    {
        public List<DzienPracy> FirmaList(string filePath)
        {
            List<DzienPracy> workDayList = new List<DzienPracy>();
            List<Firma1> firmaList = new List<Firma1>();
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Firma1 firma = new Firma1();
                    firma.EmployeeCode = Convert.ToString(values[0]);
                    firma.Date = Convert.ToDateTime(values[1]);
                    firma.InsideTime = TimeSpan.Parse(values[2]);
                    firma.OutsideTime = TimeSpan.Parse(values[3]);
                    firma.WorkTime = Convert.ToInt32(values[4]);
                    firmaList.Add(firma);
                }

                List<Firma1> firmaNoDupesList = firmaList.DistinctBy(p => new { p.EmployeeCode, p.Date }).ToList();

                foreach (var item in firmaNoDupesList)
                {
                    DzienPracy workDay = new DzienPracy();
                    workDay.KodPracownika = item.EmployeeCode;
                    workDay.Data = item.Date;
                    workDay.GodzinaWejscia = item.InsideTime;
                    workDay.GodzinaWyjscia = item.OutsideTime;
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
