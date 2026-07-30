namespace ID;

public enum ServiceLifeTime
{
    
     Transiant,
     Scoped,
     Singleton
}

public class ServiceDescriptor(Type servicetype,Type implementationtype,ServiceLifeTime lifeTime)
{
    
     public Type Servicetype {get;} = servicetype;
     public Type Implementationtype {get;} = implementationtype;
    public ServiceLifeTime LifeTime {get;} = lifeTime;

    public object ? SingletonInstance {get;set;}

    public object SingletonLock {get;set;}
}