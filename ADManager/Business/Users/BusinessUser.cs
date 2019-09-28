using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;




namespace ADManager
{
   public class BusinessUser
    {
        private static bool _stateForTest { get; set; }
        private readonly User user;
        private readonly Dictionary<int, string> _durumKodlari;
        public static bool testState
        {
        get
            {
                return _stateForTest;
            }
        
        }
        public string ResponseMessage { get; set; }

        public BusinessUser()
        {
            user = new User();
            _durumKodlari = new Dictionary<int, string>();
            CreateUserCodeList();
           
        }

        #region Methodlar
        /// <summary>
        /// Active Directory sunucusundan tüm kullanıcıları getirir.
        /// </summary>
        /// <returns>Tüm kullanıcıları tablo olarak döner </returns>
      public DataTable GetAllAdUsers()
        {

            List<UsersProperties> usersPropertiesList = new List<UsersProperties>();
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (UserPrincipal userPrincipial = user.SetUserPrincipial(principialCon))
            {
                userPrincipial.Name = "*";
                using (PrincipalSearcher principialSearcher = user.SetPrincipialSearcher())
                {
                    principialSearcher.QueryFilter = userPrincipial;

                    foreach (var result in principialSearcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        usersPropertiesList.Add(GetUserProperties(de));

                    }
                }
            }

            return   DataTableAktar(usersPropertiesList);
        }

        /// <summary>
        /// Active Directory veritabanında bulunan kullanıcı bilgilerini, UserProperties sınıfı  propertilerine set eden method.
        /// </summary>
        /// <param name="de">DirectoryEntry</param>
        /// <returns></returns>
        private UsersProperties GetUserProperties(DirectoryEntry de)
        {
            SearchResult searchResult = user.SetSearchResult(de);
            var usersProperties = new UsersProperties();
            usersProperties.cannonicalName = de.Properties["cn"].Value.ToString();
            usersProperties.samAccountName = de.Properties["samaccountname"][0].ToString();
            usersProperties.userAccountControlCode = de.Properties["useraccountcontrol"][0].ToString();
            usersProperties.userAccountControl = UserAccountControl(Convert.ToInt32(de.Properties["useraccountcontrol"][0]));
            usersProperties.whenCreated = Convert.ToDateTime(de.Properties["whenCreated"].Value).ToLocalTime().ToString();
            usersProperties.pwdLastSet = DateTime.FromFileTime((long)searchResult.Properties["pwdLastSet"][0]).ToShortDateString();
            usersProperties.lastLogon = DateTime.FromFileTime((long)searchResult.Properties["lastLogon"][0]).ToLocalTime().ToString();
            return usersProperties;

        }

        public  DataTable DataTableAktar(IEnumerable<UsersProperties> userList)
        {
            
            DataTableFill fillDataTable = new DataTableFill();
            return fillDataTable.FillDataTable(userList);
        }

      
        /// <summary>
        /// Actif Dizin veritabanında kullanıcı arama yapmak için kullanılır.
        /// </summary>
        /// <param name="arananKisi">UI'da girilen , aranan kullanıcının adı </param>
        /// <returns>Kullanıcı listesini döner.</returns>
        public List<UsersProperties> SearchUser(string arananKisi)
        {
            List<UsersProperties> userList = new List<UsersProperties>();
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (UserPrincipal userPrincipial = user.SetUserPrincipial(principialCon))
            {
                userPrincipial.Name = $"{arananKisi}*";
                using (PrincipalSearcher principialSearcher = new PrincipalSearcher(userPrincipial))
                {
                    foreach (var ps in principialSearcher.FindAll().Where( x =>(EnglishChar.ConvertTRCharToENChar(x.SamAccountName.ToUpper())).Contains(arananKisi)))
                    {
                        DirectoryEntry de = (DirectoryEntry)ps.GetUnderlyingObject();
                        userList.Add(GetUserProperties(de));
                    }
                }

            }

            return userList;
        }

        public DataTable SetSearchedPersonDT(IEnumerable<UsersProperties> userList)
        {
            IEnumerable<UsersProperties> _userList = userList;
            return DataTableAktar(_userList);

        }
      
        /// <summary>
        /// Aktif Dizin Veritabanına yeni kayıt ekleme methodu.
        /// Kullanıcı Kayıt UI formunda girilen parametreleri UserFormInputs sınıfı vasıtasıyla alır ve veritabanıına kaydeder.
        /// </summary>
        /// <param name="userFormInputs"></param>
        /// <returns></returns>
        public void CreateUserAccount(UserFormInputs userFormInputs)
        {
            try
            {
                using (PrincipalContext principialCon = user.BaglantiKur(userFormInputs.YapisalBirim))
                using (UserPrincipal userPrincipial = user.SetUserPrincipial(principialCon, userFormInputs.userName, userFormInputs.userPass))
                {
                    userPrincipial.SamAccountName = userFormInputs.userName;
                    userPrincipial.Name = string.Format("{0} {1}", userFormInputs.name, userFormInputs.surname);
                    userPrincipial.Surname = userFormInputs.surname;
                    userPrincipial.DisplayName = string.Format("{0} {1}", userFormInputs.name, userFormInputs.surname);
                    userPrincipial.Enabled = true;
                   
                    userPrincipial.Save();
                    userPrincipial.ExpirePasswordNow(); //Kullanıcı ilk oturum açılısında parolasını değiştirsin .
                    _stateForTest = true;
                    ResponseMessage = "Kullanıcı kaydı başarılı";
                }
            }
            catch (DirectoryServicesCOMException ex)
            {
                _stateForTest = false;
                ResponseMessage = ex.Message;

            }
            catch (Exception ex )
            {
                _stateForTest = false;
                ResponseMessage = ex.Message;
               
            }
        }
      
