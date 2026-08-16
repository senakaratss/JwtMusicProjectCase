# 🎵 JWT Music Platform

<p align="center">
  <img src="https://img.shields.io/badge/.NET-Core-512BD4?style=flat-square&logo=dotnet" alt=".NET Core" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=flat-square&logo=dotnet" alt="ASP.NET Core" />
  <img src="https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=flat-square" alt="EF Core" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/JWT-Authentication-000000?style=flat-square&logo=jsonwebtokens" alt="JWT" />
  <img src="https://img.shields.io/badge/ML.NET-Recommendation-blue?style=flat-square" alt="ML.NET" />
</p>

<p align="center">
  JWT tabanlı, kullanıcıların müzik dinleyebildiği, playlist oluşturabildiği, dinleme geçmişini görüntüleyebildiği ve paket seviyesine göre içeriklere erişebildiği <b>full-stack müzik platformu</b>.
</p>

---

## 📖 Proje Hakkında

**JWT Music Platform**, ASP.NET Core ile geliştirilmiş, JWT tabanlı kimlik doğrulama ve paket seviyesine (**Free / Premium / Elite**) göre içerik erişimi sunan bir müzik dinleme platformudur. Kullanıcılar şarkı dinleyebilir, playlist oluşturabilir, dinleme geçmişini takip edebilir; Elite paket sahipleri ise **ML.NET** tabanlı kişiselleştirilmiş şarkı önerilerinden faydalanır.

Proje; JWT Authentication, Role-Based Authorization, RESTful Web API ve ASP.NET Core Identity gibi konularda uçtan uca uygulamalı deneyim kazanmak amacıyla geliştirilmiştir.

---

## ✨ Öne Çıkan Özellikler

| Modül | Açıklama |
|---|---|
| 🔐 JWT Authentication | Token tabanlı, claim zengin kimlik doğrulama |
| 🎚️ Paket Bazlı Yetkilendirme | Free / Premium / Elite seviyelerine göre içerik erişimi |
| 🎧 MP3 Streaming | HTTP Range Processing ile kesintisiz dinleme |
| 📂 Playlist Yönetimi | Kullanıcıya özel, many-to-many playlist–şarkı ilişkisi |
| 🕐 Dinleme Geçmişi | Gün bazlı, kullanıcıya özel kayıt takibi |
| 🤖 ML.NET Öneri Sistemi | Elite kullanıcılara özel kişiselleştirilmiş öneriler |
| 🛡️ Role-Based Authorization | Admin ve kullanıcı rollerine göre yetkilendirme |
| 👨‍💼 Admin Panel | Kullanıcı, rol, paket ve içerik yönetimi |

---

## 🔐 Kimlik Doğrulama & Yetkilendirme

Platformdaki tüm kullanıcıya özel işlemler **JWT (JSON Web Token)** üzerinden doğrulanır. Login sonrasında üretilen token, UI tarafında **cookie** içerisinde saklanır ve her API isteğinde gönderilir.

**Token içindeki claim'ler:**

| Claim | Açıklama |
|---|---|
| `NameIdentifier` | Kullanıcı ID |
| `Email` | E-posta adresi |
| `Name` / `Surname` | Ad / Soyad |
| `PackageId` / `PackageName` / `PackageLevel` | Paket bilgileri |
| `Role` | Kullanıcı rolü |

Bu claim'ler sayesinde her istek, ekstra bir sorgu yapmadan kullanıcının kimliğini, paket seviyesini ve rolünü taşır. Aşağıdaki tüm modüller (playlist, dinleme geçmişi, içerik erişimi, admin işlemleri vb.) bu doğrulama mekanizmasını temel alır — bir kaynağa erişim isteğinde token'daki kullanıcı ID'si, kaynağın sahibiyle karşılaştırılarak **sahiplik kontrolü** yapılır.

- **Kullanıcı düzeyinde:** Kullanıcı yalnızca kendi profili, playlist'leri ve dinleme geçmişi üzerinde işlem yapabilir. *(Örn: Kullanıcı A, Kullanıcı B'nin playlist'ine şarkı ekleyemez.)*
- **Rol düzeyinde:** Admin işlemleri **Role-Based Authorization** ile korunur; admin alanına yalnızca `Admin` rolündeki kullanıcılar erişebilir.
- **Paket düzeyinde:** İçerik erişimi, token'daki `PackageLevel` claim'ine göre kontrol edilir; yetkisi olmayan kullanıcı ücretli bir içeriğe erişmeye çalıştığında içerik **kilitli** gösterilir.

---

## 🎚️ Paket Sistemi

```text
Free    → Temel içerikler
Premium → Premium içerikler
Elite   → Tüm içerikler + kişiselleştirilmiş öneriler
```

---

## 🎵 Müzik & Player

- Şarkı listeleme ve detay görüntüleme
- Sanatçı, tür ve paket bilgilerinin şarkılarla ilişkilendirilmesi
- Dinlenme sayısının otomatik artırılması
- Tarayıcı üzerinden çalışan bir **music player**; şarkı isteği API üzerinden audio stream olarak sunulur
- **HTTP Range Processing** ile kesintisiz MP3 streaming

---

## 📂 Playlist Sistemi

- Playlist oluşturma, listeleme ve detay görüntüleme
- Playlist'e şarkı ekleme ve içerikleri yönetme
- Playlist ↔ Song arasında **many-to-many** ilişki
- Kullanıcıya özel erişim

---

## 🕐 Dinleme Geçmişi

