using web_api.Core;
using web_api.TcpServers;


var builder = WebApplicationFactory.CreateBuilder();

var app = builder.Build();

app.MapGet("/test",(RequestContext context)=>{

    return "My Name is nayan is back!";

});



// var server = new TcpServer(5000);
// await server.StartAsync();

await app.RunAsync(5005);