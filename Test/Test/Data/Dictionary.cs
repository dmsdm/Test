using System;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data
{
    public class Dictionary
    {
        private static string key = "API-KEY";

        public static async Task<String[]> GetLangs()
        {
            string queryString = "https://dictionary.yandex.net/api/v1/dicservice.json/getLangs?key=" + key;

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results is Newtonsoft.Json.Linq.JArray)
            {
                return results.ToObject<String[]>();
            }
            else
            {
                return results;
            }
        }

        public static async Task<String> Lookup(String direction, String text)
        {
            string queryString = String.Format("https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key={0}&lang={1}&text={2}", key, direction, text);

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["def"] != null)
            {
                try
                {
                    int size = results["def"][0]["tr"].Count;
                    if (size > 0)
                    {
                        StringBuilder res = new StringBuilder();
                        for (int i = 0; i<size; ++i)
                        {
                            res.Append(results["def"][0]["tr"][i]["text"]);
                            res.Append("; ");
                        }
                        res.Remove(res.Length - 2, 2);
                        return res.ToString();
                    }
                }
                catch (Exception ignore)
                {
                    return text;
                }
            }
            return null;
        }
    }
}