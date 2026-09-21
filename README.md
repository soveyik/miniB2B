# MiniB2B Web Uygulaması

MiniB2B, .NET 8 ve ASP.NET Core MVC kullanılarak geliştirilmiş bir B2B e-ticaret (sipariş) uygulamasıdır. Projenin kod yapısının daha düzenli olması için katmanlı mimari (N-Tier) kullanılmıştır.

## 1. Proje Yapısı ve Kaynak Kod

Uygulama 4 farklı katmana (projeye) bölünmüştür:

- **MiniB2B.Entities**: Veritabanındaki tabloların karşılığı olan sınıfları (Product, Order, vb.) barındırır.
- **MiniB2B.DataAccess**: Veritabanı bağlantılarını ve Entity Framework `DbContext` sınıfını içerir. Ayrıca ilk verilerin (admin, kategoriler vb.) eklendiği bölüm de buradadır.
- **MiniB2B.Business**: Uygulamanın iş mantığını içeren servis katmanıdır. (Ürün arama, stok kontrolleri, sipariş oluşturma işlemleri `ProductService`, `OrderService` gibi sınıflarda yapılır.)
- **MiniB2B.Web**: Kullanıcı arayüzünü (UI) sağlayan MVC projesidir. Controller ve View'lar bu katmanda yer alır.

**Temel Teknik Özellikler:**
- **Dinamik Grid**: Ürün listeleme tablosundaki kolonların sırası ve nelerin gösterileceği veritabanındaki `GridColumnConfigs` tablosundan okunarak çizilir.
- **Sipariş ve Stok İşlemleri**: Sipariş verilirken ürünün o anki fiyatı sipariş detaylarına kopyalanır. Stoktan düşme ve sepeti temizleme işlemleri hatalara karşı `Transaction` kullanılarak aynı anda yapılır.
- **Filtreleme**: Ürün arama ve listeleme işlemleri RAM'de (in-memory) değil, `IQueryable` ile doğrudan SQL veritabanı üzerinde yapılır.

---

## 2. Kullanılan Teknolojiler

- **.NET 8 (C#) & ASP.NET Core MVC**: Projenin backend kısmı.
- **Microsoft SQL Server**: Veritabanı sistemi.
- **Entity Framework Core (Code-First)**: Veritabanı işlemlerini yapmak için kullanılan ORM.
- **Cookie Authentication**: Kullanıcı giriş ve yetki işlemleri için.
- **Bootstrap 5**: Arayüz tasarımı için.
- **jQuery (AJAX)**: Sayfa yenilenmeden sepete ürün eklemek gibi asenkron işlemler için.

---

## 3. Veritabanı ve Bağlantı Ayarları

Veritabanı yapısı **Code-First** yöntemiyle hazırlandığı için harici bir SQL script dosyasına gerek yoktur. Projede istenen tüm tablolar (Kullanıcılar, Ürünler, Kategoriler, Sepet, Siparişler vb.) ve aralarındaki ilişkiler (Primary Key / Foreign Key) Entity Framework Core üzerinden tanımlanmış olup, veritabanı migration komutlarıyla otomatik oluşturulur.

**Bağlantı Dizesi (Connection String)**
Proje varsayılan olarak Visual Studio'nun LocalDB sunucusunu kullanır. Bağlantı dizesi `MiniB2B.Web\appsettings.json` dosyasında şu şekildedir:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MiniB2B;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
*(Eğer LocalDB kullanmıyorsanız bu adresi `Server=.\SQLEXPRESS;...` olarak kendi SQL Server adınıza göre değiştirebilirsiniz.)*

---

## 4. Kurulum ve Çalıştırma

Projeyi çalıştırmak için sırasıyla şu iki komutu terminal veya komut satırında çalıştırmanız yeterlidir:

### 1. Veritabanını Oluşturma
Projenin ana klasöründe (root) şu komutu çalıştırın. Bu komut hem tabloları oluşturur hem de gerekli ilk verileri (Seed Data) veritabanına ekler:
```bash
dotnet ef database update --project MiniB2B.DataAccess --startup-project MiniB2B.Web
```

### 2. Uygulamayı Başlatma
Aynı terminalde şu komutu çalıştırarak projeyi ayağa kaldırın:
```bash
dotnet run --project MiniB2B.Web
```
Terminalde yazan `http://localhost:5242` gibi bir adresten projeye giriş yapabilirsiniz.

---

## 5. Varsayılan Kullanıcı Bilgileri

Kurulum aşamasında otomatik olarak oluşturulan test kullanıcıları aşağıdadır (Şifreler veritabanında SHA-256 ile şifrelenmiştir):

**Yönetici (Admin) Girişi:**
- **Kullanıcı Adı:** `admin`
- **Şifre:** `123456`
*(Giriş yaptıktan sonra menüdeki "Admin Panel" kısmından siparişleri yönetebilirsiniz.)*

**Müşteri Girişi:**
- **Kullanıcı Adı:** `customer`
- **Şifre:** `123456`
