using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Interfaces
{
    public interface ISentence: ICollection<ISentenceItem>
    {
        string CreateSentance();
    }
}
