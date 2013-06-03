using System;
using System.Configuration;
using System.Reflection;

namespace GoogleGeocoderService
{
    public static class AppConfig
    {
        public static string ApplicationName = Assembly.GetCallingAssembly().GetName().Name;
        public static string ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        public static int Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);
        public static int JitterMaxSleep = Convert.ToInt32(ConfigurationManager.AppSettings["JitterMaxSleep"]);
        public static int JitterMinSleep = Convert.ToInt32(ConfigurationManager.AppSettings["JitterMinSleep"]);
        public static int Parallelism = Convert.ToInt32(ConfigurationManager.AppSettings["Parallelism"]);
        public static bool ProxyServerAuthenticate = Convert.ToBoolean(ConfigurationManager.AppSettings["ProxyServerAuthenticate"]);
        public static bool ProxyServerEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["ProxyServerEnabled"]);
        public static string ProxyServerPassword = ConfigurationManager.AppSettings["ProxyServerPassword"];
        public static Uri ProxyServerUri = new Uri(ConfigurationManager.AppSettings["ProxyServerUri"]);
        public static string ProxyServerUsername = ConfigurationManager.AppSettings["ProxyServerUsername"];
    }
}