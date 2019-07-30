using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ADManager
{
    class ComputerInfo
    {
        private readonly string _ipAddress;
        public bool _errState;
        public string InfoErr { get; set; }
       
        public ComputerInfo(string _ipAddress)
        {
            this._ipAddress = _ipAddress;

        }

        public List<string> GetCpuInfo()
        {
            string computerDomain = System.Configuration.ConfigurationManager.AppSettings["computerDomain"];
            ConnectionOptions oConn = new ConnectionOptions();
            oConn.Username =computerDomain+"\\"+Giris.UserName;
            oConn.Password = Giris.UserPassword;
            List<string> cpuList = new List<string>();

            try
            {
                var oScope = new ManagementScope($"\\\\{_ipAddress}\\root\\CIMV2", oConn);
                oScope.Options.EnablePrivileges = true;
                oScope.Connect();
                var query = new ObjectQuery(" Select * from Win32_Processor");
                var  ObjSearcher = new ManagementObjectSearcher(oScope, query);

                foreach (var obj in ObjSearcher.Get())
                {
                    cpuList.Add(obj["Name"].ToString());

                }
            }
           
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                _errState = true;
                InfoErr = comEx.Message;
            }

            catch (Exception err)

            {
                _errState = true;
                InfoErr = err.Message;
            }

            return cpuList;
        }
    }
}
