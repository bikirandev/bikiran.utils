using System;

namespace Bikiran.Utils.Models
{
    public class Excep
    {
        public static Exception Create(string message, string reference = "")
        {
            var exception = new Exception(message);
            exception.Data["Reference"] = reference;
            return exception;
        }
    }
}
