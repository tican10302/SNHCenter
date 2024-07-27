using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using DTO.Base;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;


namespace GUI.Controllers
{
    [CustomAuthorizeAttribute]
    public class BaseController<T> : Controller
    {
        public BaseController()
        {
            
        }
        #region Execute API

        public ResponseData GetAPI(string action)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetBLLUrl() ?? string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());

                    var responseTask = client.GetAsync(action);
                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);

                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = DTO.Common.CustomException.ConvertExceptionToMessage(ex, "System error.");
            }

            return response;
        }

        public ResponseData PostAPI<T>(string action, T model)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    client.BaseAddress = new Uri(GetBLLUrl());
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());

                    //Called Member default GET All records  
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsJsonAsync(action, model);
                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = DTO.Common.CustomException.ConvertExceptionToMessage(ex, "Lỗi hệ thống.");
            }

            return response;
        }

        #endregion

        public ResponseData ExecuteAPIResponse(Task<HttpResponseMessage> responseTask)
        {
            ResponseData response = new ResponseData();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                if (readTask == null)
                {
                    response.Status = false;
                    response.Message = "Error system";
                }
                else
                {
                    string json = readTask.Result;
                    var resultData = JsonConvert.DeserializeObject<ModelApiBasic>(json);

                    response.Message = resultData.Message;
                    if (!resultData.Success || resultData.StatusCode != Convert.ToInt32(DTO.Common.StatusCode.Success))
                    {
                        response.Status = false;
                    }
                    else
                    {
                        response.Data = resultData.Result;
                    }
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Error system";
            }

            return response;
        }

        public string? GetToken()
        {
            return HttpContext.User.Claims.Where(x => x.Type == "Token").FirstOrDefault()?.Value.ToString();
        }
        private string? GetBLLUrl()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BLLUrl").Value?.ToString();
        }

        protected string GetBLLFileUrl()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BLLFileUrl").Value.ToString();
        }
    }
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.User.Claims.Where(x => x.Type == "Token").FirstOrDefault().Value.ToString();
                var url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BLLUrl").Value.ToString();
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsJsonAsync(URL_API.PING, new { });
                    responseTask.Wait();
                    var result = responseTask.Result;

                    //CHECK 401
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}