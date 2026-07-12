using DI;


var nofication = ObjectFactory<NotificationService>.Get("Notification-service");
nofication.Notify();
