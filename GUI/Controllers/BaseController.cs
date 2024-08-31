using DTO.Base;
using DTO.System.Account.Models;
using GUI.Constants;
using GUI.Helpers;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;


namespace GUI.Controllers
{
    [CustomAuthorizeAttribute]
    public class BaseController<T> : Controller
    {
        private readonly ICacheService _cacheService = new InMemoryCache();

        public PermissionModel GetPerMission()
        {
            List<PermissionModel> fullRole = new List<PermissionModel>();

            //Get cache
            string cacheMenu = _cacheService.Get<string>(GetUserName() + "_menu");
            string cacheRole = _cacheService.Get<string>(GetUserName() + "_role");
            string cacheInfo = _cacheService.Get<string>(GetUserName() + "_info");
            string cacheGroupRole = _cacheService.Get<string>(GetUserName() + "_grouprole");
            if (string.IsNullOrEmpty(cacheRole))
            {
                ResponseData response = this.PostAPI(URL_API.ACCOUNT_GETPERMISSION, new { Id = GetUserId() });
                if (response.Status)
                {
                    _cacheService.Set(GetUserName() + "_role", response.Data.ToString(), 60);
                    fullRole = JsonConvert.DeserializeObject<List<PermissionModel>>(response.Data.ToString());
                }
            }
            else
            {
                fullRole = JsonConvert.DeserializeObject<List<PermissionModel>>(cacheRole);
            }
            if (string.IsNullOrEmpty(cacheMenu))
            {
                ResponseData response = this.PostAPI(URL_API.ACCOUNT_GETLISTMENU, new { Id = GetUserId() });
                if (response.Status)
                {
                    _cacheService.Set(GetUserName() + "_menu", response.Data.ToString(), 60);
                }
            }
            if (string.IsNullOrEmpty(cacheInfo))
            {
                ResponseData response = this.PostAPI(URL_API.ACCOUNT_GETBYID, new { UserId = GetUserId() });
                if (response.Status)
                {
                    _cacheService.Set(GetUserName() + "_info", response.Data.ToString(), 60);
                }
            }
            if (string.IsNullOrEmpty(cacheGroupRole))
            {
                ResponseData response = this.PostAPI(URL_API.GROUPPERMISSION_GETLIST, new GetAllRequest());
                if (response.Status)
                {
                    _cacheService.Set(GetUserName() + "_grouprole", response.Data.ToString(), 60);
                }
            }

            PermissionModel result = fullRole.FirstOrDefault(x => x.ControllerName == typeof(T).Name.ToLower());
            if (result == null) result = new PermissionModel();

            return result;
        }
        
        public string GetUserName()
        {
            try
            {
                if (User != null && User.Identity != null && User.Identity.Name != null)
                {
                    return User.Identity.Name;
                }
                else
                {
                    throw new Exception("Please log in");
                }
            }
            catch
            {
                throw;
            }
        }
        
        public string GetUserId()
        {
            try
            {
                if (User != null && User.Identity != null && User.Identity.Name != null)
                {
                    var UserId = @User.Claims.FirstOrDefault(c => c.Type == "UserId");
                    if (UserId != null)
                    {
                        return UserId.Value;
                    }
                    else
                    {
                        throw new Exception("Please log in");
                    }
                }
                else
                {
                    throw new Exception("Please log in");
                }
            }
            catch
            {
                throw;
            }
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
                response.Message = DTO.Common.CustomException.ConvertExceptionToMessage(ex, "Error system.");
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
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseTask = client.PostAsJsonAsync(action, model);
                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = DTO.Common.CustomException.ConvertExceptionToMessage(ex, "Error system.");
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

                if (string.IsNullOrEmpty(readTask.Result))
                {
                    response.Status = false;
                    response.Message = "Error system: Path not found";
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