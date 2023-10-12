namespace EFK.SampleApp.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/hello")]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return this.Ok("Hello, World!");
    }
}