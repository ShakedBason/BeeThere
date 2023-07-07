using System;
using Newtonsoft.Json;
using BeThere.Models;
using BeThere.Services;
using BeThere;

namespace BeThere.Services
{
    public class ConnectToAppService : BaseService
    {
        private readonly string m_EndPointUrl = "Login";


        public async Task<ResultUnit<string>> TryToLogin(string i_Username, string i_Password)
        {
            ResultUnit<string> result = new ResultUnit<string>();
            string endPointQueryUri = $"{m_EndPointUrl}?UserName={i_Username}&Password={i_Password}";

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
    }
}
