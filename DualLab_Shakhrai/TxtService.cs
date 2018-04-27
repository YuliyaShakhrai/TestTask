using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NodaTime.Text;

namespace DualLab_Shakhrai
{
    public class TxtService : IFileService
    {
        public Schedule LoadData(string filepath)
        {
            var tempList = new List<BusRide>();
            string line = String.Empty;
            using (StreamReader file = new StreamReader(filepath))
            {
                LocalTimePattern pattern = LocalTimePattern.CreateWithInvariantCulture("HH:mm");

                while ((line = file.ReadLine()) != null)
                {
                    var tempStrings = line.Split(' ');
                    tempList.Add(new BusRide(tempStrings[0], pattern.Parse(tempStrings[1]).Value, pattern.Parse(tempStrings[2]).Value));
                }
            }
            return new Schedule(tempList);
        }

        public void UnloadData(string filepath, Schedule schedule)
        {
            using (TextWriter file = new StreamWriter(filepath))
            {
                int counter = 0;
                LocalTimePattern pattern = LocalTimePattern.CreateWithInvariantCulture("HH:mm");
                for (int i = 0; i < schedule._listRides.Count; i++)
                {
                    if (schedule._listRides[i].CompanyName == CompanyName.Posh)
                    {
                        file.WriteLine(schedule._listRides[i].CompanyName.ToString() + ' ' 
                            + schedule._listRides[i].Start.ToString("HH:mm", CultureInfo.InvariantCulture) + ' '
                            + schedule._listRides[i].End.ToString("HH:mm", CultureInfo.InvariantCulture));
                        counter++;
                    }
                }

                file.WriteLine();

                for (int i = counter; i < schedule._listRides.Count; i++)
                {
                    file.WriteLine(schedule._listRides[i].CompanyName.ToString() + ' ' 
                        + schedule._listRides[i].Start.ToString("HH:mm", CultureInfo.InvariantCulture) + ' ' 
                        + schedule._listRides[i].End.ToString("HH:mm", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
