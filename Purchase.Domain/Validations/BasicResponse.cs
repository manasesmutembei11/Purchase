using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Validations
{
    public class BasicResponse
    {
        public BasicResponse()
        {
            Errors = new List<ValidationError>();
        }

        public bool Status { get { return Errors.Count == 0 ? true : false; } }
        public string Message { get; set; }
        public List<ValidationError> Errors { get; }
        public void AddError(int code, string message)
        {
            Errors.Add(new ValidationError(code, message));
        }

    }
    public class BasicResponse<T> : BasicResponse
    {
        public BasicResponse()
        {

        }
        public T Data { get; set; }
    }
}
