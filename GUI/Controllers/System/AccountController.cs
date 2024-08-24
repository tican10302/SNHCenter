using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using DTO.Base;
using DTO.Common;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;
using GUI.Constants;
using GUI.Helpers;
using GUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.System;

public class AccountController : Controller
{
    private ICacheService _cacheService;

    public AccountController()
    {
        _cacheService = new InMemoryCache();
    }
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View("~/Views/System/Account/Index.cshtml");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<JsonResult> Login(AccountDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(CommonFunc.GetModelState(this.ModelState));
            }

            ResponseData response = this.LoginAPI(URL_API.ACCOUNT_LOGIN, request);
            if (response.Status)
            {
                var accountData = JsonConvert.DeserializeObject<AccountPermissionModel>(response.Data.ToString());
                var userData = accountData.Account;

                var claims = new List<Claim>();

                _cacheService.Set(userData.UserName + "_menu", JsonConvert.SerializeObject(accountData.Menu), 60);
                _cacheService.Set(userData.UserName + "_role", JsonConvert.SerializeObject(accountData.Permission), 60);
                _cacheService.Set(userData.UserName + "_info", JsonConvert.SerializeObject(accountData.Account), 60);
                _cacheService.Set(userData.UserName + "_grouprole", JsonConvert.SerializeObject(accountData.GroupPermission), 60);

                claims.Add(new Claim(ClaimTypes.Name, userData.UserName));
                claims.Add(new Claim(ClaimTypes.Surname, userData.FirstName ?? ""));
                claims.Add(new Claim(ClaimTypes.GivenName, userData.LastName ?? ""));
                claims.Add(new Claim(ClaimTypes.Role, userData.Role ?? ""));
                claims.Add(new Claim("UserId", userData.Id.ToString() ?? ""));
                claims.Add(new Claim("Token", userData.Token));
                if (!string.IsNullOrEmpty(userData.Avatar))
                {
                    claims.Add(new Claim(ClaimTypes.Thumbprint, userData.Avatar));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                
                return Json(new { IsSuccess = true, Message = "Logged in successfully", Data = "" });

            }
            else
            {
                throw new Exception(CommonFunc.GetModelState(this.ModelState));
            }
        }
        catch (Exception ex)
        {
            return Json(new { IsSuccess = false, Message = ex.Message, Data = "" });
        }
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
        foreach (string cacheKey in cacheKeys)
        {
            MemoryCache.Default.Remove(cacheKey);
        }

        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Account");
    }

    public ResponseData LoginAPI(string action, object model)
    {
        ResponseData response = new ResponseData();
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetBLLUrl());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Clear();
                var responseTask = client.PostAsJsonAsync(action, model);
                responseTask.Wait();
                response = ExcuteAPIResponse(responseTask);
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = DTO.Common.CommonFunc.ConvertExceptionToMessage(ex, "Error system.");
        }

        return response;
    }

    public ResponseData ExcuteAPIResponse(Task<HttpResponseMessage> responseTask)
    {
        var response = new ResponseData();
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
    
    private string? GetBLLUrl()
    {
        return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BLLUrl").Value?.ToString();
    }
}