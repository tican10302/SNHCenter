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
    public const string ACCOUNT_GETLIST = "account/get-list";
    public const string ACCOUNT_GETBYPOST = "account/get-by-post";
    public const string ACCOUNT_GETBYID = "account/get-by-id";
    public const string ACCOUNT_INSERT = "account/insert";
    public const string ACCOUNT_UPDATE = "account/update";
    public const string ACCOUNT_DELETELIST = "account/delete-list";

    // Group Permission
    public const string GROUPPERMISSION_GETLIST = "grouppermission/get-list";
    public const string GROUPPERMISSION_GETBYPOST = "grouppermission/get-by-post";
    public const string GROUPPERMISSION_GETBYID = "grouppermission/get-by-id";
    public const string GROUPPERMISSION_INSERT = "grouppermission/insert";
    public const string GROUPPERMISSION_UPDATE = "grouppermission/update";
    public const string GROUPPERMISSION_GETALLFORCOMBOBOX = "grouppermission/get-all-for-combobox";

    #endregion

    #region Category
    // Shift
    public const string SHIFT_GETLIST = "shift/get-list-paging";
    public const string SHIFT_GETBYPOST = "shift/get-by-post";
    public const string SHIFT_GETBYID = "shift/get-by-id";
    public const string SHIFT_INSERT = "shift/insert";
    public const string SHIFT_UPDATE = "shift/update";
    public const string SHIFT_DELETELIST = "shift/delete-list";
    #endregion
}