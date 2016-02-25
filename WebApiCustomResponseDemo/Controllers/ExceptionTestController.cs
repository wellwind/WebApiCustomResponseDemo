using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiCustomResponseDemo.Core.Exceptions;

namespace WebApiCustomResponseDemo.Controllers
{
    /// <summary>
    /// 測試拋出Exception時是否會依統一格式回傳
    /// </summary>
    public class ExceptionTestController : ApiController
    {
        public ExceptionTestController()
        {
            throw new Exception("throw by constructor");
        }

        /// <summary>
        /// GET方法
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Test Exception Message...</exception>
        public object Get()
        {
            throw new Exception("Test Exception Message...");
        }

        /// <summary>
        /// POST方法
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApiException">Test Api Exception Message...</exception>
        public object Post()
        {
            throw new ApiException("Test Api Exception Message...");
        }
    }
}