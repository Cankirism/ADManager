using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace ADManager
{
    public class ListtoDtConverter

    {
        // Tanımlanan list'lerin datatable convert işleminin yapıldığı sınıf.
        public DataTable ConverToDataTable<T>(List<T> items)

        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Propertilerin yüklenme kodu.

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)

            {

                //propertyler datatable sütun adı olarak alınıyor

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)

            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {

                    //Satırlar, property değerleri olarak set ediliyor.

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

          

            return dataTable;

        }

    }
}
