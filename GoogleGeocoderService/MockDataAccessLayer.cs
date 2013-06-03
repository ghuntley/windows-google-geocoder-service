using System.Collections.Generic;
using NLog;

namespace GoogleGeocoderService
{
    public class MockDataAccessLayer : IDataAccessLayer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static List<GoogleGeocoderJob> _sampleData;

        public MockDataAccessLayer()
        {
            _sampleData = new List<GoogleGeocoderJob>();

            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 1, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1"});
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 2, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 3, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 4, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 5, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 6, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 7, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 8, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 9, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 10, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 11, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 12, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 13, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 14, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 15, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 16, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 17, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 18, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 19, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 20, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 21, Address = "565 North Fifth Street San Jose, CA 95112", CompanyName = "Company 1" });
            _sampleData.Add(new GoogleGeocoderJob() { AccountId = 22, Address = "244 Madison Ave. #277, New York , NY , 10016", CompanyName = "Company 2" });

        }

        public IList<GoogleGeocoderJob> GetOutstanding()
        {
            return _sampleData;
        }

        public void SaveResponse(GoogleGeocoderJob job, GoogleGeocodeResponse response)
        {
            Log.Error("{0} has not been saved.", response.results[0].formatted_address);
        }
    }
}
