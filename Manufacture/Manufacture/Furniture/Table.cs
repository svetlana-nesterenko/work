using Manufacture.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Furniture
{
    public abstract class Table : Furniture, ITable
    {
        public override string GenerateOrder()
        {
            return base.GenerateOrder();
        }
    }
}