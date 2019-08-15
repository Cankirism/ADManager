# Aktif Dizin Entegre Yönetim Uygulaması # 
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ADManager&metric=alert_status)](https://sonarcloud.io/dashboard?id=ADManager)
![GitHub last commit](https://img.shields.io/github/last-commit/Cankirism/AdManager)
[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)](https://sonarcloud.io/dashboard?id=ADManager)

## Bilgilendirme ##
ADManager, Windows Aktif Dizin yapısı  kullanılan ortamlarda; AD sunucusu ile entegre çalışan, AD kullanıcı ve Cihaz Yönetim Uygulamasıdır. Saha IT ekibimizn talebi üzerine geliştirme kararı alınmış ve  açık kaynak olarak dağıtımı planlanmıştır.
Bu uygulama, 3. taraf uygulama ya da sunucu arayüzü kullanmadan Aktif Dizin yapınızda birçok düzenleme yapılmasını sağlar.

Bu uygulama ile ilk aşamada  planlanan  :
- Aktif Dizin veritabanından tüm kullanıcıları listeleme,
- AD veritabanına kullanıcı ekleme,
- AD veritabanında kullanıcı sorgulama,
- AD veritabanından kullanıcı silme,
- AD veritabanında kullanıcıyı pasif yapma,
- AD veritabanında pasif kullanıcıyı aktifleştirme,
- AD veritabanında kullanıcının üye gruplarını listeleme,
- Kullanıcıya admin yetkisi atama,
- Kullanıcıdan admin yetkisi alma,
- AD veritabanında Admin yetkisindeki kullanıcıları listeleme,
- Kullanıcı parolasını değiştirme,
- Kullanıcı hareketlerini görme,
- Kullanıcı bazlı ya da toplu sorgu sonucu raporlama,
- AD veritabanından kayıtlı cihazları listeleme,
- AD veritabanında cihaz sorgulama,
- AD veritabanına cihaz ekleme,
- Cihazlara uzaktan direktif uygulama,
- Cihaz bazlı yada toplu sorgu sonucu raporlama işlemleri,
- AD Sunucusunda GPO düzenlemesi yapılması,
- AD sunucusunudan  GPO dökümü alınması,
- AD sunucusunda OU tanımlaması ve kullanıcı - OU ataması yapılması,
- Kullanıcı - OU taşıma işlemleri,
- AD sunucu sağlık analizi yapılması ve fazlasıdır. 

![logo](../master/ScreenShots/uyg_logo.png)  

## Çalışma Ortamı  ##
Uygulama  test ve gerçek ortamında ;
- Windows 7 ,
- Windows 8 ,
- Windows 10 istemci işletim sistemi ailesinde sorunsuz çalışmıştır.  
   Sunucu tarafında ise ; 
 - Windows Server 2012 R2 Standart
 - Windows Server 2016 R2 Standart AD sunucuları ile sorunsuz çalışmıştır
 ## Ldap Uyumu ##
 Ldap(LightWeight Directory Access Protocol), istemci ile sunucu haberleşmesinde kullanılan protokoldür.  
 Uygulama LDAPv2 ve LDAPv3 ile uyumlu çalışmaktadır.  
 
## Gereksinimler ##
- Windows İşletim Sistemi (Sunucu - client işletim sistemi ailesi)
- .Net Framework 4.5 + 
- Ms Excel uygulaması (Raporlama)

## Kurulum ## 
Kurulum dosyası [buradan](Kurulum) indirilerek kurulum başlatılabilir.

## Ön Düzenleme ## 
Uygulamada AD Dizin hizmetleri için .Net Framework 3.5 ile gelen System.DirectoryServices.AccountManagement namespace'i tercih edilmiştir.
- Temel dizin hizmetleri tanımlaması için PrincipialContext sınıfı, kullanıcı işlemleri için UserPrincipial, Cihaz işlemlerinde ComputerPrincipial sınıfı kullanılmıştır.

Kurulum tamamlandıktan sonra yapılandırma dosyasında bazı değişiklikler yapmamız gerekecektir.  
> Uygulama Kurulduğu dizin\Admanager.exe.config   
  
Bazı parametreleri (Ldap bağlantı adresi, domain sunucu adı ve domain adı) config dosyasında belirtmemiz gerekir.
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
AD veritabanına kullanıcısı ile ilgili tüm süreç [bu form](../master/ScreenShots/KullaniciArayuz.png) üzerinden yürütülür. 
- AD Sunucularında kullanıcı kaydı yaparken kullaniciadi.soyadi vb. küçük harf ve türkçe olmayan karakterle kayıt oluşturulması(kullanıcı Adı) tavsiye edilir.  
Uygulama, kayıt esnasında türkçe karakter de girmiş olsanız arka planda ingilizce karakter karşılığında kaydeder.    
ilgili Sınıf : EnglishChar  
ilgili Method  : 
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
- Kullanıcı kaydetme esnasında kullanıcı için oluşturulan parola, default GPO(Group Policy Object) parola politikanıza uygun olmalıdır.  
- Uygulama uygunsuzluk durumunda uyarı verir ve kullanıcıyı kaydetmez.
- Kullanıcı kaydı yaparken "Aktif" seçimi yapılarak kaydı yapılmalıdır (Kullanıcıyı aktif yapmak için)
#### 2.2 Kullanıcı Kodları ####
Kullanıcıların AD veritabanında kulanıcı durum kodları(attribute/flag) bulunmaktadır.  
Bunlardan bazıları şu şekildedir:


        Property Flag   Value In Hexadecimal       Value In Decimal    
                SCRIPT            0x0001                  1  
              ACCOUNTDISABLE      0x0002                  2
              PASSWD_CANT_CHANGE  0x0040                 64
              NORMAL_ACCOUNT      0x0200                 512
              Disabled Account    0x0202                 514 
              Enabled, Password 
              Not Required        0x0220                 544 
              Disabled, Password
                Not Required      0x0222                 546 
              DONT_EXPIRE
                _PASSWORD         0x10000                65536
              Enabled, Password
                Doesn’t Expire    0x10200                66048   
              Disabled, Password
                Doesn’t Expire    0x10202                66050   
              Disabled, Password
                Doesn’t Expire
                &Not Required     0x10222                 66082   x

               DONT_REQ_PREAUTH   0x400000                4194304
               PASSWORD_EXPIRED   0x800000                8388608
               PARTIAL_SECRETS
                _ACCOUNT          0x04000000              67108864

  Uygulama üzerinde aşağıda yer aldığı şekilde belirtilmiştir.;  
  
            _durumKodlari.Add(512, "Normal Hesap");

            //Uygulama üzerinden kullanıcı  eklendiğinde aktif ise 544 görünür.
            _durumKodlari.Add(544, "Aktif-Parola Gerektirmiyor");

            //Uygulama üzerine kullanıcı kayıt esnasında Pasif seçilmiş ise .
            _durumKodlari.Add(546, "Pasif- Parola Gerektirmiyor");

            //Pasif kullanıcı
            _durumKodlari.Add(514, "Pasif");

            // 66*** kodları parola süre limiti olmadığı (doesn't expire) zamanlardır.
            _durumKodlari.Add(66048, "Aktif- Password Doesn't expire");
            _durumKodlari.Add(66082, "Pasif- Password Doesn't expire");
            _durumKodlari.Add(66050, "Pasif- Password Doesn't expire,not required");  
            
            
            
Daha fazla bilgi [buradan](https://support.microsoft.com/en-us/help/305144/how-to-use-useraccountcontrol-to-manipulate-user-account-properties) alınabilir.  
#### 2.3. Kullanıcı Kayıt ####
ilgili form üzerinde şu parametreleri girilerek kullanıcı kaydı yapılır :  
- Kullanıcı Adı
- Kullanıcı Soyadı 
- Kullanıcının AD kullanıcı adı (SamAccountName) / Uygulama otomatik olarak adi.soyadi şeklinde oluşturur.  
- Kullanıcının Parolası (GPO ile uyumlu)  

Uygulama, Kullanıcıyı başarılı şekilde kaydettikten sonra, Kullanıcının AD Ortamında ilk oturum açılışında yeni parola oluşturmasını sağlar.  

İlgili Sınıf: BussinessUser
İlgili Method : CreateUserAccount  

                    userPrincipial.Enabled = true;
                    userPrincipial.Save();
                    userPrincipial.ExpirePasswordNow();
#### 2.4. Diğer Özellikler ####
Kullanıcılar ile ilgili diğer düzenlemeler Context Menü aracılığıyla yapılır. Datagrid üzerinde ilgili kullanıcının satırı seçilir ve sağ tık ile menü açılır.  

                      
 ### 3. Bilgisayar İşlemleri ###
 AD veritabanında kayıtlı kullanıcı bilgisayarları ile işlemlerin yapıldığı [formdur](../master/ScreenShots/BilgisayarArayuz.PNG)  
 Form üzerinde;
 - Bilgisayar kaydı,
 - AD Veritabanında İp ve Bilgisayar Adı bazlı arama,
 - AD veritabanı tüm  cihazları listeleme,
 - Cihazlara uzaktan direktif gönderme,
 - RPC aktif olan cihazların sistem kaynak durumlarının takibi yapılabilmektedir 
 
 ### 4. Raporlama ###
 Her form üzerinde yer alan Excel ve PDF simgeleri raporla sınıfını tetikleyen bileşenlerdir.  
 Formlar üzerinde yer alan datagrid üzerinki verilerin dökümünü hazırlar.  
 
 ## Kod Analizi ##
 Uygulama kaynak kod analizinde Sonarqube aracından faydalanılmıştır. Kod analiz [sonuçları](https://sonarcloud.io/dashboard?id=ADManager) bulut ortamına yüklenmiştir.
 
 

 
 
     
     
     
     
     




  














