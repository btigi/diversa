using diversa.Interfaces;

namespace diversa.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var classes = assemblies
                            .Distinct()
                            .SelectMany(x => x.GetTypes())
                            .Where(x => typeof(IEndPoint).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var classe in classes)
            {
                var instance = Activator.CreateInstance(classe) as IEndPoint;
                instance?.MapEndpoint(app);
            }
        }
    }
}