using MessagePack.Resolvers;
using MessagePipe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMessagePipe()
    .AddMessagePipeTcpInterprocess("127.0.0.1", 23800, configure =>
    {
        configure.HostAsServer = true;
        configure.MessagePackSerializerOptions = StandardResolver.Options;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
