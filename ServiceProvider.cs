using System.Net.NetworkInformation;
using ID;

namespace DI;

public class ServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _services;
    private readonly Dictionary<Type,Object>? _scopeCache;

    public ServiceProvider(IReadOnlyList<ServiceDescriptor> services)
    {
        _services = services;
    }

     public ServiceProvider(IReadOnlyList<ServiceDescriptor> services,bool isScope)
    {
        _services = services;

        if(isScope)
        {
            _scopeCache = [];
        }
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
                  ServiceLifeTime.Scoped => CreateScopedInstance(descriptor),
                  _=>throw new NotImplementedException()
        };
    }

    public ServiceScope CreateScope()
    {
        var scopePorvider = new ServiceProvider(_services,true);
        return new ServiceScope(scopePorvider);
    }

    public object CreateSingleTonInstance(ServiceDescriptor descriptor)
    {


        lock (descriptor.SingletonLock)
        {
             descriptor.SingletonInstance ??=CreateInstance(descriptor.Implementationtype);
         
            return descriptor.SingletonInstance;
        }
       
    }

    public object CreateScopedInstance(ServiceDescriptor descriptor)
    {
        if(_scopeCache == null) 
          throw new InvalidOperationException("Can not resolve service  from root provider");

        if(_scopeCache.TryGetValue(descriptor.Servicetype, out var instance)) return instance;

        instance = CreateInstance(descriptor.Implementationtype);

        _scopeCache[descriptor.Servicetype] = instance;
        return instance;
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

    internal void DisposeScopedInstance()
    {
        if(_scopeCache == null) return;

        foreach(var instance in _scopeCache.Values)
        {
            if(instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        _scopeCache.Clear();
    }
}