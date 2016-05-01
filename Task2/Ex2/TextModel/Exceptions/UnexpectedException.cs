using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextModel.Exceptions
{
    public class UnexpectedException : Exception
    {
        public UnexpectedException() : base()
        {
            
        }

        public UnexpectedException(string message)
            : base(message)
        {

        }

        public UnexpectedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
