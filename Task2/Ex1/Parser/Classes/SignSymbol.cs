using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Classes
{
    public abstract class SignSymbol: Symbol
    {
        public SignSymbol(char c)
            : base(c)
        {
        }
    }
}
