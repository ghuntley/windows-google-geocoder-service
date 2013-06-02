using System.Net;
using NLog;
using RestSharp;
using ServiceStack.Text;

namespace GoogleGeocoderService
{
    public class GoogleGeocoder
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public bool UseProxyServer { get; private set; }
        public IWebProxy ProxyServer { get; private set; }

        public GoogleGeocoder()
        {
            if (AppConfig.ProxyServerEnabled)
            {
                Log.Info("Using proxy server {0} as configured in Application.Config", AppConfig.ProxyServerUri);

                UseProxyServer = true;
                ProxyServer = new WebProxy(AppConfig.ProxyServerUri);

                if (AppConfig.ProxyServerAuthenticate)
                {
                    Log.Info("Using proxy authentication ({0}/{1} chars) as configured in Application.config", AppConfig.ProxyServerUsername, AppConfig.ProxyServerPassword.Length);
                    ProxyServer.Credentials = new NetworkCredential(AppConfig.ProxyServerUsername, AppConfig.ProxyServerPassword);
                }
            }
        }

        /// <summary>
        /// retrieve details as GoogleGeocodeResponse for address via google geocode api. 
        /// </summary>
        public GoogleGeocodeResponse GeocodeAddress(string address)
        {
            Log.Debug("Address: {0}", address);
            var client = new RestClient("https://maps.googleapis.com");

            if (UseProxyServer) client.Proxy = ProxyServer;

            var request = new RestRequest("/maps/api/geocode/json?sensor=true&address={request}", Method.GET);
            request.AddUrlSegment("request", address);

            request.AddHeader("ApplicationName", AppConfig.ApplicationName);
            request.AddHeader("ApplicationVersion", AppConfig.ApplicationVersion);

            Log.Debug("HTTP Uri: {0}", request.Resource);
            Log.Debug("HTTP Parameters: {0}", request.Parameters.ToJson());

            var json = client.Execute<GoogleGeocodeResponse>(request);
            Log.Trace("Response from Google API as Json: {0}", json.ToJson());

            var serialized = json.Content.FromJson<GoogleGeocodeResponse>();
            Log.Debug("Results from serializing Json response: {0}", serialized.ToJson());

            return serialized;
        }
    }
}
