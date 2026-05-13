using Faradars.Application.Interfaces.General;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.Services;

public class BaseService(IHttpContextAccessor httpContextAccessor): IScopedDependency
{
    private readonly HttpRequest _request = httpContextAccessor.HttpContext.Request;
    protected string ImageBaseUrl => $"{_request.Scheme}://{_request.Host}";
    
    public string GetImageBaseUrl()
    {
        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null) return string.Empty;
        
        return $"{request.Scheme}://{request.Host}";
    }
}   