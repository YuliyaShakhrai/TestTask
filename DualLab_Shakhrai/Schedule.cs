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
            var result = new List<BusRide>();
            _listRides = _listRides.OrderBy(x => x.Start).ToList();

            foreach (var ride in _listRides)
            {
                if (ride.Duration > Duration.FromHours(1))
                    continue;

                if (result.Count == 0)
                {
                    result.Add(ride);
                    continue;
                }

                if (ride.StartInMinutes == (result[result.Count - 1].StartInMinutes)
                    && ride.EndInMinutes == (result[result.Count - 1].EndInMinutes))
                {
                    if (ride.CompanyName == CompanyName.Posh)
                    {
                        result.RemoveAt(result.Count - 1);
                        result.Add(ride);
                        continue;
                    }
                    else continue;
                }

                if (ride.StartInMinutes >= (result[result.Count - 1].StartInMinutes) 
                    && ride.EndInMinutes <= (result[result.Count - 1].EndInMinutes))
                {
                    result.RemoveAt(result.Count - 1);
                    result.Add(ride);
                    continue;
                }

                result.Add(ride);
            }

            _listRides = result.OrderByDescending(r => r.CompanyName.ToString()).ThenBy(r => r.Start).ToList();
        }
    }
}
