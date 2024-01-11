using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace GameServices
{
    public static class IPLocation
    {
        public static async Task<string> FetchIpCountryCode()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.ipgeolocation.io/ipgeo?apiKey=583c8760e27b46589188be792a1e44a7");
            var response = await httpClient.SendAsync(request);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            Root data = JsonUtility.FromJson<Root>(result);
            return data.country_code2;
        }

        [Serializable]
        public class Currency
        {
            public string code;
            public string name;
            public string symbol;
        }

        [Serializable]
        public class Root
        {
            public string ip;
            public string continent_code;
            public string continent_name;
            public string country_code2;
            public string country_code3;
            public string country_name;
            public string country_name_official;
            public string country_capital;
            public string state_prov;
            public string state_code;
            public string district;
            public string city;
            public string zipcode;
            public string latitude;
            public string longitude;
            public bool is_eu;
            public string calling_code;
            public string country_tld;
            public string languages;
            public string country_flag;
            public string geoname_id;
            public string isp;
            public string connection_type;
            public string organization;
            public Currency currency;
            public TimeZone time_zone;
        }

        [Serializable]
        public class TimeZone
        {
            public string name;
            public int offset;
            public int offset_with_dst;
            public string current_time;
            public double current_time_unix;
            public bool is_dst;
            public int dst_savings;
        }


    }
}