using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApiCustomResponseDemo.Core.Selectors
{
    /// <summary>
    /// HttpNotFound使用自訂Controller的ControllerSelector
    /// </summary>
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundAwareDefaultHttpControllerSelector"/> class.
        /// </summary>
        /// <param name="configuration">設定。</param>
        public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        /// <summary>
        /// 為指定的 <see cref="T:System.Net.Http.HttpRequestMessage" /> 選取 <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor" />。
        /// </summary>
        /// <param name="request">HTTP 要求的訊息。</param>
        /// <returns>
        /// 指定之 <see cref="T:System.Net.Http.HttpRequestMessage" /> 適用的 <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor" /> 執行個體。
        /// </returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                setErrorController(request, ex);

                decriptor = base.SelectController(request);
            }
            return decriptor;
        }

        private static void setErrorController(HttpRequestMessage request, HttpResponseException ex)
        {
            var code = ex.Response.StatusCode;
            var routeValues = request.GetRouteData().Values;
            routeValues["controller"] = "Error";
            if (code == HttpStatusCode.NotFound)
            {
                routeValues["controller"] = "Error404";
            }
            else
            {
                routeValues["controller"] = "ErrorOthers";
                routeValues["id"] = code;
            }
            routeValues["action"] = "Get";
            request.Method = HttpMethod.Get;
        }
    }
}