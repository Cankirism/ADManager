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
uygulama  test ve gerçek ortamında ;
- Windows 7 ,
- Windows 8 ,
- Windows 10 istemci işletim sistemi ailesinde sorunsuz çalışmıştır.  
   Sunucu tarafında ise ; 
 - Windows Server 2012 R2 Standart
 - Windows Server 2016 R2 Standart AD sunucuları ile sorunsuz çalışmıştır
 
## Gereksinimler ##
- Windows istemci cihaz  (Sunucu - client işletim sistemi ailesi)
- .Net Framework 4.5 + 

## Kurulum ## 
Kurulum dosyasını [buradan](Kurulum) indirilerek kurulum başlatılabilir.

## Ön Düzenleme ## 
Kurulum yaptıktan sonra yapılandırma dosyasında değişiklikler yapmamız gerekecektir.  
> Uygulama Kurulduğu dizin\Admanager.exe.config   
  
Bazı parametreleri (Ldap adres, domain sunucu adı ve domain adı) config dosyasındabelirtmemiz gerekir.
   >
  <appSettings>
    <add key="path" value=""/>
    <add key="domainServer" value=""/>
    <add key="computerDomain" value=""/>
  </appSettings>*









