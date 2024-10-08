using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace picpay_simplificado.Services
{
    public class UserService
    {

        public async Task NotifyUser(int payee)
        {
            using var httpClient = new HttpClient();
            try
            {
                string url = "https://util.devi.tools/api/v1/notify";

                var data = new
                {
                    payee,
                };

                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                Console.WriteLine(response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}