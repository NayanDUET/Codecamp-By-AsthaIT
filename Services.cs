namespace DI;

public interface ITransientService
{
    Guid Id{get;}
}

public class TransientService : ITransientService
{
    public Guid Id {get;} = Guid.NewGuid();
}

public interface IScopedService
{
    Guid Id{get;}
}

public class ScopedService : IScopedService
{
    public Guid Id {get;} = Guid.NewGuid();
}

public interface ISingletonService
{
    Guid Id{get;}
}

public class SingletonService : ISingletonService
{
    public Guid Id {get;} = Guid.NewGuid();
}
