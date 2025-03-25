using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Company.ClientName.PL.Models;
using Company.ClientName.PL.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Company.ClientName.PL.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private readonly IScopedService scopedService01;
    //private readonly IScopedService scopedService02;
    //private readonly ITransiantService transiantService01;
    //private readonly ITransiantService transiantService02;
    //private readonly ISingletonService singletonService01;
    //private readonly ISingletonService singletonService02;

    public HomeController(
        ILogger<HomeController> logger/*,
        IScopedService scopedService01,
        IScopedService scopedService02,
        ITransiantService transiantService01,
        ITransiantService transiantService02,
        ISingletonService singletonService01,
        ISingletonService singletonService02*/
        )
    {
        _logger = logger;
        //this.scopedService01 = scopedService01;
        //this.scopedService02 = scopedService02;
        //this.transiantService01 = transiantService01;
        //this.transiantService02 = transiantService02;
        //this.singletonService01 = singletonService01;
        //this.singletonService02 = singletonService02;
    }

    //public string TestLifeTime()
    //{
    //    StringBuilder builder = new StringBuilder();

    //    builder.Append($"scopedService01 :: { scopedService01.GetGuid()}\n");
    //    builder.Append($"scopedService02 :: {scopedService02.GetGuid()}\n\n");
    //    builder.Append($"transiantService01 :: {transiantService01.GetGuid()}\n");
    //    builder.Append($"transiantService02 :: {transiantService02.GetGuid()}\n\n");
    //    builder.Append($"singletonService01 :: {singletonService01.GetGuid()}\n");
    //    builder.Append($"singletonService02 :: {singletonService02.GetGuid()}\n\n");

    //    return builder.ToString();
    //}

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
