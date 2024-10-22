var builder = WebApplication.CreateBuilder(args);



// Loggin kisitlama
builder.Logging.ClearProviders();  // 4 saglayiciyi temizledik Bunlar ; Console, Debug, EventSource, EventLog:Wþndows  only
builder.Logging.AddConsole(); // Sadece Consolo giriþ yapicaksin
builder.Logging.AddDebug(); // Sadece hata ayiklamaya giris yapar

// Add services to the container.



// Posmanda jsondan baska formatlar de mesela XML format yoksa assagidaki kod sorgulucak ve hata firlaticak - 406 hatasi 
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters(); // XML formati icin bunu eklememiz gerekiyor



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
