using NodaTime;

namespace DualLab_Shakhrai
{
    public sealed class BusRide
    {
        public CompanyName CompanyName { get; }
        public LocalTime Start { get; }
        public LocalTime End { get; }
        public Duration Duration { get; }
        public int Day;
        public int StartInMinutes { get; }
        public int EndInMinutes { get; }

        public BusRide(string companyName, LocalTime start, LocalTime end)
        {
            if (companyName == CompanyName.Posh.ToString())
            {
                this.CompanyName = CompanyName.Posh;
            }
            else
            {
                this.CompanyName = CompanyName.Grotty;
            }
            this.Start = start;
            this.End = end;
            this.Day = 0;

            this.Duration = (this.End - this.Start).ToDuration();

            if (this.Duration.TotalMinutes < 0)
            {
                this.Duration += Duration.FromDays(1);
                this.Day = 1;
            }

            this.StartInMinutes = this.Day * 24 * 60 + this.Start.Hour * 60 + this.Start.Minute;
            this.EndInMinutes = this.Day * 24 * 60 + this.End.Hour * 60 + this.End.Minute;
        }
    }
}
