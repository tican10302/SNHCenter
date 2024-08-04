namespace GUI.Constants;

public class URL_API
{
    #region Base
    public const string PING = "ping";
    #endregion
    
    #region System
    // Account
    public const string ACCOUNT_LOGIN = "account/login";
    public const string ACCOUNT_GETPERMISSION = "account/get-permission";
    public const string ACCOUNT_GETLISTMENU = "account/get-list-menu";

    #endregion

    #region Category
    public const string SHIFT_GETLIST = "shift/get-list-paging";
    public const string SHIFT_GETBYPOST = "shift/get-by-post";
    public const string SHIFT_GETBYID = "shift/get-by-id";
    public const string SHIFT_INSERT = "shift/insert";
    public const string SHIFT_UPDATE = "shift/update";
    public const string SHIFT_DELETELIST = "shift/delete-list";
    #endregion
}