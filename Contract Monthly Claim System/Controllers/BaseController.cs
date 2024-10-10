using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        // Retrieve the user's role from the session
        var userEmail = HttpContext.Session.GetString("UserEmail");
        var userRole = HttpContext.Session.GetString("UserRole");
        var userName = HttpContext.Session.GetString("UserName");
        var userID = HttpContext.Session.GetInt32("UserID");

        ViewBag.IsLoggedIn = !string.IsNullOrEmpty(userEmail);
        ViewBag.UserRole = userRole;
        ViewBag.UserName = userName;
        ViewBag.UserID = userID;
    }
}
