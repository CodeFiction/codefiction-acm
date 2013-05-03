using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeFiction.Acm.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class AcmMethodDescriptor : ActionDescriptor
    {
        private readonly ControllerDescriptor _controllerDescriptor;
        private readonly string _actionName;
        private readonly ApplicationContext _appContext;

        public AcmMethodDescriptor(ControllerDescriptor descriptor, string actionName, ApplicationContext appContext)
        {
            _controllerDescriptor = descriptor;
            _actionName = actionName;
            _appContext = appContext;
        }

        public override string ActionName
        {
            get { return _actionName; }
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get { return _controllerDescriptor; }
        }

        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            var result = _appContext.Story.Run(parameters);
            //Func<object, object> f = o => o.ToString();
            //ContentResult vr = new ContentResult();
            //vr.Content = f("aaa").ToString();
            return result;
        }

        public override ParameterDescriptor[] GetParameters()
        {
            var parameters = _appContext.Story.GetParameters();
            IList<ParameterDescriptor> descs = parameters.Select(p => (ParameterDescriptor)new ReflectedParameterDescriptor(p, this)).ToList();
            //buralara cacheler konacak..
            return descs.ToArray();
        }

        //private ParameterDescriptor[] LazilyFetchParametersCollection()
        //{
        //    return DescriptorUtil.LazilyFetchOrCreateDescriptors<ParameterInfo, ParameterDescriptor>(
        //        ref _parametersCache /* cacheLocation */,
        //        MethodInfo.GetParameters /* initializer */,
        //        parameterInfo => new ReflectedParameterDescriptor(parameterInfo, this) /* converter */);
        //}
    }
}