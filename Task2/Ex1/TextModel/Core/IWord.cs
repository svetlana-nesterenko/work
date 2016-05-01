using System.Collections.Generic;

namespace TextModel.Core
{
    public interface IWord : ISentenceItem, IEnumerable<Symbol>
    {
        Symbol this[int index] { get; }
    }
}
