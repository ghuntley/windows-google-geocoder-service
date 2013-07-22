using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using NLog;

using RestSharp;

using ServiceStack.Text;

namespace GoogleGeocoderService
{
    public class GoogleGeocoder
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public GoogleGeocoder()
        {
            if (AppConfig.ProxyServerEnabled)
            {
                Log.Info("Using proxy server {0} as configured in Application.Config", AppConfig.ProxyServerUri);

                UseProxyServer = true;
                ProxyServer = new WebProxy(AppConfig.ProxyServerUri);

                if (AppConfig.ProxyServerAuthenticate)
                {
                    Log.Info("Using proxy authentication ({0}/{1} chars) as configured in Application.config",
                             AppConfig.ProxyServerUsername, AppConfig.ProxyServerPassword.Length);
                    ProxyServer.Credentials = new NetworkCredential(AppConfig.ProxyServerUsername,
                                                                    AppConfig.ProxyServerPassword);
                }
            }
        }

        public WebProxy ProxyServer
        {
            get; private set;
        }

        public bool UseProxyServer
        {
            get; private set;
        }

        /// <summary>
        /// retrieve details as GoogleGeocodeResponse for address via google geocode api. 
        /// </summary>
        public GoogleGeocodeResponse GeocodeAddress(string address)
        {
            Log.Debug("Address: {0}", address);

            WebClient client = new WebClient();
            //if (UseProxyServer) client.Proxy = ProxyServer;

            // Recommended reading -> https://developers.google.com/maps/documentation/business/webservices#digital_signatures
            var unsigned = String.Format("https://maps.googleapis.com/maps/api/geocode/json?sensor=true&address={0}&client={1}",
                                        address,
                                        AppConfig.GoogleApiClientId);
            Log.Debug("HTTP Unsigned Uri: {0}", unsigned);

            var signed = SignUrl(unsigned, AppConfig.GoogleApiCryptoKey);
            Log.Debug("HTTP Signed Uri: {0}", signed);

            client.Headers.Add("ApplicationName", AppConfig.ApplicationName);
            client.Headers.Add("ApplicationVersion", AppConfig.ApplicationVersion);

            var response  = client.OpenRead(signed);
            var stream = new StreamReader(response);

            var results = stream.ReadToEnd();

            Log.Trace("Response from Google API as Json: {0}", results.ToJson());

            var serialized = results.FromJson<GoogleGeocodeResponse>();
            Log.Debug("Results from serializing Json response: {0}", serialized.ToJson());

            return serialized;
        }

        /// <summary>
        /// Literally copied and pasted from Google SDK (https://developers.google.com/maps/documentation/business/webservices#CSharpSignatureExample)
        /// </summary>
        public string SignUrl(string url, string keyString)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    }
}