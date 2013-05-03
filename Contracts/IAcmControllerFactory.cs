using System;
using System.Web.Mvc;

namespace CodeFiction.Acm.Contracts
{
    public interface IAcmControllerFactory : IControllerFactory 
    {
        event AcmContextEventHandler OnControllerCreation;
        event EventHandler OnControllerCreated;
    }
}
