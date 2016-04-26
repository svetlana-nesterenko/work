using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Interfaces
{
    public interface IParagraph : ICollection<ISentence>
    {
        double Indent { get; }
        double LineSpacing { get; }
    }
}
