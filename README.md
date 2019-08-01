# ADManager
## Bilgilendirme ##
ADManager, Windows Aktif Dizin yapısı  kullanılan ortamlarda; AD sunucusu ile entegre çalışan, AD kullanıcı ve Cihaz Yönetim Uygulamasıdır. Saha IT ekibimizn talebi üzerine geliştirme kararı alınmış ve  açık kaynak olarak dağıtımı planlanmıştır.
Bu uygulama, 3. taraf uygulama ya da sunucu arayüzü kullanmadan Aktif Dizin yapınızda birçok düzenleme yapılmasını sağlar.

Bu uygulama ile ilk aşamada  planlanan  :
- Aktif Dizin veritabanından tüm kullanıcıları listeleme,
- AD veri tabanına kullanıcı ekleme,
- AD veri tabanında kullanıcı sorgulama,
- AD veri tabanından kullanıcı silme,
- AD veri tabanında kullanıcıyı pasif yapma,
- AD veri tabanında pasif kullanıcıyı aktifleştirme,
- AD veri tabanında kullanıcının üye gruplarını listeleme,
- Kullanıcıya admin yetkisi atama,
- Kullanıcıdan admin yetkisi alma,
- AD veri tabanında Admin yetkisindeki kullanıcıları listeleme,
- Kullanıcı parolasını değiştirme,
- Kullanıcı hareketlerini görme,
- Kullanıcı bazlı ya da toplu sorgu sonucu raporlama,
- AD veri tabanından kayıtlı cihazları listeleme,
- AD veri tabanında cihaz sorgulama,
- AD veri tabanına cihaz ekleme,
- Cihazlara uzaktan direktif uygulama,
- Cihaz bazlı yada toplu sorgu sonucu raporlama işlemleri,
- AD Sunucusunda GPO düzenlemesi yapılması,
- AD sunucusunudan  GPO dökümü alınması,
- AD sunucusunda OU tanımlaması ve kullanıcı - OU ataması yapılması,
- Kullanıcı - OU taşıma işlemleri,
- AD sunucu sağlık analizi yapılması ve fazlasıdır.

## Çalışma Ortamı  ##
Uygulama  test ve gerçek ortamında ;
- Windows 7 ,
- Windows 8 ,
- Windows 10 istemci işletim sistemi ailesinde sorunsuz çalışmıştır.  
   Sunucu tarafında ise ; 
 - Windows Server 2012 R2 Standart
 - Windows Server 2016 R2 Standart AD sunucuları ile sorunsuz çalışmıştır
 ## Ldap Uyumu ##
 Uygulama LDAP2 ve LDAP3 ile uyumlu çalışmaktadır.  
 
## Gereksinimler ##
- Windows istemci cihaz  (Sunucu - client işletim sistemi ailesi)
- .Net Framework 4.5 + 

## Kurulum ## 
Kurulum dosyasını [buradan](Kurulum) indirilerek kurulum başlatılabilir.

## Ön Düzenleme ## 
Kurulum yaptıktan sonra yapılandırma dosyasında değişiklikler yapmamız gerekecektir.  
> Uygulama Kurulduğu dizin\Admanager.exe.config   
  
Bazı parametreleri (Ldap bağlantı adresi, domain sunucu adı ve domain adı) config dosyasındabelirtmemiz gerekir.~~
  ```
  <appSettings>
    <add key="path" value=""/>
    <add key="domainServer" value=""/>
    <add key="computerDomain" value=""/>
  </appSettings>
```  
**path** :  Domain Sunucu Ldap bağlantı yolu . Örn: LDAP://mydomainservername.mydomain.local  
**domainServer** : Domain sunucusunu adı. Örn: mydomainservername.mydomain.local (domainfqdn)   
**computerDomain** : Domainizin adı. Örn : mydomain.local  

Bu ayarları yaptıktan sonra uygulama kullanıma hazır hale gelecektir.

## Uygulama Arayüzü Kullanım ##

### 1. Uygulama Ana Form ###
Uygulama kimlik doğrulama başarılı olduktan sonra  [Ana Ekrana](../master/ScreenShots/Home.png) yönlendirir. An Ekran'da ilgili formlar için yölendirme butonları yer almaktadır.   
### 2. Kullanıcı İşlemleri Formu ###
AD veritabanına kullanıcısı ile ilgili tüm süreç [bu form](../master/ScreenShots/KullaniciArayuz.png)) üzerinden yürütülür. 
- AD Sunucularında kullanıcı kaydı yaparken kullaniciadi.soyadi vb. küçük harf ve türkçe olmayan karakterle kayıt oluşturulması(kullanıcı Adı) tavsiye edilir.  
Uygulama, kayıt esnasında türkçe karakter de girmiş olsanız arka planda ingilizce karakter karşılığında kaydeder.    
ilgili Sınıf : CharConvertion  
ilgili kod bloğu : 
```
static string ConvertTRCharToENChar(string textToConvert)
        {
            return String.Join("",textToConvert.Normalize(NormalizationForm.FormD)
            .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }
```
> Girilen: şaziye.test  
   Kaydedilen: saziye.test olacaktır
   
#### 2.1. Parola Politikası ####
Kullanıcı esnasında kullanıcı için oluşturulan parola, default GPO(Group Policy Object)   parola politikanıza uygun olmalıdır. uygulama uygunsuzluk durumunda uyarı verir ve kullanıcıyı kaydetmez.


  














