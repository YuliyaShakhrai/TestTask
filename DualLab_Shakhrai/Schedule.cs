using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace DualLab_Shakhrai
{
    public sealed class Schedule
    {
        public List<BusRide> _listRides;

        public Schedule(IEnumerable<BusRide> list)
        {
            _listRides = list.ToList();
        }

        public void Optimize()
        {
            var rawList = _listRides.Select(r => (Ride: r, Delete: false)).ToArray();
            for (int i = 0; i < rawList.Length; i++)
            {
                if (rawList[i].Ride.Duration > Duration.FromHours(1))
                {
                    rawList[i].Delete = true;
                }

                for (int j = 0; j < rawList.Length; j++)
                {
                    if (i == j || rawList[i].Delete || rawList[j].Delete) continue;
                    if (rawList[i].Ride.StartInMinutes == rawList[j].Ride.StartInMinutes 
                        && rawList[i].Ride.EndInMinutes == rawList[j].Ride.EndInMinutes)
                    {
                        if (rawList[i].Ride.CompanyName == CompanyName.Grotty) rawList[i].Delete = true;
                        else rawList[j].Delete = true;
                        continue;
                    }

                    if ((rawList[i].Ride.StartInMinutes >= rawList[j].Ride.StartInMinutes) 
                        && (rawList[i].Ride.EndInMinutes <= rawList[j].Ride.EndInMinutes))
                    {
                        rawList[j].Delete = true;
                    }
                }
            }

            _listRides = rawList.Where(x => x.Delete == false).Select(x => x.Ride).ToList();
            _listRides = _listRides.OrderByDescending(r => r.CompanyName.ToString()).ThenBy(r => r.Start).ToList();
        }
    }
}
