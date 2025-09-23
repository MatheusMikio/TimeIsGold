using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ErrorMessage
    {
        public string Property { get; set; }
        public string Message { get; set; }
        public ErrorMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }
    }
}
