namespace DI;

//heigher level
public class NotificationService(IEmialService emailService)
{
    private readonly IEmialService _emailService = emailService; 
     public void Notify()
    {
            _emailService.SendEmail();
    }
}

public interface IEmialService
{
    
     public void SendEmail();
    
}
//lower level
public class EmailService : IEmialService
{
    
     public void SendEmail()
    {
        
         Console.WriteLine("this is email service");
    }
}