using _88Studio.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace _88Studio.Web.Base
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var model = filterContext.Controller.ViewData.Model;
            if(model != null)
            {
                var baseModel = model as BaseDto;
                if (!modelState.IsValid && baseModel != null)
                {
                    baseModel.Status = HttpStatusCode.BadRequest;
                    baseModel.AppendMessage(_88Studio.Web.Code.Utils.GetErrorsFromModelState(modelState));
                }
            }
        }
    }
}