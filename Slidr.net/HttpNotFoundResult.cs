using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Slidr.net
{
    public class HttpNotFoundResult : ActionResult
    {
        public HttpNotFoundResult(String message)
        {
            Message = message;
        }

        public HttpNotFoundResult()
            : this(String.Empty) { }

        public String Message { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            throw new HttpException((Int32)HttpStatusCode.NotFound, Message);
        }
    }

}