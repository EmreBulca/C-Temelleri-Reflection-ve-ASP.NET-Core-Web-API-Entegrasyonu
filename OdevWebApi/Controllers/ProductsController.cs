using Microsoft.AspNetCore.Mvc;
using OdevWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdevWebApi.Controllers
{
    // Bu attribute'lar ne işe yarıyor?
    // URL'in nasıl olacağını belirler (api/products).
    // [ApiController] ise bu sınıfın bir API olduğunu belirtir ve Validasyon (Model doğrulama) işlemlerini otomatik yapar.
    [Route("api/[controller]")]
    [ApiController]
    // Neden 'Controller' değil de 'ControllerBase'den miras aldın?
    // Çünkü biz View (HTML sayfası) döndürmüyoruz, sadece veri (JSON) döndürüyoruz.
    // 'Controller' sınıfı MVC (View desteği) içindir, 'ControllerBase' ise Web API (Saf veri) içindir. Daha hafiftir.
    public class ProductsController : ControllerBase
    {
        // Neden listeyi 'static' yaptın?
        // Veritabanımız olmadığı için verileri RAM'de tutuyoruz.
        // Eğer 'static' yapmasaydım, her istekte (Request) Controller yeniden oluşturulur ve liste sıfırlanırdı.
        // Static yaparak uygulama açık kaldığı sürece verilerin korunmasını sağladım.
        private static List<ProductDto> _products = new List<ProductDto>()
        {
            new ProductDto { Id = 1, Name = "Laptop", Price = 5000 },
            new ProductDto { Id = 2, Name = "Mouse", Price = 100 }
        };

        // 1. Listeleme (GET api/products)
        // IActionResult nedir?
        // HTTP durum kodlarını (200 OK, 404 NotFound vb.) ve veriyi birlikte döndürmemizi sağlayan bir arayüzdür.
        [HttpGet]
        public IActionResult Get()
        {
            // Ok() metodu geriye "200 Success" kodu ve veriyi döner.
            return Ok(_products);
        }

        // 2. ID ile Getirme (GET api/products/5)
        // "{id}" ne anlama geliyor?
        // URL'den parametre alacağımızı belirtir. Örn: api/products/5 dediğimizde 5 sayısı 'id' değişkenine atanır.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // LINQ kullanarak ID'si eşleşen ilk ürünü arıyoruz.
            var product = _products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                // Eğer ürün yoksa "404 Not Found" döneriz. Bu standarttır.
                return NotFound("Ürün bulunamadı.");
            }
            return Ok(product);
        }

        // 3. Ekleme (POST api/products)
        [HttpPost]
        // [FromBody] nedir?
        // Gelen isteğin gövdesindeki (Body) JSON verisini alıp 'ProductDto' nesnesine dönüştürür.
        public IActionResult Post([FromBody] ProductDto newProduct)
        {
            // Basit bir ID atama mantığı (Gerçekte DB otomatik yapar).
            newProduct.Id = _products.Count + 1;
            _products.Add(newProduct);

            // Neden 'CreatedAtAction' kullandın, direkt Ok() dönmedin?
            // REST standartlarına göre, yeni bir veri oluştuğunda "201 Created" kodu dönülmelidir.
            // Ayrıca yanıtın Header kısmında yeni ürünün linkini (api/products/3 gibi) verir.
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        // 4. Silme (DELETE api/products/5)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound("Silinecek ürün bulunamadı.");
            }

            _products.Remove(product);

            // Neden NoContent() döndün?
            // Silme işlemi başarılı olduğunda geriye veri dönmeye gerek yoktur.
            // "204 No Content" kodu, "İşlem başarılı ama sana gösterecek bir verim yok" demektir.
            return NoContent();
        }
    }
}