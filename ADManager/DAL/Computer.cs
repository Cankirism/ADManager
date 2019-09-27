using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Management;

namespace ADManager
{
   public class Computer
    {
        private readonly string _domainServer;
        private ComputerPrincipal _computerPrincipial;
        public Computer()
        {
            _domainServer =   System.Configuration.ConfigurationManager.AppSettings["domainServer"]; 
        }

        #region CihazMethodlar

        public PrincipalContext SetPrincipialContext()
        {
            PrincipalContext principialContext = new PrincipalContext(ContextType.Domain, _domainServer);
            return principialContext;

        }
        public PrincipalContext SetPrincipialContext(string yapisalBirim)
        {
            if (string.IsNullOrEmpty(yapisalBirim)) { return SetPrincipialContext(); }

            else
            {
                PrincipalContext principialContext = new PrincipalContext(ContextType.Domain, "CANKIRISM", $"OU={yapisalBirim},DC=cankirism,DC=local");
                return principialContext;
            } 

        }

        public ComputerPrincipal SetComputerPrincipial(PrincipalContext principialContext)
        {
           
            _computerPrincipial = new ComputerPrincipal(principialContext);
            return _computerPrincipial;
        }

        public PrincipalSearcher SetPrincipialSearcher()
        {
            PrincipalSearcher principialSearcher = new PrincipalSearcher(_computerPrincipial);
            return principialSearcher;

        }

        #endregion
    }
    public class ComputersProperties
    {

        public string computerName { get; set; }
        public string computerSID { get; set; }
        public string ipAdress { get; set; }
        public string lastPassSet { get; set; }
        public string lastBadAttempt { get; set; }

        public ComputersProperties(string computerName, string computerSID, string ipAdress, string lastPassSet, string lastBadAttempt)
        {
            this.computerName = computerName;
            this.computerSID = computerSID;
            this.ipAdress = ipAdress;
            this.lastPassSet = lastPassSet;
            this.lastBadAttempt = lastBadAttempt;

        }
    }
}