        //Kullanıcı Silme.
        public void DeleteUser(string samAccountName)
        {
            try
            {
                using (PrincipalContext principialCon = user.BaglantiKur())
                using (UserPrincipal userPrincipial = user.SetPrincipialToFind(principialCon, samAccountName))
                {
                    userPrincipial.Delete();
                    _stateForTest = true;
                }
            }
            catch (Exception)
            {
                _stateForTest = false;
            }  
        }

        #region KullaniciPasif

        /// <summary>
        /// Kullanıcı hesabını Aktif Dizin veritabanında Pasife almaya yarayan method.
        /// </summary>
        /// <param name="samAccountName">Kullanıcı Adı </param>
        /// <returns>İşlem gerçekleşme durumunu döner</returns>
        public string DisableUser(string samAccountName)
        {
            string _samAccountName = samAccountName;
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (UserPrincipal userPrincipial = user.SetPrincipialToFind(principialCon, IdentityType.SamAccountName, _samAccountName))
            {
                if (userPrincipial.Enabled == true)
                {
                    userPrincipial.Enabled = false;
                    userPrincipial.Save();
                    ResponseMessage = "Kullanıcı başarıyla Pasif edildi";
                }

                else
                {
                    ResponseMessage = "Zaten Pasif";
                }
            }

            return ResponseMessage;
        }

        #endregion

        #region AdminGrubaEkleme

        public string AddUserToAdminGroup(string samAccountName, string adminGroupName)
        {
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (GroupPrincipal adminGroup = user.FindAdminGroup(principialCon, IdentityType.Name, adminGroupName))
            {
                adminGroup.Members.Add(principialCon, IdentityType.SamAccountName, samAccountName);
                adminGroup.Save();
                ResponseMessage = "İşlem Başarılı";
            }
            return ResponseMessage;
        }

        #endregion

        #region AdminListele
        public List<string> ListAdmins(string adminGroupName)
        {
            List<string> adminUserNameList = new List<string>();
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (GroupPrincipal adminGroup = user.FindAdminGroup(principialCon, IdentityType.Name, adminGroupName))
            {
                PrincipalSearchResult<Principal> adminList = adminGroup.GetMembers();
                foreach (var pr in adminList)
                {
                    adminUserNameList.Add(pr.SamAccountName);
                }
            }
            return adminUserNameList;
        }

        #endregion

        /// <summary>
        ///  Kullanıcının Üye Olduğu Grupları Listeler.
        /// </summary>
        /// <param name="samAccountName">Kullanıcı Adı</param>
        /// <returns>Grup Listesini döner</returns>
       
        public IEnumerable<string> GetUserGroups(string samAccountName)
        {
            List<string> groupList = new List<string>();
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (UserPrincipal userPrincipial = user.SetPrincipialToFind(principialCon, IdentityType.SamAccountName, samAccountName))
            {
                var groups = userPrincipial.GetAuthorizationGroups();

                foreach (Principal gp in groups)
                {
                    groupList.Add(gp.ToString());
                }
            }
            return groupList;
        }

        /// <summary>
        /// Kullanıcı Parola SIfırlama işlemini yapan method.
        /// </summary>
        /// <param name="samAccountName">Kullanıcı Adı</param>
        /// <param name="password"> Yeni Parolası </param>
        /// <returns>Durum Bilgisini Döner (Başarılı ya da Hatalı)</returns>
        public string ResetUserPassword(string samAccountName, string password)
        {
            using (PrincipalContext principialCon = user.BaglantiKur())
            using (UserPrincipal userPrincipial = user.SetPrincipialToFind(principialCon, samAccountName))
            {
                if (samAccountName != null)
                {
                    userPrincipial.SetPassword(password);
                    ResponseMessage = "Kullanıcı Parolası Değiştirildi";
                }
            }
            return ResponseMessage;
        }

        // AD veritabanından bulunan Kullanıcı Durum kodlarının kontrolünü sağlayan method.
        private string UserAccountControl(int durumKodu)
        {
            string userCode = string.Empty;
            foreach (var a in _durumKodlari)
            {
                if (a.Key == durumKodu)
                {
                    userCode = a.Value;
                    break;
                }

                else
                {
                    userCode = "Bilinmiyor";

                }
            }

            return userCode;

        }

        // Kullanıcı aktif durum kod listesi Dic type olarak oluşturur.
        private void CreateUserCodeList()
        {

            // AD Sunucusundan kullanıcı eklendiğinde 
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
        }
    }
    #endregion
}
