using ID;

namespace DI;

public class ServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _services;

    private readonly Dictionary<Type, object> _singletonInstances = new();

    public ServiceProvider(IReadOnlyList<ServiceDescriptor> services)
    {
        _services = services;
    }

    public T GetService<T>()
    {
        return (T)GetService(typeof(T));
    }

    public object GetService(Type serviceType)
    {
        
        var descriptor = _services.FirstOrDefault(x => x.Servicetype == serviceType);

        if (descriptor == null)
            throw new Exception($"Service {serviceType.Name} is not registered.");

        switch (descriptor.LifeTime)
        {
            case ServiceLifeTime.Singleton:

                if (_singletonInstances.TryGetValue(serviceType, out var instance))
                    return instance;

                instance = CreateInstance(descriptor.Implementationtype);

                _singletonInstances[serviceType] = instance;

                return instance;

            case ServiceLifeTime.Transiant:

                return CreateInstance(descriptor.Implementationtype);

            default:
                throw new NotImplementedException("Scoped is not implemented.");
        }
    }

    private object CreateInstance(Type implementationType)
    {
        var constructor = implementationType.GetConstructors().First();

        var parameters = constructor.GetParameters();

        if (parameters.Length == 0)
        {
            return Activator.CreateInstance(implementationType)!;
        }

        var dependencies = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            dependencies[i] = GetService(parameters[i].ParameterType);
        }

        return Activator.CreateInstance(implementationType, dependencies)!;
    }
}