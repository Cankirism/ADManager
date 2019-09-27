using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ADManager
{
    public class User
    {
        private readonly string _userName;
        private readonly string _userPassword;
        private readonly string _domain;
        public User()
        {
            this._userName = Giris.UserName;
            this._userPassword = Giris.UserPassword;
            _domain = System.Configuration.ConfigurationManager.AppSettings["domainServer"];
        }

        #region Methodlar

        public PrincipalContext BaglantiKur()
        {
            PrincipalContext _principialContext = new PrincipalContext(ContextType.Domain,_domain, _userName, _userPassword);
            return _principialContext;
        }
        public PrincipalContext BaglantiKur(string yapisalBirim)
        {
            if (string.IsNullOrEmpty(yapisalBirim))
            {
                return BaglantiKur();
            }
            else
            {
                PrincipalContext _principialContext = new PrincipalContext(ContextType.Domain, "CANKIRISM", $"OU={yapisalBirim},DC=cankirism,DC=local", _userName, _userPassword);
                return _principialContext;
            }
           

          
        }
        public UserPrincipal SetUserPrincipial(PrincipalContext con)
        {
            UserPrincipal userPrincipial = new UserPrincipal(con);
            return userPrincipial;
        }
        public UserPrincipal SetUserPrincipial(PrincipalContext con, string userName, string Password)
        {
           
            UserPrincipal userPrincipial = new UserPrincipal(con,userName, Password, true);
            return userPrincipial;
        }

        public UserPrincipal SetPrincipialToFind(PrincipalContext principialContext, string samAccountName)
        {  
            return UserPrincipal.FindByIdentity(principialContext, samAccountName);
        }

        public UserPrincipal SetPrincipialToFind(PrincipalContext principialContext, IdentityType Itype, string samAccountName)
        { 
            return UserPrincipal.FindByIdentity(principialContext, samAccountName);
        }

        public PrincipalSearcher SetPrincipialSearcher()
        {
            PrincipalSearcher pS = new PrincipalSearcher();
            return pS;
        }

        public SearchResult SetSearchResult(DirectoryEntry de)
        {
            DirectorySearcher sc = new DirectorySearcher(de);
            SearchResult results = sc.FindOne();
            return results;
        }

        public GroupPrincipal FindAdminGroup(PrincipalContext principialContext, IdentityType Itype, string adminGroupName)
        {
            GroupPrincipal groupPrincipial = GroupPrincipal.FindByIdentity(principialContext, Itype, adminGroupName);
            return groupPrincipial;

        }
        #endregion

    }
    
   
    public class UserProperties
    {
        public string cannonicalName { get; set; }

        public string samAccountName { get; set; }

        public string userAccountControlCode { get; set; }

        public string userAccountControl { get; set; }

        public string lastLogon { get; set; }

        public string whenCreated { get; set; }

        public string pwdLastSet { get; set; }
    }
}

