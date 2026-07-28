using System.Net.NetworkInformation;
using ID;

namespace DI;

public class ServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _services;

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
        
        var descriptor = _services.FirstOrDefault(x=>x.Servicetype == serviceType) ?? throw new Exception($"Service of type {serviceType.Name } is not registrered");

        return descriptor.LifeTime switch{
            
                  ServiceLifeTime.Transiant => CreateInstance(descriptor.Implementationtype),
                  ServiceLifeTime.Singleton => CreateSingleTonInstance(descriptor),
                  _=>throw new NotImplementedException()
        };
    }

    public object CreateSingleTonInstance(ServiceDescriptor descriptor)
    {
        
        //if(descriptor.SingletonInstance == null)
        //{
            
           ///  var instance = CreateInstance(descriptor.Implementationtype);
           //  descriptor.SingletonInstance = instance;
      //  }

        descriptor.SingletonInstance ??=CreateInstance(descriptor.Implementationtype);
         
         return descriptor.SingletonInstance;
    }

    private object CreateInstance(Type implementationType)
    {
       var ctor = implementationType.GetConstructors();
       var firstConstractor = ctor.FirstOrDefault()
        ?? throw new Exception($"No public constructior is foud this type {implementationType.Name}");

        var depts = firstConstractor.GetParameters()
                     .Select(p=>GetService(p.ParameterType))
                     .ToArray();

        
        return Activator.CreateInstance(implementationType,depts);


    }
}