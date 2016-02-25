using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiCustomResponseDemo.Core.Filters.ActionFilters
{
    /// <summary>
    /// 將Api結果統一格式回傳的ActionFilter
    /// </summary>
    public class ApiResponseAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在叫用動作方法之後發生。
        /// </summary>
        /// <param name="actionExecutedContext">動作已執行內容。</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Exception != null)
            {
                return;
            }

            var result = new ApiResponse
            {
                StatusCode = actionExecutedContext.ActionContext.Response.StatusCode,
                Result = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result
            };

            // 重新封裝回傳格式
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.StatusCode, result);
        }
    }
}