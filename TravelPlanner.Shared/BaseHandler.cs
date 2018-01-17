using System;
using System.Collections.Generic;
using System.Text;
using TravelPlanner.Shared.Enums;

namespace TravelPlanner.Shared
{
    public abstract class BaseHandler
    {
        protected ResponseStatus GetResponseStatus(object result)
        {
            return result == null ? ResponseStatus.Failed : ResponseStatus.Succeeded;
        }
    }
}
