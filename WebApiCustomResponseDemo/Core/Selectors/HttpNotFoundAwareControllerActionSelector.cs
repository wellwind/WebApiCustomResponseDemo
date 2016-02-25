using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApiCustomResponseDemo.Controllers;

namespace WebApiCustomResponseDemo.Core.Selectors
{
    /// <summary>
    /// HttpNotFound使用自訂Controller的ApiActionSelector
    /// </summary>
    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundAwareControllerActionSelector"/> class.
        /// </summary>
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        /// <summary>
        /// 為 <see cref="T:System.Web.Http.Controllers.ApiControllerActionSelector" /> 選取動作。
        /// </summary>
        /// <param name="controllerContext">控制器內容。</param>
        /// <returns>
        /// 選取的動作。
        /// </returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                setErrorController(controllerContext, ex);
                decriptor = base.SelectAction(controllerContext);
            }
            return decriptor;
        }

        private static void setErrorController(HttpControllerContext controllerContext, HttpResponseException ex)
        {
            var controllerName = "Error404";
            var code = ex.Response.StatusCode;
            var routeValues = controllerContext.RouteData.Values;
            if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
            {
                controllerName = "ErrorOthers";
                routeValues["id"] = code;
            }
            routeValues["action"] = "Get";
            controllerContext.Request.Method = HttpMethod.Get;

            IHttpController httpController = new Error404Controller();
            controllerContext.Controller = httpController;
            controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration,
                controllerName, httpController.GetType());
        }
    }
}