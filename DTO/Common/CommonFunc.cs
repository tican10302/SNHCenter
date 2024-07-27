using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DTO.Common;

public static class CommonFunc
{
    public static string GetModelState(ModelStateDictionary dic)
    {
        string error = "";

        var arrError = dic.Select(f => f.Value.Errors).Where(p => p.Count > 0).ToList();
        foreach (var item in arrError)
        {
            error += item[0].ErrorMessage + "<br />";
        }

        return error;
    }

    public static string GetModelStateAPI(ModelStateDictionary modelState)
    {
        var errorList = (from item in modelState.Values
            from error in item.Errors
            select error.ErrorMessage).ToList();

        return errorList[0];
    }
    
    public static string ConvertExceptionToMessage(Exception ex, string message)
    {
        return message + " " + ex.Message;
    }
}