using Serilog;

var builder = WebApplication.CreateBuilder(args);



// Loggin kisitlama
// builder.Logging.ClearProviders();  // 4 saglayiciyi temizledik Bunlar ; Console, Debug, EventSource, EventLog:Wþndows  only
// builder.Logging.AddDebug(); // Sadece hata ayiklamaya giris yapar

// Add services to the container.


// -----------------------------------------------------------------------------------------------------------------------------------------

// Serilog ;

// once Nugetten assagidaki paketleri kurduk 
// Serilog.AspNetCore
// Serilog.Sinks.File

// Sonra ;

//Log.Logger = new LoggerConfiguration()
//   .MinimumLevel.Information()
//   .WriteTo.File("Log/Log.txt", rollingInterval: RollingInterval.Minute) // Burada metin dosyasi her dakika olusturulacak (Demo icin dakika olarak ayarladim)
//.WriteTo.File("Log/Log.txt", rollingInterval: RollingInterval.Day) // Burada her bir gun icin metin dosyasi olusturacak
//   .CreateLogger();

// Sonrasinda ;

//builder.Host.UseSerilog();

// Hem yerlesik hem serilog kullanmak istiyorsak;
//builder.Logging.AddSerilog();


// -----------------------------------------------------------------------------------------------------------------------------------------


// Log4Ne ; 

// once Nugetten assagidaki paketleri kurduk 
// Microsoft.Extensions.Logging.Log4Net.AspNetCore

// Sonra ;


builder.Logging.AddLog4Net();

// Daha sonrasin da log4net.config adinda bir konfigurasyon dosyasi olusturmamiz gerekiyor bunun icin;

// projeye sag tik add New item  biraz assagýda " web configuration file  "  sablo seciyoruz ve ismine " log4net.config " veriyoruz.

// https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore sitedeki configuration alanini kopyalayim olusturdugumuz
// configuration sablonun icerisini sitip yapistiriyaruz

// 













// -----------------------------------------------------------------------------------------------------------------------------------------




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
