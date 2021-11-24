var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (c) => {
    if (!c.Request.Query.ContainsKey("hostname")) {
        await c.Response.WriteAsync("Please provide a hostname parameter in the query string...");
        return;
    }
    var hostname = c.Request.Query["hostname"];
    var req = await System.Net.Dns.GetHostEntryAsync(hostname);
    await c.Response.WriteAsync($"The address {hostname} resolves to the following addresses: {String.Join(", ", req.AddressList.Select(x => x.ToString()))}");
});

app.Run();
