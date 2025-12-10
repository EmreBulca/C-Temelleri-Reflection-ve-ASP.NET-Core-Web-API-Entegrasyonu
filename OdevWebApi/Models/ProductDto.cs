using System.ComponentModel.DataAnnotations; 

namespace OdevWebApi.Models
{
    // DTO (Data Transfer Object), veritabanı tablolarımızı dış dünyadan gizlemek ve sadece API'nin ihtiyaç duyduğu verileri taşımak için kullanılan bir yöntemdir.
    // Güvenlik ve performans için "Entity" yerine "Dto" kullanmak best practice'dir.
    public class ProductDto
    {
        public int Id { get; set; }

        // Bunlara "Data Annotations" denir. Kullanıcıdan gelen veriyi daha Controller'a girmeden doğrular.
        // Eğer kurallara uyulmazsa, sistem otomatik olarak "400 Bad Request" hatası fırlatır.

        [Required(ErrorMessage = "Ürün adı boş bırakılamaz!")] // Null gelmesini engeller.
        [StringLength(50, ErrorMessage = "Ürün adı en fazla 50 karakter olabilir.")] // Veritabanı şişmesini veya saldırıları engeller.
        public string Name { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Fiyat 1 ile 10000 arasında olmalıdır.")] // Mantıksız veri girişini (negatif fiyat vb.) engeller.
        public decimal Price { get; set; }
    }
}