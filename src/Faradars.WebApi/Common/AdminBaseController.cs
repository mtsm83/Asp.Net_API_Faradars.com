using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Common;

[ApiController]
[Route("api/admin_v{version:apiVersion}/[controller]/[action]")]
[ApiExplorerSettings(GroupName = "admin")]

public class AdminBaseController : ControllerBase
{
    
}