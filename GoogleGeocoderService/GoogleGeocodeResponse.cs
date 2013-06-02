using System.Device.Location;

namespace GoogleGeocoderService
{
    /// <summary>
    /// Based upon API documentation from 31/May/2013 
    /// https://developers.google.com/maps/documentation/geocoding/
    /// </summary>
    public class GoogleGeocodeResponse
    {
        /// <summary>
        /// "OK" indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
        /// "ZERO_RESULTS" indicates that the geocode was successful but returned no results. This may occur if the geocode was passed a non-existent address or a latlng in a remote location.
        /// "OVER_QUERY_LIMIT" indicates that you are over your quota.
        /// "REQUEST_DENIED" indicates that your request was denied, generally because of lack of a sensor parameter.
        /// "INVALID_REQUEST" generally indicates that the query (address or latlng) is missing.
        /// "UNKNOWN_ERROR" indicates that the request could not be processed due to a server error. The request may succeed if you try again.
        /// </summary>
        public string status { get; set; }
        public GoogleGeocodeResults[] results { get; set; }         
    }

    public class GoogleGeocodeResults
    {
        /// <summary>
        /// A string containing the human-readable address of this location. Often this address is equivalent to the "postal address," which sometimes differs from country to country. (Note that some countries, such as the United Kingdom, do not allow distribution of true postal addresses due to licensing restrictions.) This address is generally composed of one or more address components. For example, the address "111 8th Avenue, New York, NY" contains separate address components for "111" (the street number, "8th Avenue" (the route), "New York" (the city) and "NY" (the US state). These address components contain additional information as noted below.
        /// </summary>
        public string formatted_address { get; set; }
        
        public GoogleGeocodeGeometry geometry { get; set; }

        /// <summary>
        /// A array indicates the type of the returned result. This array contains a set of zero or more tags identifying the type of feature returned in the result. For example, a geocode of "Chicago" returns "locality" which indicates that "Chicago" is a city, and also returns "political" which indicates it is a political entity.
        /// </summary>
        public string[] types { get; set; }

        public GoogleGeocodeAddressComponent[] address_components { get; set; }
    }

    public class GoogleGeocodeGeometry
    {
        /// <summary>
        /// stores additional data about the specified location. The following values are currently supported:
        ///     "ROOFTOP" indicates that the returned result is a precise geocode for which we have location information accurate down to street address precision.
        ///     "RANGE_INTERPOLATED" indicates that the returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections). Interpolated results are generally returned when rooftop geocodes are unavailable for a street address.
        ///     "GEOMETRIC_CENTER" indicates that the returned result is the geometric center of a result such as a polyline (for example, a street) or polygon (region).
        ///     "APPROXIMATE" indicates that the returned result is approximate.
        /// </summary>
        public string location_type { get; set; }

        public GoogleGeocodeLocation location { get; set; }
    }

    public class GoogleGeocodeLocation
    {
        
        public double lat { get; set; }
        public double lng { get; set; }

        /// <summary>
        /// overides handling of .ToString() to return "{lat},{long}"
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0},{1}", lat, lng);
        }

        /// <summary>
        /// converts lat and long into GeoCoordinate
        /// </summary>
        public GeoCoordinate ToGeoCoordinate()
        {
            return new GeoCoordinate(lat, lng);
        }
    }

    public class GoogleGeocodeAddressComponent
    {
        /// <summary>
        /// The full text description or name of the address component as returned by the Geocoder.
        /// </summary>
        public string long_name { get; set; }

        /// <summary>
        /// An abbreviated textual name for the address component, if available. For example, an address component for the state of Alaska may have a long_name of "Alaska" and a short_name of "AK" using the 2-letter postal abbreviation.
        /// </summary>
        public string short_name { get; set; }

        /// <summary>
        /// An array that indicates the type of the returned result. This array contains a set of zero or more tags identifying the type of feature returned in the result. For example, a geocode of "Chicago" returns "locality" which indicates that "Chicago" is a city, and also returns "political" which indicates it is a political entity.
        /// </summary>
        public string[] types { get; set; }
    }
}
