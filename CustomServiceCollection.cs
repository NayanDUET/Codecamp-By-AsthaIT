using ID;

namespace DI;

public class CustomServiceCollection
{
    private readonly List<ServiceDescriptor> _service = [];

    public void AddTransient<TServiceType, TImplementType>()
    {
        _service.Add(
            new ServiceDescriptor(
                typeof(TServiceType),
                typeof(TImplementType),
                ServiceLifeTime.Transiant
            )
        );
    }

    public void AddSingleton<TServiceType, TImplementType>()
    {
        _service.Add(
            new ServiceDescriptor(
                typeof(TServiceType),
                typeof(TImplementType),
                ServiceLifeTime.Singleton
            )
        );
    }

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(_service.AsReadOnly());
    }
}