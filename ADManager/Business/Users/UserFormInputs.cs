using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADManager
{
    public class UserFormInputs
    {

        public string userName { get; set; }
        public string userPass { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public bool isUserEnable { get; set; }

        public UserFormInputs(string name, string surname, string userName, string userPass, bool isUserEnable)
        {
            this.userName = userName;
            this.userPass = userPass;
            this.name = name;
            this.surname = surname;
            this.isUserEnable = isUserEnable;

        }
    }
}
