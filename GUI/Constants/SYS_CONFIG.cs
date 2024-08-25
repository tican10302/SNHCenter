using Microsoft.AspNetCore.Mvc.Rendering;

namespace GUI.Constants;

public class SYS_CONFIG
{
    public static List<SelectListItem> ISACTIVE = new List<SelectListItem>()
    {
        new SelectListItem() {Text = "Online", Value = "true", Selected = true},
        new SelectListItem() {Text = "Offine", Value = "false"},
    };
}