- Dinlenen şarkıların ve dinleme tarihlerinin kaydı
- Gün bazlı dinleme kayıtları
- Kullanıcıya özel görüntüleme

---

## 🤖 Şarkı Öneri Sistemi (ML.NET)

Kullanıcıların dinleme davranışlarından yararlanılarak **ML.NET** ile şarkı önerileri üretilir. Öneri sistemi yalnızca **Elite paket kullanıcılarına özel** olarak çalışır ve kullanıcı–şarkı etkileşimini temel alır. Önerilecek şarkı bulunmadığında öneri alanı gösterilmez; içerik Elite paketine özel kilitli olarak sunulur.

**Eğitim verisi modeli:**

```csharp
public class RecommendationTrainingData
{
    public string UserId { get; set; }
    public uint SongId { get; set; }
    public float Label { get; set; }
}
```

**Tahmin sonucu modeli:**

```csharp
public class RecommendationPrediction
{
    public float Score { get; set; }
}
```

---

## 👨‍💼 Admin Panel

Admin kullanıcıları için ayrı bir **Admin Area** bulunur ve Role-Based Authorization ile korunur. Yönetilebilen alanlar:

- Kullanıcılar ve roller
- Paketler ve paket seviyeleri
- Şarkılar ve içerik yönetimi

---

## 🧰 Kullanılan Teknolojiler

### Backend
C# · ASP.NET Core · ASP.NET Core Web API · ASP.NET Core MVC · Entity Framework Core · Microsoft SQL Server · ASP.NET Core Identity · JWT Authentication · AutoMapper · FluentValidation  · ML.NET

### Frontend
HTML5 · CSS3 · Bootstrap · JavaScript · Razor Views · AJAX

### Development & Testing Tools
Visual Studio · SQL Server Management Studio · Postman · Git / GitHub

---

## 📸 Ekran Görüntüleri
 
### 🖥️ Kullanıcı Arayüzü
 
<table>
  <tr>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/15d8a621-8984-4cc1-9a88-9f0560615f38" width="380"/><br/><b>Ana Sayfa</b></td>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/653c1a8e-1d73-44f4-ab91-0daadc1e4f81" width="380"/><br/><b>Kayıt Ol</b></td>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/aac6d641-c0b2-4ccc-99e3-a390970af919" width="380"/><br/><b>Giriş Ekranı</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/27c6a5cb-51a1-4f6a-908b-937e7750a5ec" width="380"/><br/><b>Şarkı Listesi (Öneriler)</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/6254e657-86b1-485c-8551-4b45dfd47b44" width="380"/><br/><b>Şarkı Listesi (Kilitli İçerikler)</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/87b41dc3-f731-4a3e-9ac3-c4f3e41072cc" width="380"/><br/><b>Sanatçı Listesi</b></td>
  </tr>
    <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/50ce8734-16f6-4ab4-83b6-9ebf81f909ff" width="380"/><br/><b>Sanatçı Detayı</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/2c04ea19-1acb-4aca-ba48-060a4956ed6d" width="380"/><br/><b>Kilitli İçerik</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/2cacf0e7-b2de-486b-a734-2ad6a40bcf77" width="380"/><br/><b>Playliste Ekle</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/2cff3205-7129-44bb-931e-30fd414e4eea" width="380"/><br/><b>Playlist Detayı</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/7a9a9d67-c7c5-451d-8049-f8bd29eff5d6" width="380"/><br/><b>Profil / Hesap Ayarları</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/2b646bb6-1a1a-4d71-8e07-be36de2cdf1c" width="380"/><br/><b>Dinleme Geçmişi</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/959ffaed-6b15-4e67-b684-f3689ba331da" width="380"/><br/><b>Playlist Listesi</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/abaec046-07cb-4fb7-a619-5d7722b7d60e" width="380"/><br/><b>Paket Seçimi</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/94ee1262-1f37-490d-ae71-9d767dcbb250" width="380"/><br/><b>Tür Listesi</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/fbdfa1e5-df10-45c4-b13e-dcb69618f547" width="380"/><br/><b>Tür Detayı</b></td>
    <td></td>
    <td></td>
  </tr>
</table>
### 🛠️ Admin Panel
 
<table>
  <tr>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/04f61f53-45ce-4825-b46a-b3779652b6f8" width="380"/><br/><b>Admin Dashboard</b></td>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/d6dd034d-759b-4fea-ab3b-9028c15dacd3" width="380"/><br/><b>Şarkı Yönetimi</b></td>
    <td align="center" width="33%"><img src="https://github.com/user-attachments/assets/8f214e9e-3daa-4d5e-9b50-4e3de73309e7" width="380"/><br/><b>Şarkı Ekle</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/625faf5e-4acf-4b04-a5c2-82a7e110e5f7" width="380"/><br/><b>Sanatçı Yönetimi</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/5d79380e-f012-46ce-b7d4-4614bab58735" width="380"/><br/><b>Sanatçı Düzenle</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/f030d557-7354-4758-9701-e51fd6100a23" width="380"/><br/><b>Tür Ekle</b></td>
  </tr>
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/3e531811-ecf3-48cd-a44e-688c3fe6fafd" width="380"/><br/><b>Paket Yönetimi</b></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/785c3482-f96f-4a9b-9a46-338d1f64b23e" width="380"/><br/><b>Kullanıcı Yönetimi</b></td>
    <td></td>
  </tr>
</table>
---
 
<p align="center">Made with ❤️ using ASP.NET Core & JWT</p>
