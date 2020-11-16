using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.Utilities.Exceptions
{
    public class ErrorException: Exception
    {
        public int exceptionCode;
        public string exceptionMessage;
        public ErrorException(int code, string message) :base(message)
        {
            exceptionMessage = message;
            exceptionCode = code;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { exceptionCode, Message = exceptionMessage });
        }
    }
}
