using System.Collections.Generic;
using NLog;

namespace GoogleGeocoderService
{
    public class MockDataAccessLayer : IDataAccessLayer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static Dictionary<int, string> _sampleData;

        public MockDataAccessLayer()
        {
            _sampleData = new Dictionary<int, string>();

            _sampleData.Add(1, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(2, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(3, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(4, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(5, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(6, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(7, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(8, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(9, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(10, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(11, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(12, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(13, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(14, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(15, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(16, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(17, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(18, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(19, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(20, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(21, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(22, "244 Madison Ave. #277, New York , NY , 10016");
            _sampleData.Add(23, "565 North Fifth Street San Jose, CA 95112");
            _sampleData.Add(24, "244 Madison Ave. #277, New York , NY , 10016");

        }

        public Dictionary<int, string> GetOutstanding()
        {
            return _sampleData;
        }

        public void SaveResponse(int primarykey, GoogleGeocodeResponse response)
        {
            Log.Error("{0} has not been saved.", response.results[0].formatted_address);
        }
    }
}
