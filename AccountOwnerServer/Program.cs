using AccountOwnerServer.Extension;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureAccountOwner(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add new Services 
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();//app.UseStaticFiles() enables using static files for the request. If we don’t set a path to the static files, it will use a wwwroot folder in our solution explorer by default.


app.UseForwardedHeaders(new ForwardedHeadersOptions //app.UseForwardedHeaders will forward proxy headers to the current request. This will help us during the Linux deployment.
{
    ForwardedHeaders=ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
