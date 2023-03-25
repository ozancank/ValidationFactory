using Microsoft.AspNetCore.Mvc;
using ValidationFactory.Models;
using ValidationFactory.Validators;

namespace ValidationFactory.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        User user = new()
        {
            Name = "Bora",
            LastName = "Kasmer",
            UserName = "bA",
            Password = "hasßm+KKpEl/qgGDrqHZOi6RCQ9Wlj48vYzDnDdm6ctFGO4=æ1rN9gprpjCRDlkINl+ybHw==",
            Email = "encßnkLseH+tsLd1JH6dMzAB9giK5uX/YjqrczLWwVPrT/M=",
            Email2 = "bora.kasmer78@gmail.com",
            BirthDate = new DateTime(1881, 6, 3),
            Gsm = "encßGWXGzj2SGYu70PzzMYVaFfWwl0wzS6Lc",
        };
        List<(bool, Exception)> errorList = new();
        errorList = ValidateClassProperties.GetValidateResult(user);
        ResultModel model = new() { Exception = errorList.Select(li => li.Item2).ToList(), User = user };
        return View(model);
    }

    [HttpPost]
    public IActionResult Index(User model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        User user = new()
        {
            Name = "Bora",
            LastName = "Kasmer",
            UserName = "bk@smer",
            Password = "vbt123456",
            Email = "bora.kasmer78",
            Email2 = "bora.kasmer78@gmail.com",
            BirthDate = new DateTime(1881, 6, 3),
            Gsm = "5426781944",
        };
        List<(bool, Exception)> errorList = new();
        errorList = ValidateClassProperties.GetValidateResult(user);
        ResultModel resultModel = new() { Exception = errorList.Select(li => li.Item2).ToList(), User = user };
        return View(resultModel);
    }
}