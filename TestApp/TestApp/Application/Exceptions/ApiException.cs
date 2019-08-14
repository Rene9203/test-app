using System;
using System.Collections.Generic;

namespace TestApp.Application.Exceptions
{
    public class ApiException : Exception
    {
        public IEnumerable<string> Messages => _messages;
        private readonly List<string> _messages = new List<string>();

        public int StatusCode { get; private set; }

        public ApiException(IEnumerable<string> messages, int statusCode)
        {
            StatusCode = statusCode;
            _messages.AddRange(messages);
        }

        public ApiException(string message, int statusCode)
        {
            StatusCode = statusCode;
            AddErrorMessage(message);
        }

        public void AddErrorMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
