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
        private readonly string _dcServer;
        private readonly string _dcEk;
      
        public Computer()
        {
            _domainServer =   System.Configuration.ConfigurationManager.AppSettings["domainServer"];
            _dcServer = System.Configuration.ConfigurationManager.AppSettings["DCDomainServer"];
            _dcEk = System.Configuration.ConfigurationManager.AppSettings["DCDomainEk"];
        }

        #region CihazMethodlar

        public PrincipalContext SetPrincipialContext()
        {
            PrincipalContext principialContext = new PrincipalContext(ContextType.Domain, _domainServer);
            return principialContext;

        }

        public PrincipalContext SetPrincipialContext(string computerName, string yapisalBirim)
        {
            if (string.IsNullOrEmpty(yapisalBirim))
            { return SetPrincipialContext(); }
            else
            {
                PrincipalContext principialContext = new PrincipalContext(ContextType.Domain, _domainServer, $"OU={yapisalBirim},DC={_dcServer},DC={_dcEk}");
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
