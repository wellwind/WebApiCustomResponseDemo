using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApiCustomResponseDemo.Controllers
{
    public class ErrorOthersController : ApiController
    {
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object Get(int id)
        {
            return new HttpStatusCodeResult(id);
        }
    }
}