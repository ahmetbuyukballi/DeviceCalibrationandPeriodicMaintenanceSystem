# Cihaz Kalibrasyon ve Periyodik Bakım Sistemi

Bu proje, cihazların kalibrasyon ve periyodik bakım süreçlerini yönetmek için geliştirilmiş, katmanlı mimariye sahip bir .NET 8 ASP.NET Core Web API uygulamasıdır. 
Sistem; cihaz yönetimi, bakım planlama, bakım kayıtları, geri bildirim ve bildirim süreçlerini merkezi olarak yönetmeyi amaçlar.

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

 
