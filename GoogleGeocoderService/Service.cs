using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;

using NLog;

using ServiceStack.Text;

namespace GoogleGeocoderService
{
    public partial class Service : ServiceBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Service()
        {
            ApplicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ApplicationName = AppConfig.ApplicationName;
            ApplicationVersion = AppConfig.ApplicationVersion;
            DataAccessLayer = new MockDataAccessLayer();
            Geocoder = new GoogleGeocoder();
            GeocoderStats = new GoogleGeocoderStats();
            GeocoderQueue = new Dictionary<int, string>();
            IsGeocoding = false;
            Random = new Random();

            // Execute the stuff from designer.cs
            InitializeComponent();

            // Allow user to override the Interval specified in designer.cs
            MainTimer.Interval = AppConfig.Interval * 1000 * 60;

            Log.Info("Main event loop will run every {0} minutes.", AppConfig.Interval);
        }

        public string ApplicationDirectory
        {
            get; private set;
        }

        public string ApplicationName
        {
            get; private set;
        }

        public string ApplicationVersion
        {
            get; private set;
        }

        public IDataAccessLayer DataAccessLayer
        {
            get; private set;
        }

        public GoogleGeocoder Geocoder
        {
            get; private set;
        }

        public Dictionary<int, string> GeocoderQueue
        {
            get; private set;
        }

        public GoogleGeocoderStats GeocoderStats
        {
            get; private set;
        }

        public bool IsGeocoding
        {
            get; private set;
        }

        public Random Random
        {
            get; private set;
        }

        [Conditional("DEBUG")]
        public void DebugBreak()
        {
            Debugger.Break();
        }

        [Conditional("DEBUG")]
        public void DebugLaunch()
        {
            Debugger.Launch();
        }

        public void InteractiveStart(string [] args)
        {
            OnStart(args);
        }

        public void InteractiveStop()
        {
            OnStop();
        }

        public void ResetGeocoderStats()
        {
            GeocoderStats = new GoogleGeocoderStats();
        }

        protected override void OnStart(string[] args)
        {
            MainTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            MainTimer.Enabled = false;
        }

        private void MainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                if (IsGeocoding)
                {
                    Log.Warn("An existing geocoding session was found, will try again in {0} minutes.",
                             AppConfig.Interval);
                }
                else
                {
                    Log.Info("Started geocoding session using {0} threads with {1} min / {2} max seconds of jitter.",
                             AppConfig.Parallelism, AppConfig.JitterMinSleep, AppConfig.JitterMaxSleep);
                    IsGeocoding = true;

                    GeocoderQueue = DataAccessLayer.GetOutstanding();

                    GeocoderQueue.AsParallel().WithDegreeOfParallelism(AppConfig.Parallelism).ForAll(dictionary =>
                        {
                            var primarykey = dictionary.Key;
                            var address = dictionary.Value;

                            // add a element of jitter to the parrallelism to prevent
                            // a thundering herd of requests.
                            var jitter = Random.Next(AppConfig.JitterMinSleep, AppConfig.JitterMaxSleep);

                            Log.Debug("Sleeping for {0} seconds, afterwards will retrieve {1}.", jitter, address);
                            System.Threading.Thread.Sleep(jitter * 1000);

                            var response = Geocoder.GeocodeAddress(address);

                            if (response.Equals(null))
                            {
                                Log.Error("SERVICE_ERROR: {0} response was null.", address);
                                GeocoderStats.ServiceError++;
                            }
                            else
                            {
                                switch (response.status.ToUpperInvariant())
                                {
                                    case "OK":
                                        GeocoderStats.Ok++;
                                        Log.Info("OK: {0}", address);
                                        DataAccessLayer.SaveResponse(primarykey, response);
                                        break;
                                    case "ZERO_RESULTS":
                                        Log.Warn("ZERO_RESULTS: {0}", address);
                                        GeocoderStats.ZeroResults++;
                                        break;
                                    case "OVER_QUERY_LIMIT":
                                        Log.Fatal("OVER_QUERY_LIMIT: {0}", address);
                                        GeocoderStats.OverQueryLimit++;
                                        break;
                                    case "REQUEST_DENIED":
                                        Log.Fatal("REQUEST_DENIED: {0}", address);
                                        GeocoderStats.RequestDenied++;
                                        break;
                                    case "INVALID_REQUEST":
                                        Log.Fatal("INVALID_REQUEST: {0}", address);
                                        GeocoderStats.InvalidRequest++;
                                        break;
                                    case "UNKNOWN_ERROR":
                                        Log.Error("UNKNOWN_ERROR: {0}", address);
                                        GeocoderStats.UnknownError++;
                                        break;

                                    default:
                                        // This would only happen if Google add new STATUS codes to their
                                        // api or a typo exists in the above switch statement.
                                        Log.Fatal("SERVICE_ERROR: {0}", address);
                                        GeocoderStats.ServiceError++;
                                        break;
                                }
                            }

                        });

                    Log.Info("Finished Geocoding Session: \n\n {0} \n {1} \n", GeocoderQueue.ToJson(), GeocoderStats);

                    ResetGeocoderStats();

                    IsGeocoding = false;
                }
            }
            catch (Exception ex)
            {
                Log.FatalException("Abort, Abort.. Something went seriously wrong: ", ex);
            }
        }
    }
}