using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADManager
{
    class DataTableFill
    {

        private DataTable _dataTable;
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Fill DataGridView on  User Form. 
        /// Kullanıcı Formlarındaki datagridviewleri bu class yordamıyla dolduruyoruz.
        /// </summary>
        /// <param name="usersDataList"></param>
        /// <returns> Domain Users  as List </returns>
        public DataTable FillDataTable(IEnumerable<UsersProperties> usersDataList)
        {
            try
            {
                List<UsersProperties> userData = usersDataList.ToList();
                _dataTable = new DataTable();
                _dataTable.Columns.Add("Kayıt No", typeof(int));
                _dataTable.Columns.Add("İsim", typeof(string));
                _dataTable.Columns.Add("Kullanıcı Adı", typeof(string));
                _dataTable.Columns.Add("Kullanıcı Aktif Kodu", typeof(string));
                _dataTable.Columns.Add("Kullanıcı Durumu", typeof(string));
                _dataTable.Columns.Add("Son Bağlantı", typeof(string));
                _dataTable.Columns.Add("Katılma Tarihi", typeof(string));
                _dataTable.Columns.Add("Parola Değiştirme", typeof(string));

                for (int i = 0, j = 1; i < userData.Count && j <= userData.Count; i++, j++)
                {
                    _dataTable.NewRow();
                    UsersProperties users = userData[i];
                    _dataTable.Rows.Add(j, users.cannonicalName, users.samAccountName, users.userAccountControlCode, users.userAccountControl, users.lastLogon, users.whenCreated, users.pwdLastSet);
                }
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                _dataTable = null;
            }

            return _dataTable;
        }

        /// <summary>
        /// Fill DataGridView on  Computer Form. 
        /// Bilgisayar Formlarındaki datagridviewleri bu class yordamıyla dolduruyoruz.
        /// </summary>
        /// <param name="computerList"></param>
        /// <returns> Domain Computers  as List </returns>
        public DataTable FillDataTableWithComputers(List<ComputersProperties> computerList)
        {
            List<ComputersProperties> _computerList = computerList;
            DataTable computerDt = new DataTable();
            computerDt.Columns.Add("Kayıt No", typeof(int));
            computerDt.Columns.Add("Pc Adı", typeof(string));
            computerDt.Columns.Add("Sid", typeof(string));
            computerDt.Columns.Add("İp Adresi", typeof(string));
            computerDt.Columns.Add("Son Parola Oluşturma", typeof(string));
            computerDt.Columns.Add("Son Başarısız Parola Giriş", typeof(string));

            for (int i = 0, j = 1; i < _computerList.Count && j <= _computerList.Count; i++, j++)
            {
                computerDt.NewRow();
                ComputersProperties computerPro = _computerList[i];
                computerDt.Rows.Add(j, computerPro.computerName, computerPro.computerSID, computerPro.ipAdress, computerPro.lastPassSet, computerPro.lastBadAttempt);
            }

            return computerDt;
        }
    }
}

