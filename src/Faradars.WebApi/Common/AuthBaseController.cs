using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Common;

[ApiController]
[ApiExplorerSettings(GroupName = "auth")]
[Route("api/auth_v{version:apiVersion}/[controller]/[action]")]
public class AuthBaseController : ControllerBase
{
    
}