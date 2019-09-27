using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace ADManager
{
    class OUClass
    {

        private const string LDAP_PATH = "LDAP://DC=cankirism,dc=local";

        public List<string> GetAllOU()
        {
            List<string> ouList = new List<string>();
            DirectoryEntry startingPoint = new DirectoryEntry(LDAP_PATH);
            DirectorySearcher searcher = new DirectorySearcher(startingPoint);
            searcher.Filter = "(objectCategory=organizationalUnit)";
            foreach (SearchResult ou in searcher.FindAll())
            {
                 if (ou.Properties["ou"][0].ToString().Substring(0,2).ToUpper()=="OU")
                {
                  ouList.Add(ou.Properties["ou"][0].ToString());
                }
               
                
            }

            return ouList;

        }

    }
}
