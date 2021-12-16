using Newtonsoft.Json;
using System.Text;

namespace Blockchain.Utils.Helpers
{
    public static class ConverterHelper
    {
        public static string Serialize(object objectToConvert)
        {
            return JsonConvert.SerializeObject(objectToConvert);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static byte[] GetJsonAsByteArray(string json)
        {
            return Encoding.UTF8.GetBytes(json);
        }

        public static string GetMessageFromUTF8ByteArray(byte[] array)
        {
            return Encoding.UTF8.GetString(array);
        }
    }
}
