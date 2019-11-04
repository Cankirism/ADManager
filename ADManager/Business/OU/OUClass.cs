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

        private  readonly string _ldapOu= System.Configuration.ConfigurationManager.AppSettings["LDAPOU"];
        public List<string> GetAllOU()
        {
            List<string> ouList = new List<string>();
            DirectoryEntry startingPoint = new DirectoryEntry(_ldapOu);
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
