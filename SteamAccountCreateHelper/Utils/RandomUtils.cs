namespace SteamAccountCreateHelper
{
    using Newtonsoft.Json;
    using System;
    using System.Text;

    class RandomUtils
    {
        private static Random random = new Random();
        public static namefake_com LatesNameFakeRequest = new namefake_com();
        public static int GetRandomInt(int minBound, int maxBound)
        {
            return random.Next(minBound, maxBound);
        }

        public static int RandomNumber()
        {
            return random.Next(1234, 314123423);
        }

        public static int RandomNumberSmall()
        {
            return random.Next(1930, 2040);
        }

        public static string RandomLogin()//Using api.namefake.com 
        {
            var request = new RequestBuilder("https://api.namefake.com/").GET()
                       .Execute();
            namefake_com response = JsonConvert.DeserializeObject<namefake_com>(request.Content);
            string name = response.name.ToLower().Replace(".", " ").Replace("'", " ").Replace(" ", "");
            name = $"{name}_{RandomNumberSmall()}";
            LatesNameFakeRequest = response;
            return name;
        }

        // Generate a random string with a given size
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomString(1, false));
            builder.Append(RandomNumber());
            builder.Append(RandomString(2, true));
            builder.Append(RandomString(2, false));
            builder.Append(RandomString(1, true));
            return builder.ToString();
        }

    }


    public class namefake_com
    {
        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string maiden_name { get; set; }
        public string birth_data { get; set; }
        public string phone_h { get; set; }
        public string phone_w { get; set; }
        public string email_u { get; set; }
        public string email_d { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public string useragent { get; set; }
        public string ipv4 { get; set; }
        public string macaddress { get; set; }
        public string plasticcard { get; set; }
        public string cardexpir { get; set; }
        public int bonus { get; set; }
        public string company { get; set; }
        public string color { get; set; }
        public string uuid { get; set; }
        public int height { get; set; }
        public double weight { get; set; }
        public string blood { get; set; }
        public string eye { get; set; }
        public string hair { get; set; }
        public string pict { get; set; }
        public string url { get; set; }
        public string sport { get; set; }
        public string ipv4_url { get; set; }
        public string email_url { get; set; }
        public string domain_url { get; set; }
    }
}