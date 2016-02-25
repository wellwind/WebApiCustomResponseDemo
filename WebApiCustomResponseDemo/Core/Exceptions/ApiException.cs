using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApiCustomResponseDemo.Core.Exceptions
{
    /// <summary>
    /// Api Exception基底
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        /// <value>
        /// The error identifier.
        /// </value>
        public string ErrorId { get; set; }

        /// <summary>
        /// 回傳的HTTP status Code
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException()
            : this("API呼叫錯誤")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public ApiException(string errorMessage)
            : base(errorMessage)
        {
            ErrorId = "unknown_api_error";
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}