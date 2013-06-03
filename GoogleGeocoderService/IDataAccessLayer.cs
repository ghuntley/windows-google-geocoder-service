using System.Collections.Generic;

namespace GoogleGeocoderService
{
    public interface IDataAccessLayer
    {
        /// <summary>
        /// retrieves list of database records which require geoencoding.
        /// </summary>
        IList<GoogleGeocoderJob> GetOutstanding();

        /// <summary>
        /// updates database record with response from identified by (dictionary key) with data from response.
        /// </summary>
        void SaveResponse(GoogleGeocoderJob job, GoogleGeocodeResponse response);
    }
}