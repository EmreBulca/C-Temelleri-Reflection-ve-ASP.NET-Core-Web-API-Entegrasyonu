# 🚀 C# Temelleri, Reflection ve ASP.NET Core Web API Mimarisi

Bu proje, C# programlama dilinin derinliklerini (Struct, Reflection, Attributes) ve modern **ASP.NET Core Web API** mimarisini (Middleware, Filters, Routing) tek bir çatı altında birleştiren kapsamlı bir çalışmadır.

Proje iki ana modülden oluşmaktadır:
1.  **Console Application:** C# temelleri ve Reflection ile metadata analizi.
2.  **Web API:** RESTful servisler, özel Middleware katmanları ve kendi kendini analiz eden (Self-Reflective) endpoint mimarisi.

---

## 📚 Proje İçeriği

### 1. Console Uygulaması (Temel Yapılar)
Bu bölümde C#'ın yapı taşları ve çalışma zamanı (Runtime) özellikleri test edilmiştir.
* **Struct vs Class:** `Student` struct'ı üzerinden değer tiplerinin (Value Type) davranışı incelenmiştir.
* **Exception Handling:** `try-catch-finally` blokları ile `DivideByZeroException` ve `FormatException` gibi hatalar özel olarak yönetilmiştir.
* **Custom Attributes:** `[DeveloperInfo]` adında özel bir nitelik (attribute) geliştirilmiş ve sınıflara uygulanmıştır.
* **Reflection Raporu:** Kod çalışırken kendi üzerindeki sınıfları ve metotları tarayarak, geliştirici notlarını okuyan dinamik bir raporlama sistemi kurulmuştur.

### 2. ASP.NET Core Web API (Backend)
Modern web geliştirme standartlarına uygun, genişletilebilir bir API mimarisi kurulmuştur.
* **CRUD İşlemleri:** `ProductsController` üzerinden ürün ekleme, silme ve listeleme işlemleri (In-Memory veri yapısı ile).
* **Model Validation:** `ProductDto` üzerinde `[Required]`, `[Range]` gibi Data Annotations kullanılarak veri bütünlüğü sağlanmıştır.
* **Custom Middleware (Loglama):** Gelen her HTTP isteğini (Request) ve dönen yanıtı (Response) konsola yazan, trafiği izleyen bir ara katman geliştirilmiştir.
* **Action Filter (Performans Ölçümü):** Metotların çalışma sürelerini milisaniye cinsinden ölçen bir `TimingFilter` yazılmıştır.
* **System Reflection Endpoint:** API, `SystemController` üzerinden projedeki tüm Controller ve Action'ları tarayarak kendi dokümantasyon haritasını JSON formatında çıkaran akıllı bir endpoint'e sahiptir.

---

## 🛠 Kullanılan Teknolojiler

* **Dil:** C#
* **Framework:** .NET 6.0 / .NET 7.0
* **Platform:** ASP.NET Core Web API & Console App
* **Araçlar:** Visual Studio 2022, Swagger UI
* **Kavramlar:** Reflection, Middleware, Action Filters, Dependency Injection, RESTful Architecture.

---

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  Bu repoyu klonlayın:
    ```bash
    git clone [https://github.com/KULLANICI_ADIN/REPO_ADIN.git](https://github.com/KULLANICI_ADIN/REPO_ADIN.git)
    ```
2.  Proje dosyasını (`.sln`) Visual Studio ile açın.

### Console Uygulamasını Test Etmek İçin:
* Solution Explorer'da **ConsoleApp** projesine sağ tıklayın -> **"Set as Startup Project"** seçin.
* `F5` tuşuna basarak çalıştırın. Siyah ekranda struct ve reflection raporlarını göreceksiniz.

### Web API'yi Test Etmek İçin:
* Solution Explorer'da **WebApi** projesine sağ tıklayın -> **"Set as Startup Project"** seçin.
* `F5` tuşuna basın. Tarayıcıda **Swagger** arayüzü açılacaktır.
* Endpoint'leri test ederken açılan siyah konsol penceresinden **Middleware Loglarını** takip edebilirsiniz.

---

## 📷 Ekran Görüntüleri

### API Dokümantasyonu (Swagger) & Reflection Haritası
*(Buraya Swagger ekran görüntüsünü veya JSON çıktısını koyabilirsin)*

### Middleware Logları
*(Buraya siyah konsol ekranındaki [LOG] çıktılarının görüntüsünü koyabilirsin)*

---

## 👨‍💻 Geliştirici

**Adı Soyadı:** [Emre Bulca]  
**Öğrenci No:** [16008124031]  

Bu proje [Nesneye Yönelik Programlama] dersi kapsamında geliştirilmiştir.
