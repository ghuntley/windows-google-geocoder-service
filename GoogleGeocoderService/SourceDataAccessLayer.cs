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

                var record = context.DEBTORS.Single(s => s.AccountID.Equals(job.AccountId));
                    //(from debtors in context.DEBTORS
                    //          where debtors.AccountID == job.AccountId
                    //          select debtors).FirstOrDefault();

                if (record == null)
                {
                    Log.Error("Unable to update AccountID: {0} ({1}) with Lat: {2} / Lng: {3}", job.AccountId,
                              job.CompanyName, latitude, longitude);
                    return;
                }

                //context.DEBTORS.InsertOnSubmit();
                record.Latitude = latitude;
                record.Longitude = longitude;
                context.SubmitChanges();
            }

            //Log.Error("{0} has not been saved.", response.results[0].formatted_address);
        }
    }
}
