using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Classes;

namespace Parser.Interfaces
{
    public interface IWord: ISentenceItem, IEnumerable<Symbol>
    {
        Symbol this[int index] { get; }
        int Length { get; }
    }
}
