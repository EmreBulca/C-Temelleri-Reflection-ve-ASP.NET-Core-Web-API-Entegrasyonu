using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace OdevWebApi.Middlewares
{
    // Middleware nedir?
    // İstemci (Client) ile bizim Controller'ımız arasında duran bir "Ara Katman"dır.
    // Gelen isteği karşılar, işlem yapar ve sonraki adıma devreder. Bir nevi gümrük kapısı gibidir.
    public class LoggingMiddleware
    {
        // RequestDelegate nedir?
        // Boru hattındaki (Pipeline) "bir sonraki adımı" temsil eder.
        // Bizim işimiz bitince topu kime atacağımızı bu değişken sayesinde biliriz.
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Invoke metodu ne zaman çalışır?
        // Her HTTP isteği (Request) geldiğinde ASP.NET Core bu metodu otomatik tetikler.
        public async Task Invoke(HttpContext context)
        {
            // --- İSTEK (REQUEST) ANI ---
            // Buradaki kodlar, Controller daha çalışmadan ÖNCE çalışır.
            Console.WriteLine($"[LOG] İstek Geldi: {context.Request.Method} - {context.Request.Path} | Zaman: {DateTime.Now}");

            // "await _next(context)" satırını silersek ne olur?
            // "Short-circuit" (Kısa devre) olur. İstek burada takılır kalır, Controller'a asla gitmez.
            // Bu satır, "Ben işimi bitirdim, sıradaki çalışsın" demektir.
            await _next(context);

            // --- BÖLÜM 2: YANIT (RESPONSE) ANI ---
            // Buradaki kodlar, Controller işini bitirip geriye döndükten SONRA çalışır.
            // Yani Request giderken değil, Response dönerken buraya düşeriz.
            Console.WriteLine($"[LOG] Yanıt Döndü: Durum Kodu {context.Response.StatusCode}");
        }
    }
}