using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Application.ViewModels
{
    public class ApiResponse : SimpleResponse
    {
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorHeader { get; set; }
        public string ErrorType => Success ? null : (ValidationErrors.Any() ? "validation" : "error");
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public object DeveloperMessage { get; set; }

        public string WarningHeader { get; set; }
        public string WarningType { get; set; }
        public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> ValidationWarnings { get; set; } = new Dictionary<string, List<string>>();
    }
    public class ApiResponse<T> : SimpleResponse<T>
    {
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorHeader { get; set; }
        public string ErrorType => Success ? null : (ValidationErrors.Any() ? "validation" : "error");
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public object DeveloperMessage { get; set; }
        public string WarningHeader { get; set; }
        public string WarningType { get; set; }
        public Dictionary<string, List<string>> ValidationErrors { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> ValidationWarnings { get; set; } = new Dictionary<string, List<string>>();
    }
}
