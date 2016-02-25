using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApiCustomResponseDemo.Utils;

namespace WebApiCustomResponseDemo.Core.Handlers
{
    /// <summary>
    /// 全域的Api錯誤處理
    /// </summary>
    public class GlobalApiExceptionHandler : ExceptionHandler
    {
        internal class GlobalExceptionResponseActoinResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public Exception Exception { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var apiResponseResult = ExceptionUtils.ConvertToApiResponse(Exception);
                var response = new HttpResponseMessage(apiResponseResult.StatusCode)
                {
                    Content = new ObjectContent(apiResponseResult.GetType(), apiResponseResult, new JsonMediaTypeFormatter()),
                    RequestMessage = Request,
                };
                return Task.FromResult(response);
            }
        }

        /// <summary>
        /// 在衍生類別中覆寫時，同步處理例外狀況。
        /// </summary>
        /// <param name="context">例外狀況處理常式內容。</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new GlobalExceptionResponseActoinResult
            {
                Request = context.ExceptionContext.Request,
                Exception = context.Exception
            };
        }
    }
}