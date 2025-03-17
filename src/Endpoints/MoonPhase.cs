using diversa.Interfaces;

namespace diversa.Endpoints
{
    public class MoonPhase : IEndPoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/moonphase", (DateTime? date) => $"Moon phase is {GetMoonPhase(date ?? DateTime.Now)}");
        }

        public static string GetMoonPhase(DateTime date)
        {
            const double totalLengthOfCycle = 29.53059;
            const double julianConstant = 2415018.5;

            var referenceNewMoon = new DateTime(2000, 1, 6, 18, 14, 0, DateTimeKind.Utc);

            var julianDate = date.ToOADate() + julianConstant;
            var daysSinceReferenceNewMoon = referenceNewMoon.ToOADate() + julianConstant;

            var newMoons = (julianDate - daysSinceReferenceNewMoon) / totalLengthOfCycle;
            var daysIntoCycle = (newMoons - Math.Floor(newMoons)) * totalLengthOfCycle;

            if (daysIntoCycle < 1.84566) return "New Moon";
            if (daysIntoCycle < 5.53699) return "Waxing Crescent";
            if (daysIntoCycle < 9.22831) return "First Quarter";
            if (daysIntoCycle < 12.91963) return "Waxing Gibbous";
            if (daysIntoCycle < 16.61096) return "Full Moon";
            if (daysIntoCycle < 20.30228) return "Waning Gibbous";
            if (daysIntoCycle < 23.99361) return "Last Quarter";
            if (daysIntoCycle < 27.68493) return "Waning Crescent";
            return "New Moon";
        }
    }
}