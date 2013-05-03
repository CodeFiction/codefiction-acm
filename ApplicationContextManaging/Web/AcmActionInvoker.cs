using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using CodeFiction.Acm.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class AcmActionInvoker : ControllerActionInvoker
    {
        private readonly ApplicationContext _appContext;

        public AcmActionInvoker(ApplicationContext context)
        {
            _appContext = context;

        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            return base.InvokeAction(controllerContext, actionName);
        }

        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            //var mi = controllerDescriptor.ControllerType.GetMethod("About");
            ActionDescriptor desc = new AcmMethodDescriptor(controllerDescriptor, "", _appContext);
            return desc;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
        {
            if (actionReturnValue == null)
                return new EmptyResult();

            ActionResult result = actionReturnValue as ActionResult;
            if (result != null)
                return result;

            // This is where the magic happens
            // Depending on the value in the _outputType field,
            // return an appropriate ActionResult

            string _outputType = "";//from context
            switch (_outputType)
            {
                case "json":
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string json = serializer.Serialize(actionReturnValue);
                        return new ContentResult { Content = json, ContentType = "application/json" };
                    }
                case "xml":
                    {
                        XmlSerializer serializer = new XmlSerializer(actionReturnValue.GetType());
                        using (StringWriter writer = new StringWriter())
                        {
                            serializer.Serialize(writer, actionReturnValue);
                            return new ContentResult { Content = writer.ToString(), ContentType = "text/xml" };
                        }
                    }
                    //case "csv":
                    //    controllerContext.HttpContext.Response.AddHeader("Content-Disposition",
                    //                                                     "attachment; filename=items.csv");
                    //    return new ContentResult
                    //        {
                    //            Content = ToCsv(actionReturnValue as IList<Item>),
                    //            ContentType = "application/ms-excel"
                    //        };
                    //case "pdf":
                    //    string filePath = controllerContext.HttpContext.Server.MapPath("~/items.pdf");
                    //    controllerContext.HttpContext.Response.AddHeader("content-disposition",
                    //                                                     "attachment; filename=items.pdf");
                    //    ToPdf(actionReturnValue as IList<Item>, filePath);
                    //    return new FileContentResult(StreamFile(filePath), "application/pdf");

                default:
                    controllerContext.Controller.ViewData.Model = actionReturnValue;
                    return new ViewResult
                        {
                            TempData = controllerContext.Controller.TempData,
                            ViewData = controllerContext.Controller.ViewData,
                            ViewName = _appContext.Story.ResultWrapper != null ? _appContext.Story.ResultWrapper.ViewName : "",
                            MasterName = ""
                        };
            }
        }

        protected override ResultExecutedContext InvokeActionResultWithFilters(ControllerContext controllerContext, IList<IResultFilter> filters, ActionResult actionResult)
        {
            return base.InvokeActionResultWithFilters(controllerContext, filters, actionResult);
        }
    }
}