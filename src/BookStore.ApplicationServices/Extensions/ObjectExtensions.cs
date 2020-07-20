using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.ApplicationServices.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetUniqueHash<T>(this T obj)
            where T : class
        {
            var stringValue = JsonConvert.SerializeObject(obj);

            return GetMD5Hash(stringValue);
        }


        private static string GetMD5Hash(string objectStringVal)
        {
            var md5 = MD5.Create();

            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(objectStringVal));

            return System.Convert.ToBase64String(Encoding.ASCII.GetBytes(objectStringVal));
        }
    }
}
