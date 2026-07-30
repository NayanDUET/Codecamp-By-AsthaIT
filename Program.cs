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



//register
var customservice = new CustomServiceCollection();

customservice.AddTransient<ITransientService,TransientService>();
customservice.AddSingleton<ISingletonService,SingletonService>();
customservice.AddScoped<IScopedService,ScopedService>();

var customServiceProvider = customservice.BuildServiceProvider();

//resolve
Console.WriteLine("Test TransientS Service");


var customServiceProvider1 = serviceProvider.GetRequiredService<ITransientService>();

Console.WriteLine($"Transiant service ID: {customServiceProvider1.Id}");

var customServiceProvider2 = serviceProvider.GetRequiredService<ITransientService>();
Console.WriteLine($"tringletonService ID: {customServiceProvider2.Id}");

//resolve
Console.WriteLine("Test Singleton Service");

var customISingletonServiceProvider1 = serviceProvider.GetRequiredService<ISingletonService>();

Console.WriteLine($"SingleTon service ID: {customISingletonServiceProvider1.Id}");

var customISingletonServiceProvider2 = serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"Singleton service ID: {customISingletonServiceProvider2.Id}");

Console.WriteLine("Test Scoped Service");

using ( var scope = serviceProvider.CreateScope())
{
    var scopeService1 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService1.Id}");

    var scopeService2 = scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"ScopedService ID: {scopeService2.Id}");

}