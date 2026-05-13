using Asp.Versioning;
using Faradars.Application.Interfaces.Services.Payments.Invoice;
using Faradars.WebApi.Common;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Invoice;

[ApiVersion("1")]

public class InvoiceController(IInvoiceService service) : AdminBaseController
{
    
}