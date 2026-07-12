namespace DI;

public class ObjectFactory<T>
{
    public static T Get(string key)
    {

        if (key.Equals("Notification-service", StringComparison.CurrentCultureIgnoreCase))
        {
             var email = ObjectFactory<IEmialService>.Get("email-service");
             return (T)(object)new NotificationService(email);
        }
        if (key.Equals("email-service", StringComparison.CurrentCultureIgnoreCase))
        {
            return (T)(object)new EmailService();
        }

        throw new ArgumentException($"no implementation found for {key}");
        
    }
}