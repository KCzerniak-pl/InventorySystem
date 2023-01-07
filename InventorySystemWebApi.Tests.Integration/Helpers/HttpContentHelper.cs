using Newtonsoft.Json;
using System.Text;

namespace InventorySystemWebApi.Tests.Integration.Helpers
{
    public static class HttpContentHelper
    {
        // Extension method for object.
        public static HttpContent ToJsonHttpContent(this object obj)
        {
            // Serializable object to JSON.
            var json = JsonConvert.SerializeObject(obj);

            // Create HttpContent.
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return httpContent;
        }
    }
}
