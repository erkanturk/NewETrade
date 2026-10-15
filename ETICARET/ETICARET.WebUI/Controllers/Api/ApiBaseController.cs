using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api;

[ApiController]
[Produces("application/json")]
[EnableCors(CorsPolicyName)]
[IgnoreAntiforgeryToken]

public abstract class ApiBaseController : ControllerBase
{
    public const string CorsPolicyName = "ApiCors";
    //iphone.1.jpg "Https://localhost:3000/img/products/iphone-1.jpg"
    protected string AbsoluteUrl(string path) => $"{Request.Scheme}://{Request.Host}{path}";
}
