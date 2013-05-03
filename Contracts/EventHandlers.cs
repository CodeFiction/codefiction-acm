using System;
using System.Runtime.InteropServices;
using System.Web.Routing;

namespace CodeFiction.Acm.Contracts
{
    /// <summary>
    /// Represents the method that will handle an event that has no event data.
    /// </summary>
    /// <param name="sender">The source of the event. </param><param name="e">An <see cref="T:System.EventArgs"/> that contains no event data. </param><filterpriority>1</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public delegate ApplicationContext AcmContextEventHandler(object sender, AcmEventArgs e);

    public class AcmEventArgs
    {
        public static readonly AcmEventArgs Empty = new AcmEventArgs();

        public string ControllerName { get; set; }
        public RequestContext RequestContext { get; set; }
    }
}
