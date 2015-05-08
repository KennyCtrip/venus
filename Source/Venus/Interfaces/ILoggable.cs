using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Venus
{
    public interface ILoggable
    {
        void SetLogger(ILogger logger);
    }
}
