# Cihaz Kalibrasyon ve Periyodik Bakım Sistemi

Bu proje, cihazların kalibrasyon ve periyodik bakım süreçlerini yönetmek için geliştirilmiş, katmanlı mimariye sahip bir .NET 8 ASP.NET Core Web API uygulamasıdır. 
Sistem; cihaz yönetimi, bakım planlama, bakım kayıtları, geri bildirim ve bildirim süreçlerini merkezi olarak yönetmeyi amaçlar.

# Proje Amacı

Cihazların bakım ve kalibrasyon süreçlerini dijital ortamda takip etmek, planlamak ve kayıt altına almak için ölçeklenebilir ve sürdürülebilir bir backend API altyapısı sunar.

# Proje katmanlı mimari (layered architecture) prensibine göre tasarlanmıştır
Domain
 ├── Entity sınıfları
 └── Temel domain modelleri

ApplicationCore
 ├── Abstractions (Interface’ler)
 ├── Concrete (Servis implementasyonları)
 ├── BaseService
 ├── DTO yapıları
 └── İş kuralları

Infrastructure
 └── Veri erişim ve dış servis entegrasyonları

Web API
 ├── Controller’lar
 ├── Middleware
 ├── Swagger yapılandırması
 └── API başlangıç ayarları

#  Özellikler

 Cihaz yönetimi (CRUD işlemleri)

 Kalibrasyon ve bakım takibi

 Periyodik bakım planı oluşturma

 Bakım kayıt geçmişi tutma

 Geri bildirim (feedback) yönetimi

 Bildirim loglama altyapısı

 Token & Refresh Token model yapısı

 AES şifreleme servis soyutlaması

 Global exception middleware

 DTO tabanlı veri transferi

 Dependency Injection uyumlu yapı

 Swagger / OpenAPI dokümantasyonu

 Temiz katmanlı mimari

# Kullanılan Teknolojiler

.NET 8

ASP.NET Core Web API

Katmanlı Mimari

Dependency Injection

Swagger / OpenAPI

DTO Pattern

Middleware tabanlı hata yönetimi

Cache servis soyutlaması

Token model altyapısı

# Kurulum
 Gereksinimler

.NET 8 SDK

Visual Studio 2022 / Rider / VS Code

SQL Server (veya yapılandırılmış farklı bir veritabanı)

Git


 # Projeyi Klonla
 git clone https://github.com/<ahmetbuyukballi>/<DeviceCalibrationandPeriodicMaintenanceSystem>.git
cd DeviceCalibrationandPeriodicMaintenanceSystem

# Paketleri Yükle
dotnet restore

# Uygulamayı Çalıştır
dotnet run --project DeviceCalibrationAndPeriodicMaintenanceSystemm
# Derleme
dotnet build

# API Dokümantasyonu (Swagger)
https://localhost:<port>/swagger

# Modüller
🔹 Cihazlar

Cihaz ekleme

Cihaz güncelleme

Cihaz silme

Cihaz listeleme

Cihaz detay görüntüleme

🔹 Bakım Planları

Periyodik bakım planı oluşturma

Plan güncelleme

Plan silme

🔹 Bakım Kayıtları

Bakım işlemi kaydetme

Geçmiş bakım kayıtlarını görüntüleme

Kayıt güncelleme

🔹 Geri Bildirim

Feedback ekleme

Güncelleme

Silme

Listeleme

🔹 Bildirimler

Bildirim log yapısı

Bildirim servis soyutlaması

🔹 Kullanıcı & Token

Token DTO yapıları

Refresh token modelleri

Claim tabanlı base servis desteği

# Güvenlik Altyapısı

Projede aşağıdaki güvenlik yapıları için altyapı hazırdır:

Token modelleri

Refresh token akışı

Claim tabanlı servisler

AES şifreleme soyutlaması

Genişletilebilir kimlik doğrulama yapısı

# API Testi

Endpoint’leri test etmek için:

Swagger UI

Postman

Proje içindeki .http dosyası

kullanılabilir.
