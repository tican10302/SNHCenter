using Microsoft.AspNetCore.Mvc.Rendering;

namespace GUI.Constants;

public class SysConfig
{
    public static List<SelectListItem> IsActive { get; } = new List<SelectListItem>()
    {
        new SelectListItem() {Text = "Online", Value = "true", Selected = true},
        new SelectListItem() {Text = "Offine", Value = "false"},
    };

    public static List<SelectListItem> Gender { get; } = new List<SelectListItem>()
    {
        new SelectListItem() { Text = "Male", Value = "0" },
        new SelectListItem() { Text = "Female", Value = "1" },
        new SelectListItem() { Text = "Other", Value = "2" },
    };
}