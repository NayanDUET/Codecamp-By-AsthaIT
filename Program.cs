using DI;
using Microsoft.Extensions.DependencyInjection;




//register
var service = new ServiceCollection();
service.AddTransient<NotificationService>();

service.AddTransient<IEmialService,EmailService>();

var serviceProvider = service.BuildServiceProvider();


//var nofication = ObjectFactory<NotificationService>.Get();

var nofication = serviceProvider.GetRequiredService<NotificationService>();
nofication.Notify();
