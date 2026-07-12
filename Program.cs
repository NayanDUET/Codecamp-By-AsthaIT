using DI;


var nofication = ObjectFactory<NotificationService>.Get();
nofication.Notify();
