using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Service.Filters {
    public class RequestExceptionFilter : ActionFilterAttribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {

            try {
                if (context.Exception != null) {
                    string uaString = context.HttpContext.Request?.Headers["User-Agent"];

                    //do log

                }
            } catch (Exception) {
                //TODO: Tell the user that logging failed, and ask them to contact Pascal support
            }


            context.HttpContext.Response.StatusCode = 500;



            var errorModel = new {
                Message = context.Exception.Message,
                Type = context.Exception.GetType()
            };




            context.Result = new JsonResult(errorModel) {
                StatusCode = 500,
            };
        }
    }       
}

