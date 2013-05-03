using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodeFiction.Acm.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class AcmController : Controller
    {
        private readonly ApplicationContext _context;
        public ApplicationContext Context
        {
            get { return _context; }
        }
        public AcmController(ApplicationContext context)
        {
            _context = context;
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}
