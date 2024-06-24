using GST.Library.Middleware.HttpOverrides.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<ForwardedHeadersOptions>(options => {
//    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
//    options.KnownNetworks.Clear();
//    options.KnownProxies.Clear();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.All,
//    RequireHeaderSymmetry = false,
//    ForwardLimit = null,
//    KnownNetworks = { new IPNetwork(IPAddress.Parse("::ffff:172.17.0.1"), 104) }
//});

//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//});

app.UseGSTForwardedHeaders(new GST.Library.Middleware.HttpOverrides.Builder.ForwardedHeadersOptions
{
    ForwardedHeaders = (GST.Library.Middleware.HttpOverrides.ForwardedHeaders)(ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto),
    ForceXForxardedOrRealIp = true,
});


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
