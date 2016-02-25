using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApiCustomResponseDemo.Controllers
{
    public class Error404Controller : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            throw new Exception("找不到此API");
        }
    }
}