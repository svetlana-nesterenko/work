using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleIEnumerable
{
    public class MyListEnum <T> : IEnumerator<T>
    {
        private T[] _List;
        private int _Position = -1;
        public MyListEnum(T[] list)
        {
            _List = list;
        }
        public T Current
        {
            get
            {
                try
                {
                    return _List[_Position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            _Position++;
            return (_Position < _List.Length);
        }

        public void Reset()
        {
             _Position = -1;
        }
    }
}
