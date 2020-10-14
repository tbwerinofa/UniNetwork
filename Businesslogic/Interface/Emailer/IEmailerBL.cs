using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interface
{
    public interface IEmailerBL
    {
        Dictionary<bool, string> SendEmail(DataAccess.QueuedEmail emailRecord);
    }
}
