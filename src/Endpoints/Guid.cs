using diversa.Interfaces;

namespace diversa.Endpoints
{
    public class GetGuid : IEndPoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/getguid", (int? count = 1) => $"{string.Join(", ", GetGuids(count))}");
        }

        public static IEnumerable<string> GetGuids(int? count)
        {
            foreach (var i in Enumerable.Range(0, count ?? 1))
            {
                yield return Guid.NewGuid().ToString();
            }
        }
    }
}