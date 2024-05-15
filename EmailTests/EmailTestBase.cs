using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailTests
{
    public class EmailTestBase
    {
        public TestContext TestContext { get; set; }

        public T GetTestSetting<T>(string name,  T defaultValue)
        {
            T returnValue = defaultValue;

            try
            {
                var temp = TestContext.Properties[name];
                if (temp != null)
                {
                    returnValue = (T)Convert.ChangeType(temp, typeof(T));
                }
            }
            catch
            {

            }

            return returnValue;
        }
    }
}
