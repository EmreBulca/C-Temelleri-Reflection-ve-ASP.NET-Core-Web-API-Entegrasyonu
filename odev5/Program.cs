using System;
using System.Collections.Generic;
using System.Reflection; 

namespace OdevConsoleApp
{
    // CEVAP: Struct "Değer Tipi"dir (Value Type) ve Stack hafızasında tutulur. 
    // Küçük veriler için Struct daha performanslıdır.
    public struct Student
    {
        // Özellikler 
        public int Id { get; set; }
        public string Name { get; set; }
        public double Gpa { get; set; } // Not Ortalaması

        // Constructor
        public Student(int id, string name, double gpa)
        {
            Id = id;
            Name = name;
            Gpa = gpa;
        }
        // verileri düzgün formatta göstersin diye ToString metodunu ezdik.
        public override string ToString()
        {
            return $"ID: {Id}, İsim: {Name}, Ortalama: {Gpa}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- 1.1 Struct ve Değer Tipleri ---");

            // Listeler dinamik dizilerdir, boyut belirtmemize gerek kalmaz.
            List<Student> students = new List<Student>();

            students.Add(new Student(1, "İrem", 3.5));
            students.Add(new Student(2, "Emre", 3.8));
            students.Add(new Student(3, "Türüdü", 2.9));

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }

            // Hata yönetimi testini çağırıyoruz.
            HataYonetimiTesti();

            Console.WriteLine("\n--- 1.3 Obsolete Attribute Testi ---");

            // Obsolete ne işe yarar?
            // Eski veya kullanılmaması gereken metotları işaretlemek için kullanılır.
            // Yazılımcıyı uyarır veya kodun derlenmesini engeller.

            // Bu metot çalışır ama Sarı ünlem verir.
            EskiMetot();

            // Bu metodu açarsan proje çalışmaz çünkü Kırmızı hata verecek şekilde ayarladık.
            // YasakliMetot(); 

            // Reflection raporunu çalıştırıyoruz.
            ReflectionRaporuYazdir();

