using diversa.Interfaces;
using System.Globalization;

namespace diversa.Endpoints
{
    public class ChineseYear : IEndPoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/chineseyear", (DateTime? date) => $"Chinese zodiac year is {GetChineseYear(date ?? DateTime.Now)}");
        }

        private string GetChineseYear(DateTime date)
        {
            var chineseCalendar = new ChineseLunisolarCalendar();
            var sexagenaryYear = chineseCalendar.GetSexagenaryYear(date);
            var terrestrialBranch = chineseCalendar.GetTerrestrialBranch(sexagenaryYear);

            string[] zodiacSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            return zodiacSigns[terrestrialBranch - 1];
        }
    }
}