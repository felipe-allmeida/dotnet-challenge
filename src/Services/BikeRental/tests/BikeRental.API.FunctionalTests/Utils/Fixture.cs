using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.API.FunctionalTests.Utils
{
    public class Fixture
    {
        public Dictionary<string, object> Data { get; set; }

        public Fixture()
        {
            Data = new Dictionary<string, object>();
        }
    }
}
