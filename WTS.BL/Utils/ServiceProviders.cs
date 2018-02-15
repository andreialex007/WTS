using System;
using Microsoft.AspNetCore.Hosting;

namespace WTS.BL.Utils
{
    public static class ServiceProviders
    {
        public static Func<Microsoft.AspNetCore.Http.HttpContext> HttpContextFunc = null;
        public static Microsoft.AspNetCore.Http.HttpContext HttpContext => HttpContextFunc();


        public static Func<IHostingEnvironment> GetEnvFunc = null;
        public static IHostingEnvironment Environment => GetEnvFunc();


        public static string RootDirectory => Environment.WebRootPath.Replace("wwwroot",string.Empty);
    }
}
