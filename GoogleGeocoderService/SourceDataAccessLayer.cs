using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SourceDb;

namespace GoogleGeocoderService
{
    public class SourceDataAccessLayer : IDataAccessLayer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private SourceDbDataContext _sourceDbDataContext; 
 
        /// <summary>
        /// retrieves list of database records which require geoencoding.
        /// </summary>
        public IList<GoogleGeocoderJob> GetOutstanding()
        {
            try
            {
                _sourceDbDataContext = new SourceDbDataContext(AppConfig.ConnectionString);

                var query = from debtors in _sourceDbDataContext.DEBTORS
                            where (debtors.Longitude.Equals(null) || debtors.Latitude.Equals(null) || debtors.Latitude == 0 || debtors.Longitude == 0)

                            select new GoogleGeocoderJob()
                            {
                                AccountId = debtors.AccountID,
                                Address = ((debtors.Address1 ?? "") + " " + (debtors.Address2 ?? "") + " " + (debtors.Address3 ?? "") + " " + (debtors.Address4 ?? "") + " " + (debtors.City ?? "") + " " + (debtors.State ?? "") + " " + (debtors.PostCode ?? "")).Trim(),
                                CompanyName = debtors.Company
                            };

                var filtered =
                    query.Where(
                        job => !job.Address.ToUpper().Contains("PO BOX")).Where(
                        job => !job.Address.ToUpper().Contains("LOCKED BAG"));

                return filtered.ToList();
            }
            catch (Exception ex)
            {
                Log.Fatal("SQL Error: {0}", ex.Message);
                return new List<GoogleGeocoderJob>();
            }
        }

        /// <summary>
        /// updates database record with response from identified by (dictionary key) with data from response.
        /// </summary>
        public void SaveResponse(GoogleGeocoderJob job, GoogleGeocodeResponse response)
        {
            using (var context = new SourceDbDataContext(AppConfig.ConnectionString))
            {
                var latitude = (decimal?) response.results[0].geometry.location.lat;
                var longitude = (decimal?) response.results[0].geometry.location.lng;

                var accountid = job.AccountId;

                try
                {
                    context.SpWindowsGeocoderService_SetAccountIdLatLong(accountid, latitude, longitude);
                }
                catch (Exception ex)
                {
                    Log.Error("Unable to update AccountID: {0} ({1}) with Lat: {2} / Lng: {3}", job.AccountId,
                              job.CompanyName, latitude, longitude); 
                   
                    Log.Error("Exception: {0}", ex.Message);
                }
            }
        }
    }
}
