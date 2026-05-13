using Asp.Versioning;
using Faradars.Application.Interfaces.Services.Students.Monitoring;
using Faradars.WebApi.Common;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Monitoring;

[ApiVersion("1")]

public class MonitoringController(IMonitoringService service) : AdminBaseController
{
    
}