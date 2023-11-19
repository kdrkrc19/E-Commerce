using E_Commerce_WebAPI.Extensions;
using E_Commerce_WebAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureSQLContext(builder.Configuration); //db i�lemleri i�in configure (connectionStringler)
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureDataShaper();
//builder.Services.AddCustomMediaTypes();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(configure =>
{
    configure.RespectBrowserAcceptHeader = true; //AcceptHeaderdan gelen d�n�� tipine izin verir. (i�erik pazarl��� yapar)
    configure.ReturnHttpNotAcceptable = true; //gelen iste�in d�n�� tipine izin verir
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
