using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WTS.BL.Common;
using WTS.BL.Utils;
using WTS.Code;
using WTS.DL;

namespace WTS.Controllers._Common
{
    [Authorize("Bearer")]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class ControllerBase : Controller
    {
        protected IConfiguration Configuration;
        protected IHostingEnvironment HostingEnvironment;

        protected AppService Service { get; set; }

        public ControllerBase(IConfiguration configuration, AppDbContext appDbContext, IHostingEnvironment hostingEnvironment)
        {
            ServiceProviders.HttpContextFunc = () => HttpContext;
            ServiceProviders.GetEnvFunc = () => hostingEnvironment;
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Service = new AppService(appDbContext);
        } 
    }
}