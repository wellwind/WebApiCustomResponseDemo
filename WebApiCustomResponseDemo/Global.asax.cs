using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiCustomResponseDemo.Utils;

namespace WebApiCustomResponseDemo
{
    // 注意: 如需啟用 IIS6 或 IIS7 傳統模式的說明，
    // 請造訪 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            // 測試在Request時在Api lifecycle前出錯時的情況
            // throw new Exception("thrown by Application_BeginRequest");
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception == null)
            {
                return;
            }

            object exceptionToSerialize = exception.InnerException ?? exception;
            Response.ContentType = "text/json";
            Response.Write(
                JsonConvert.SerializeObject(
                    ExceptionUtils.ConvertToApiResponse((Exception)exceptionToSerialize)));
            Response.End();
        }
    }
}