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
                            where (debtors.Longitude.Equals(null) || debtors.Latitude.Equals(null))

                            select new GoogleGeocoderJob()
                            {
                                AccountId = debtors.AccountID,
                                Address = ((debtors.Address1 ?? "") + " " + (debtors.Address2 ?? "") + " " + (debtors.Address3 ?? "") + " " + (debtors.Address4 ?? "") + " " + (debtors.City ?? "") + " " + (debtors.State ?? "") + " " + (debtors.PostCode ?? "")).Trim(),
                                CompanyName = debtors.Company
                            };

                return query.ToList();
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
            Log.Error("{0} has not been saved.", response.results[0].formatted_address);
        }
    }
}
