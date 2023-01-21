# CaseEndeksa

Tapu Kadastro Genel Müdürlüğü, Parsel sorgulama sistemi üzerinden arazilere ait sorgulama yapabileceğimiz;
yaptığımız sorguları veri tabanına ve cache sistemine kaydedebileceğimiz bir uygulama geliştirilmiştir.

Uygulamada Kullanılan Teknolojiler & Araçlar:
- https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/parsel/... (TKGM parsel sorgulama servisi)
- .Net Core Web Api 6.0
- N- Tier Architecture
- Redis Cache
- ORM : Entity Framework
- MSSQL

Mimaride bulunan Repository katmanı DataAccess, Service katmanı ise Business katmanına denk gelmektedir.
Proje katmanlarının ortak olarak kullandığı classlar Core katmanı içerisinde tanımlanmıştır.
API katmanında ise swagger yardımı ile data operasyonları (CRUD) yapılabilmektedir.

GetAndAddTkgmData action'ı kullanıldığında; datalar tkgm api üzerinden getirilmekte, veri tabanı ve cache e kaydedilmektedir.
Eğer cache üzerinde data bulunmuyor ise; GetAll actionı üzerinden "There is no any data at redis cache." uyarısı dönecektir.
GetAndAddTkgmData action'a istek yapıldığında öncelikle datalar redis cache üzerinde bulunuyor mu kontrolü yapılır; eğer bu data
cache sisteminde bulunmuyor ise web servis üzerinden datalar çekilmektedir.

- Endeksa.API > Properties > launchSettings.json > default true olan launchBrowser ayarı "false" olarak değiştirilmiştir.
