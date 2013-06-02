using System.Collections.Generic;

namespace GoogleGeocoderService
{
    public interface IDataAccessLayer
    {
        /// <summary>
        /// retrieves list of database records which require geoencoding.
        /// </summary>
        Dictionary<int, string> GetOutstanding();

        /// <summary>
        /// updates database record with response from identified by (dictionary key) with data from response.
        /// </summary>
        void SaveResponse(int key, GoogleGeocodeResponse response);
    }
}