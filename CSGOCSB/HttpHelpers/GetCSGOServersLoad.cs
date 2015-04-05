using CSGOCSB.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSGOCSB.HttpHelpers
{
    public static partial class HttpHelpers
    {
        public static async Task<ServerLoadModel> GetServerLoadData()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApplicationData.ServerLoadApiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var steamApiResponse = await response.Content.ReadAsStringAsync();
                        var results = JsonConvert.DeserializeObject<ServerLoadModel>(steamApiResponse);
                        return results;
                    }
                }
            }
            catch
            {
            }
            return new ServerLoadModel();
        }
    }
}
