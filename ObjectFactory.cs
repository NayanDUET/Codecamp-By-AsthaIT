namespace DI;

public class ObjectFactory<T>
{
    public static T Get()
    {

        if (typeof(T) == typeof(NotificationService))
        {
             var email = ObjectFactory<IEmialService>.Get();
             return (T)(object)new NotificationService(email);
        }
        if (typeof(T) == typeof(IEmialService))
        {
            return (T)(object)new EmailService();
        }

        throw new ArgumentException($"no implementation found for{typeof(T)}");
        
    }
}