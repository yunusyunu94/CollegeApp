using AutoMapper;
using CollegeApp.Configuration;
using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



// Loggin kisitlama
// builder.Logging.ClearProviders();  // 4 saglayiciyi temizledik Bunlar ; Console, Debug, EventSource, EventLog:W�ndows  only
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

builder.Logging.ClearProviders(); //dahili kaydedicileri tevizliyoruz

builder.Logging.AddLog4Net();

// Daha sonrasin da log4net.config adinda bir konfigurasyon dosyasi olusturmamiz gerekiyor bunun icin;

// projeye sag tik add New item  biraz assag�da " web configuration file  "  sablo seciyoruz ve ismine " log4net.config " veriyoruz.

// https://github.com/huorswords/Microsoft.Extensions.Logging.Log4Net.AspNetCore sitedeki configuration alanini kopyalayim olusturdugumuz
// configuration sablonun icerisini sitip yapistiriyaruz

// Kay�t seviyelerini appsettings.json alt�nda appsettings.Development.json icerisinde Information n� degistirerel seviyeyi ayarlaya biliyoruz 
//Bunlar ;

// ALL : T�m mesajlar�n logland��� seviyedir.
// DEBUG: Developement a�amas�na y�nelik loglama seviyesidir.
// INFO: Uygulaman�n �al��mas� s�ras�nda yararl� olabilece�ini d���nd���m�z durum bilgilerini loglayabilece�imiz seviyedir.
// WARN: Hata olmayan fakat �nemli bir durumun olu�tu�unu belirtebilece�imiz seviye.
// ERROR: Hata durumunu belirten seviye. Sistem hala �al���r haldedir.
// FATAL: Uygulaman�n sonlanaca��n�, faaliyet g�steremeyece�ini belirten mesajlar i�in kullan�lacak seviyedir.
// OFF: Hi� bir mesaj�n loglanmad��� seviyedir.

// Konsolda hicbirsey olmayacaktir konsoluda eklemek icin log4net.config icerisine yazd�k












// -----------------------------------------------------------------------------------------------------------------------------------------


// CollageDBContext icin servisleri eklememiz gerek ;
builder.Services.AddDbContext<CollageDBContext>(options =>
{
    options.UseSqlServer(
       /*"Data Source=.;Initial Catalog=CollegeAppDB;Integrated Security=True;Trust Server Certificate=True"*/ // Baglanti dizisini appsettings.json a ekledil oradan okuyacagiz

       builder.Configuration.GetConnectionString("CollegeAppDBConnection")

        );


});

// Sonrasinda appsettings.json a gidicez



// -----------------------------------------------------------------------------------------------------------------------------------------




// Posmanda jsondan baska formatlar de mesela XML format yoksa assagidaki kod sorgulucak ve hata firlaticak - 406 hatasi 
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters(); // XML formati icin bunu eklememiz gerekiyor



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// -----------------------------------------------------------------------------------------------------------------------------------------


// AUTAMAPPER ;

// Nugetten bunlar� kurduk ;
// AutoMapper
// AutoMapper.Extensions.Microsoft.DependencyInjection

// Configuration adli klas�r olustur ve bir class olusturup " AutoMapperConfig.cs : Profile " dan miras almali ve Constroctur olu�turup icerisine
// CreateMap<Student, StudentDTO>(); eklemelisin sonra assag�daki gibi



builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);


// -----------------------------------------------------------------------------------------------------------------------------------------



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
