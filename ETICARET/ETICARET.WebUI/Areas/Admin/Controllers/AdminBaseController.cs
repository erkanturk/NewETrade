using ETICARET.Entities.Identity;
using ETICARET.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles =Roles.Admin)]
public class AdminBaseController : BaseController;
