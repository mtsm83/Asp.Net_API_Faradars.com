using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Common;

[ApiController]
[ApiExplorerSettings(GroupName = "user")]
[Route("api/user_v{version:apiVersion}/[controller]/[action]/")]
public class UserBaseController : ControllerBase
{

}