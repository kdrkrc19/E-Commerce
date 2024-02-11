using AspNetCoreRateLimit;
using E_Commerce_WebAPI.Extensions;
using E_Commerce_WebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureSQLContext(builder.Configuration); //db i�lemleri i�in configure (connectionStringler)
builder.Services.ConfigureRepositoryManager(); 
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureDataShaper();
//builder.Services.AddCustomMediaTypes();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureResponseCaching(); //Cacheleme
//builder.Services.ConfigureHttpCacheHeaders(); //Marvin Cache Headers'� kullanmak i�in
builder.Services.ConfigureSpecificHttpCacheHeaders();
builder.Services.AddAuthentication();//do�rulama(kullan�c� var m� yok mu)
builder.Services.ConfigureIdentity();//kullan�c�n�n yetkilerini belirleme(authorization) Authentication olmadan authorization yap�lamaz
builder.Services.ConfigureJWT(builder.Configuration);


builder.Services.AddControllers(configure =>
{
    configure.RespectBrowserAcceptHeader = true; //AcceptHeaderdan gelen d�n�� tipine izin verir. (i�erik pazarl��� yapar)
    configure.ReturnHttpNotAcceptable = true; //gelen iste�in d�n�� tipine izin verir
    //configure.CacheProfiles.Add("MyCache", new CacheProfile() { Duration = 300}); //Cache profili olu�turma
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
