using System;
using Newtonsoft.Json;
using BeThere.Models;
using BeThere.Services;
using BeThere;
using System.Text;

namespace BeThere.Services
{
    public class ConnectToAppService : BaseService
    {
        //private readonly string m_EndPointUrl = "ConnectToApp";

        public async Task<ResultUnit<string>> TryToLogin(string i_Username, string i_Password)
        {
            ResultUnit<string> result = new ResultUnit<string>();
            string endPointQueryUri = $"Login?UserName={i_Username}&Password={i_Password}";

            HttpResponseMessage response = await GetClient().GetAsync(endPointQueryUri);
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                result.IsSuccess = false;
                result.ResultMessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException();
            }

            return result;
        }

        public async Task<ResultUnit<string>> TryToRegisterUser(User i_UserData)
        {
            ResultUnit<string> result = new ResultUnit<string>();
            string endPointQueryUri = $"CreateAccount";

            string jsonData = JsonConvert.SerializeObject(i_UserData);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await GetClient().PostAsync(endPointQueryUri, content);
            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                result.IsSuccess = false;
                result.ResultMessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException();
            }

            return result;
        }



    }
}
