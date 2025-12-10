using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection; 
using System.Collections.Generic;

namespace OdevWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        // Bu Endpoint'in amacı nedir?
        // Projemizdeki tüm Controller ve Action'ların haritasını dinamik olarak çıkarmak.
        // Yeni bir Controller eklesem bile burayı değiştirmeme gerek kalmadan otomatik algılar.
        [HttpGet("attribute-map")]
        public IActionResult GetAttributeMap()
        {
            //  Assembly nedir?
            //  Şu an çalışan kodun derlenmiş halidir (DLL veya EXE). 
            // "GetExecutingAssembly" diyerek "Şu an çalıştığımız projenin içindekileri getir" diyoruz.
            var assembly = Assembly.GetExecutingAssembly();

            // 2. Sadece "Controller" olanları buluyoruz
            var controllers = assembly.GetTypes()
                // Bu Where koşulu ne yapıyor?
                // Her sınıfı değil, sadece 'ControllerBase'den miras alan, soyut olmayan (Abstract) 
                // ve isminde "Controller" geçen sınıfları filtreliyoruz.
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract && type.Name.EndsWith("Controller"))
                .Select(controllerType => new
                {
                    ControllerName = controllerType.Name,

                    // 3. Her Controller'ın içindeki Metotları (Action) buluyoruz
                    // BindingFlags ne işe yarar?
                    // Hangi metotları istediğimizi filtreler.
                    // Instance: Static olmayanlar, Public: Herkese açık olanlar.
                    // DeclaredOnly: EN ÖNEMLİSİ BU. Miras alınan (ToString, Equals vb.) metotları getirme, sadece benim yazdıklarımı getir demek.
                    Actions = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                        .Where(m => !m.IsSpecialName) // [HOCA SORARSA]: IsSpecialName nedir? -> Property'lerin get/set metotlarını eler.
                        .Select(method => new
                        {
                            MethodName = method.Name,
                            ReturnType = method.ReturnType.Name,

                            // 4. Metodun üzerindeki Attribute'ları (HttpGet, Post vb.) okuyoruz
                            // GetCustomAttributes: Metodun tepesine yapıştırılmış [HttpGet], [Authorize] gibi etiketleri okur.
                            Attributes = method.GetCustomAttributes().Select(attr => attr.GetType().Name).ToList()
                        })
                        .ToList()
                })
                .ToList();

            return Ok(controllers);
        }
    }
}