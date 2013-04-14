using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFiction.Acm.ApplicationContextManaging.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging
{
    public class ApplicationContextManager : IApplicationContextManager
    {
        private readonly IAcmControllerFactory _acmControllerFactory;

        public ApplicationContextManager(IAcmControllerFactory acmControllerFactory)
        {
            _acmControllerFactory = acmControllerFactory;
        }
    }
}
