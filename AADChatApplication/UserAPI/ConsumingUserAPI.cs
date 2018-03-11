using System;
using System.Threading.Tasks;
using AADChatApplication.Models;
using RestSharp;

namespace AADChatApplication.UserAPI
{
    public class ConsummingUserAPI
    {
        private RestClient client = null;

        public ConsummingUserAPI()
        {
            client = new RestClient("http://localhost:16725/api");
        }

        public async Task<T> Get<T>(string EndPoint)
        {
            try
            {
                var request = new RestRequest(EndPoint, Method.GET);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                request.RequestFormat = DataFormat.Json;
                var asu = client.ExecuteGetTaskAsync<T>(request);
                var response = await asu;
                return response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Post<T>(string EndPoint, User user)
        {
            try
            {
                var request = new RestRequest(EndPoint, Method.POST);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                request.AddJsonBody(user);
                var asu = client.ExecutePostTaskAsync<T>(request);
                var response = await asu;
                return response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
