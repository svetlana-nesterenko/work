using System.Linq;
using System.Text;
using ConcordanceModel.Core;

namespace ConcordanceModel.Core
{
    public class StringItem : BaseEnumerable<Word>
    {
        public override string ToString()
        {
            return (base.ToString() + " ").Trim();
        }
    }
}
