using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Configuration;


namespace ADManager
{
    public class BusinessComputer
    {
        #region field-Property

        readonly Computer _computer;
        public  string ComputerDomain { get; set; }
        public string ErrorMessage { get; set; }
        public List<ComputersProperties> ComputerList { get; set; }
        
        #endregion

        #region Constructors
        public BusinessComputer()
        {
            _computer = new Computer();
            ComputerDomain = ConfigurationManager.AppSettings["ComputerDomain"];

        }

        #endregion

        #region Methodlar 
        
        /// <summary>
        /// ComputerForm UI'da Sorgula butonu ya da Tük kayıtları getir butonu tıklandığından çağrılan method.
        /// </summary>
        /// <param name="btnName"> Tıklanılan Butonun adını parametre olarak alır. Tıklanılan butona göre farklı liste oluşturur.</param>
        /// <param name="arananPc">Arama alanına girilen değeri parametre olarak alır.Bilgisayar arama yapılırken kullanılır
        ///Girilen text , AD Veritabanında Bilgisayar adı ya da bilgisayar IP'si ile eşleşme kontrolü için. 
        /// </param>
        /// <returns>From üzerindeki Datagrid'in veri kaynağı olan Datatable döner.</returns>
        public DataTable FillGridView(string btnName, string arananPc)
        {
            List<ComputersProperties> secilenPcList = new List<ComputersProperties>();
            var dataTableFill = new DataTableFill();

            if (ComputerList == null)
            {
                ComputerList = GetAllComputers().ToList();
            }

            switch (btnName)
            {
                case "AraBtn":

                    secilenPcList = SearchSingleComputer(EnglishChar.ConvertTRCharToENChar(arananPc.ToUpper()));
                    break;

                case "HepsiniGetirBtn":
                    secilenPcList = ComputerList;
                    break;

            }

            DataTable dataTableComputers = dataTableFill.FillDataTableWithComputers(secilenPcList);
            return dataTableComputers;

        }

        #region BilgisayarListele
        /// <summary>
        /// AD Sunucusundan kayıtlı tüm bilgisayar listesini getirir.
        /// </summary>
        /// <returns> Bilgisayar Listesini döner </returns>
        public IEnumerable<ComputersProperties> GetAllComputers()
        {
            List<ComputersProperties> computerPropertiesList = new List<ComputersProperties>();
            using (var principialContext = _computer.SetPrincipialContext())
            using (var computerPrincipial = _computer.SetComputerPrincipial(principialContext))
            using (var principialSearcher = _computer.SetPrincipialSearcher())
            {
                computerPrincipial.Name = "*";
                principialSearcher.QueryFilter = computerPrincipial;
                PrincipalSearchResult<Principal> _computerSearchResult = principialSearcher.FindAll();
                foreach (var p in _computerSearchResult)
                {
                    ComputerPrincipal pc = (ComputerPrincipal)p;
                    string ipAdress = GetComputerIpAddress(p.Name);
                    var computerPro = new ComputersProperties(pc.Name, pc.Sid.ToString(), ipAdress, pc.LastPasswordSet.ToString(), pc.LastBadPasswordAttempt.ToString());
                    computerPropertiesList.Add(computerPro);
                }
            }

            return computerPropertiesList;
        }
        #endregion

        #region BilgisayarArama
        /// <summary>
        /// Aktif Dizin sunucusu veritabanında bilgisayar araması için kullanılan method.Ip ya da cihaz adına göre arama yapar.
        /// </summary>
        /// <returns>Bulunan Cihazların listesini döner.</returns>
        public List<ComputersProperties> SearchSingleComputer(string arananPc)
        {
            List<ComputersProperties> bulunanCihazlar = new List<ComputersProperties>();
            bool isContain = false;

            for (int a = 0; a < ComputerList.Count; a++)
            {
                var computerPro = ComputerList[a];
                isContain = EnglishChar.ConvertTRCharToENChar( computerPro.computerName.ToUpper()).Contains(arananPc) || computerPro.ipAdress.Contains(arananPc);

                if (isContain)
                {
                    bulunanCihazlar.Add(computerPro);
                }
            }

            return bulunanCihazlar;
        }

        #endregion

        #region BilgisayarKayit

        // Bilgisayar Kayit Methodu

        public string SaveComputer(string computerName,string yapisalBirim)
        {
            try
            {
                using (PrincipalContext principialContext = _computer.SetPrincipialContext(computerName, yapisalBirim))
                using (ComputerPrincipal computerPrincipial = _computer.SetComputerPrincipial(principialContext))
                {
                    computerPrincipial.SamAccountName = computerName;
                    computerPrincipial.Enabled = true;
                    computerPrincipial.Save();
                    return "Başarı ile Kayıt Yapıldı";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region IpAdresCozumle
        public string GetComputerIpAddress(string computerName)
        {

            string ipAdressMessage = string.Empty;
            string computerDomainName = $"{computerName}.{ComputerDomain}";

            try
            {
                if (computerName != null)
                {
                    ipAdressMessage = Dns.GetHostEntry(computerDomainName).AddressList[0].ToString();
                }
            }

            catch (Exception)
            {
                ipAdressMessage = "İp Adresi Çözümlenemedi";
            }

            return ipAdressMessage;

        }

        #endregion

        #endregion

        #region BilgisayarKontrol

        public void ShutDownComputer(string computerName)
        {

            System.Diagnostics.ProcessStartInfo shutDownPs = new System.Diagnostics.ProcessStartInfo("shutdown");
            shutDownPs.Arguments = $"/m \\\\{computerName} /s /t 0 ";
            System.Diagnostics.Process.Start(shutDownPs);


        }

        public void RebootComputer(string computerName)
        {

            System.Diagnostics.ProcessStartInfo rebootPs = new System.Diagnostics.ProcessStartInfo("shutdown");
            rebootPs.Arguments = $"/m \\\\{computerName} /r /t 0";
            System.Diagnostics.Process.Start(rebootPs);
        }

        #endregion

    }
}
