using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace ADManager
{
    static class AbuseService
    {
        private  static string _apiEndpoint =" https://api.abuseipdb.com/api/v2/check";
        private  static string _apiKey      = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];

        public static List<AbuseProperties> AbuseApiConnect(string ipAddress, int day)
        {
            
            List<AbuseProperties> abuseList = new List<AbuseProperties>();
            var client = new RestClient(_apiEndpoint);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Key", _apiKey);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("ipAddress", ipAddress);
            request.AddParameter("maxAgeInDays", day);
            request.AddParameter("verbose", "");

            IRestResponse response = client.Execute(request);

            dynamic parsedJson = JsonConvert.DeserializeObject(response.Content);
            for (int i = 0; i < Convert.ToInt32( parsedJson["data"]["totalReports"]); i++)
            {
                AbuseProperties abuseProperties = new AbuseProperties();
                abuseProperties.IpAddress = parsedJson["data"]["ipAddress"];
                abuseProperties.CountryName = parsedJson["data"]["countryName"];
                abuseProperties.Isp = parsedJson["data"]["isp"];
                abuseProperties.TotalReports = parsedJson["data"]["totalReports"];
                abuseProperties.LastReportedAt = parsedJson["data"]["lastReportedAt"];
                abuseProperties.ReportedAt = parsedJson["data"]["reports"][i]["reportedAt"];
                abuseProperties.Comment = parsedJson["data"]["reports"][i]["comment"];
                abuseProperties.ReporterCountry = parsedJson["data"]["reports"][i]["reporterCountryName"];
                abuseList.Add(abuseProperties);
            }

            return abuseList;  
        }
    }

    class AbuseProperties
    {
        public string IpAddress { get; set; }
        public string CountryName { get; set; }
        public string Isp { get; set; }
        public int TotalReports { get; set; }
        public string LastReportedAt { get; set; }
        public string ReportedAt { get; set; }
        public string Comment { get; set; }
        public string ReporterCountry { get; set; } 

    }
}
