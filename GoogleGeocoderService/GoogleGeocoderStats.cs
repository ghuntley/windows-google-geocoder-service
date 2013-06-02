using System;

namespace GoogleGeocoderService
{
    public class GoogleGeocoderStats
    {
        public int Ok = 0;
        public int ZeroResults = 0;
        public int OverQueryLimit = 0;
        public int RequestDenied = 0;
        public int InvalidRequest = 0;
        public int UnknownError = 0;
        public int ServiceError = 0;

        public int CalculateTotal()
        {
            return Ok + ZeroResults + OverQueryLimit + RequestDenied + InvalidRequest + UnknownError + ServiceError;
        }

        public override string ToString()
        {
            return String.Format(@"
Ok:                 {0}
Zero Results:       {1}
Over Query Limit:   {2}
Request Denied:     {3}
Invalid Request:    {4}
Unknown Error:      {5}
Service Error:      {6}
Total API Queries:  {7}
", Ok, ZeroResults, OverQueryLimit, RequestDenied, InvalidRequest, UnknownError, ServiceError, CalculateTotal());
        }
    }
}
