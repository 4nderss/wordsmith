using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WordSmith.Core.Managers.Interfaces;
using WordSmith.Core.Models.Database;

namespace Service.Filters {
    public class RequestExceptionFilter : ActionFilterAttribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {

            try {
                if (context.Exception != null) {
                    //do log
                    var logEntry = new ErrorLog();
                    logEntry.LogDate = DateTime.Now;
                    logEntry.Message = context.Exception.Message;
                    logEntry.Stacktrace = context.Exception.StackTrace;
                    logEntry.Server = Environment.MachineName;

                    var databaseManager = context.HttpContext.RequestServices.GetService(typeof(IDatabaseManager)) as IDatabaseManager;
                    databaseManager.LogErrorAsync(logEntry);

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

