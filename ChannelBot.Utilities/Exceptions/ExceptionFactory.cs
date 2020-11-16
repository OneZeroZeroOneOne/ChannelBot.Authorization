using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.Utilities.Exceptions
{
    public class ExceptionFactory
    {
        public static ErrorException SoftException(ExceptionEnum code, string message)
        {
            return new ErrorException((int)code, message);
        }

        public static FriendlyException FriendlyException(ExceptionEnum code, string message)
        {
            return new FriendlyException((int)code, message);
        }
    }
}
