var builder = WebApplication.CreateBuilder(args);

// AddControllers ne işe yarar?
// Projedeki Controller sınıflarını (ProductsController vb.) bulup sisteme tanıtır. 
// Bu sayede HTTP istekleri geldiğinde hangi metoda gideceğini bilir.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Swagger neden ekliyoruz?
// API'mizin dokümantasyonunu çıkarmak ve tarayıcı üzerinden test edebilmek için.
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// [HOCA SORARSA]: Pipeline (Boru Hattı) nedir?
// CEVAP: Gelen isteğin (Request) ve dönen yanıtın (Response) geçtiği işlem sırasıdır. 
// Kodlar yukarıdan aşağıya sırayla işlenir.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Neden UseMiddleware yazdık?
// Kendi yazdığımız "LoggingMiddleware" sınıfını devreye sokmak için.
// Bunu buraya yazdığımız için gelen her istek önce bizim loglama kodumuzdan geçiyor.
app.UseMiddleware<OdevWebApi.Middlewares.LoggingMiddleware>();

// MapControllers ne yapar?
// Controller içindeki [Route] attribute'larına bakarak URL eşleştirmelerini yapar.
app.MapControllers();

app.Run(); // Uygulamayı ayağa kaldırır.