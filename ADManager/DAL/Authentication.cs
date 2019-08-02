using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Security.Cryptography;
using System.Net;

namespace ADManager
{
   public class Authentication
    {
        readonly string _userName;
        readonly string _password;
        public Authentication(string _userName, string _password)
        {
            this._userName = _userName;
            this._password = _password;
        }

        public bool IsAuthenticated()
        {
            bool authenticated = false;
            string path = System.Configuration.ConfigurationManager.AppSettings["path"];
            using (DirectoryEntry ldapEntry = new DirectoryEntry(path, _userName, _password, AuthenticationTypes.Secure))
            {
            
                authenticated = true;
                ldapEntry.Close();
            }
            return authenticated;
        }
    }
}
