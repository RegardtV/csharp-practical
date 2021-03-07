using FullStack.API.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(ValidationResult[] results)// : base()//results[0].Message)
        {
            this.Errors = new ReadOnlyCollection<ValidationResult>(results);
        }

        public ReadOnlyCollection<ValidationResult> Errors { get; }
    }
}
