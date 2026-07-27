using DI;
using Microsoft.Extensions.DependencyInjection;




//register
var service = new ServiceCollection();

service.AddTransient<ITransientService,TransientService>();
service.AddScoped<IScopedService,ScopedService>();
service.AddSingleton<ISingletonService,SingletonService>();

var serviceProvider = service.BuildServiceProvider();

//resolve
var tansientservice1 = serviceProvider.GetRequiredService<ITransientService>();
Console.WriteLine($"Transiant service ID: {tansientservice1.Id}");

var tansientservice2 = serviceProvider.GetRequiredService<ITransientService>();
Console.WriteLine($"Transiant service ID: {tansientservice2.Id}");

Console.WriteLine();

var singleTonservice1 = serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"Transiant service ID: {singleTonservice1.Id}");

var singleTonservice2 = serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"ingletonService ID: {singleTonservice2.Id}");

var singleTonservice3 = serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"ingletonService ID: {singleTonservice3.Id}");

Console.WriteLine();

using ( var scope = serviceProvider.CreateScope())
{
    var scopeService1 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService1.Id}");

    var scopeService2 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService2.Id}");

}

Console.WriteLine();

using ( var scope = serviceProvider.CreateScope())
{
    var scopeService1 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService1.Id}");

    var scopeService2 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService2.Id}");

}

Console.WriteLine("Test TransientS Service");

//register
var customservice = new CustomServiceCollection();

customservice.AddTransient<ITransientService,TransientService>();

var customServiceProvider = customservice.BuildServiceProvider();

//resolve

var customServiceProvider1 = serviceProvider.GetRequiredService<ITransientService>();

Console.WriteLine($"Transiant service ID: {customServiceProvider1.Id}");

var customServiceProvider2 = serviceProvider.GetRequiredService<ITransientService>();
Console.WriteLine($"ingletonService ID: {customServiceProvider2.Id}");