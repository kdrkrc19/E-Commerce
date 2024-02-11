using AspNetCoreRateLimit;
using E_Commerce_WebAPI.Extensions;
using E_Commerce_WebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureSQLContext(builder.Configuration); //db iþlemleri için configure (connectionStringler)
builder.Services.ConfigureRepositoryManager(); 
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureDataShaper();
//builder.Services.AddCustomMediaTypes();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureResponseCaching(); //Cacheleme
//builder.Services.ConfigureHttpCacheHeaders(); //Marvin Cache Headers'ý kullanmak için
builder.Services.ConfigureSpecificHttpCacheHeaders();
builder.Services.AddAuthentication();//doðrulama(kullanýcý var mý yok mu)
builder.Services.ConfigureIdentity();//kullanýcýnýn yetkilerini belirleme(authorization) Authentication olmadan authorization yapýlamaz
builder.Services.ConfigureJWT(builder.Configuration);


builder.Services.AddControllers(configure =>
{
    configure.RespectBrowserAcceptHeader = true; //AcceptHeaderdan gelen dönüþ tipine izin verir. (içerik pazarlýðý yapar)
    configure.ReturnHttpNotAcceptable = true; //gelen isteðin dönüþ tipine izin verir
    //configure.CacheProfiles.Add("MyCache", new CacheProfile() { Duration = 300}); //Cache profili oluþturma
})  
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCsvFormatter()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swag => swag.OperationFilter<MyHeaderFilter>()); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseResponseCaching();   //Cacheleme

app.UseHttpCacheHeaders(); //Marvin Cache

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