            Console.WriteLine("\nÇıkmak için bir tuşa basın...");
            Console.ReadLine();
        }

        static void HataYonetimiTesti()
        {
            Console.WriteLine("\n--- 1.2 Exception Handling Testi ---");

            // Neden try-catch kullandın?
            // Uygulamanın çalışma anında çökmesini engellemek ve kullanıcıya anlaşılır hata mesajı vermek için.
            try
            {
                Console.Write("Bölünecek sayıyı giriniz: ");
                string giris1 = Console.ReadLine();
                int sayi1 = int.Parse(giris1); // Harf girerse burada patlar -> FormatException

                Console.Write("Bölen sayıyı giriniz: ");
                string giris2 = Console.ReadLine();
                int sayi2 = int.Parse(giris2);

                int sonuc = sayi1 / sayi2; // 0 girerse burada patlar -> DivideByZeroException

                Console.WriteLine($"Sonuç: {sonuc}");
            }
            catch (DivideByZeroException ex)
            {
                // Senaryo 1: Sıfıra bölme hatasını özel olarak yakalıyoruz.
                Console.WriteLine("HATA: Bir sayı sıfıra bölünemez! (DivideByZeroException)");
                Console.WriteLine($"Sistem Mesajı: {ex.Message}");
            }
            catch (FormatException ex)
            {
                // Senaryo 2: Sayı yerine harf girilmesini özel olarak yakalıyoruz.
                Console.WriteLine("HATA: Lütfen sadece sayısal değer giriniz! (FormatException)");
                Console.WriteLine($"Sistem Mesajı: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Diğer tüm öngörülemeyen hatalar için genel yakalayıcı.
                Console.WriteLine($"Beklenmeyen bir hata oluştu: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("--> Finally bloğu çalıştı: İşlem tamamlandı veya hata yönetildi.");
            }
        }

        // Attribute kullanımı örneği
        [Obsolete("Bu metot eski versiyondur. Lütfen YeniMetot() kullanın.")]
        static void EskiMetot()
        {
            Console.WriteLine("Eski metot çalıştı (Warning verdi ama durmadı).");
        }

        // isError=true diyerek bu kodun derlenmesini engelliyoruz.
        [Obsolete("Bu metot GÜVENLİK NEDENİYLE kaldırıldı! Kullanılması yasaktır.", true)]
        static void YasakliMetot()
        {
            Console.WriteLine("Bu kod asla çalışmayacak.");
        }

        // Custom Attribute nedir?
        // Sınıflara veya metotlara ekstra bilgi (Metadata) eklememizi sağlayan etiketlerdir.
        // AttributeUsage: Bu etiketin nerelere yapıştırılabileceğini belirler (Sınıf ve Metot).
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class DeveloperInfoAttribute : Attribute
        {
            public string DeveloperName { get; set; }
            public string Version { get; set; }
            public string Description { get; set; }

            public DeveloperInfoAttribute(string name, string version, string desc)
            {
                DeveloperName = name;
                Version = version;
                Description = desc;
            }
        }

        // Burada kendi yazdığımız attribute'u sınıfa uyguluyoruz.
        [DeveloperInfo("Proje Yöneticisi", "v1.0", "Banka işlemlerini içeren ana sınıf")]
        class BankaIslemleri
        {
            [DeveloperInfo("İrem", "v1.1", "Para çekme işlemini yapar.")]
            public void ParaCek()
            {
                Console.WriteLine("Para çekildi.");
            }

            [DeveloperInfo("Emre", "v1.2", "Para yatırma işlemini yapar.")]
            public void ParaYatir()
            {
                Console.WriteLine("Para yatırıldı.");
            }

            [DeveloperInfo("Türüdü", "v1.0", "Hesap bakiyesini görüntüler.")]
            public void BakiyeSorgula()
            {
                Console.WriteLine("Bakiye: 100 TL");
            }
        }

        // Reflection nedir?
        // Kodun çalışma zamanında kendi yapısını (sınıflarını, metotlarını, özelliklerini) çözümlemesidir.
        // Biz burada "BankaIslemleri" sınıfını dışarıdan tarayıp içindeki bilgileri okuyoruz.
        static void ReflectionRaporuYazdir()
        {
            Console.WriteLine("\n--- 1.4 Custom Attribute ve Reflection Raporu ---");

            // typeof: Bir sınıfın kimlik kartını alır.
            Type tip = typeof(BankaIslemleri);

            // Sınıfın tepesindeki [DeveloperInfo] etiketini okuyoruz.
            var sinifAttribute = (DeveloperInfoAttribute)Attribute.GetCustomAttribute(tip, typeof(DeveloperInfoAttribute));

            if (sinifAttribute != null)
            {
                Console.WriteLine($"SINIF: {tip.Name}");
                Console.WriteLine($"  -> Geliştirici: {sinifAttribute.DeveloperName}");
                Console.WriteLine($"  -> Versiyon: {sinifAttribute.Version}");
                Console.WriteLine($"  -> Açıklama: {sinifAttribute.Description}");
                Console.WriteLine("--------------------------------------------------");
            }

            // GetMethods: Sınıfın içindeki tüm metotları bir dizi olarak getirir.
            var metotlar = tip.GetMethods();

            foreach (var metot in metotlar)
            {
                // Her metodun tepesindeki [DeveloperInfo] etiketini kontrol ediyoruz.
                var metotAttribute = (DeveloperInfoAttribute)Attribute.GetCustomAttribute(metot, typeof(DeveloperInfoAttribute));

                if (metotAttribute != null)
                {
                    Console.WriteLine($"METOT: {metot.Name}");
                    Console.WriteLine($"  -> Geliştirici: {metotAttribute.DeveloperName}");
                    Console.WriteLine($"  -> Versiyon: {metotAttribute.Version}");
                    Console.WriteLine($"  -> Açıklama: {metotAttribute.Description}");
                    Console.WriteLine("  . . . . .");
                }
            }
        }
    }
}