using diversa.Interfaces;

namespace diversa.Endpoints
{
    public class TamrielDate : IEndPoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/tamrieldate", (DateTime? date) => $"Tamriel date is {ConvertToTamrielDate(date ?? DateTime.Now)}");
        }

        private static readonly string[] DaysOfWeek = { "Sundas", "Morndas", "Tirdas", "Middas", "Turdas", "Fredas", "Loredas" };
        private static readonly Dictionary<int, (string Month, string StarSign)> MonthsAndSigns = new()
        {
            {1, ("Morning Star", "The Ritual")},
            {2, ("Sun's Dawn", "The Lover")},
            {3, ("First Seed", "The Lord")},
            {4, ("Rain's Hand", "The Mage")},
            {5, ("Second Seed", "The Shadow")},
            {6, ("Midyear", "The Steed")},
            {7, ("Sun's Height", "The Apprentice")},
            {8, ("Last Seed", "The Warrior")},
            {9, ("Hearthfire", "The Lady")},
            {10, ("Frostfall", "The Tower")},
            {11, ("Sun's Dusk", "The Atronach")},
            {12, ("Evening Star", "The Thief")}
        };

        public static string ConvertToTamrielDate(DateTime date)
        {
            var dayOfWeek = DaysOfWeek[((int)date.DayOfWeek + 1) % 7];
            var day = date.Day;
            (string month, string starSign) = MonthsAndSigns[date.Month];

            return $"{dayOfWeek}, {day.ToString().PadLeft(2, '0')} of {month} ({starSign})";
        }
    }
}