using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class ComparadorData : IComparer<VendasData>
    {
        public int Compare(VendasData x, VendasData y)
        {
            return x.Data.CompareTo(y.Data);
        }

        public int ComparePreco(VendasData x, VendasData y)
        {
            return x.Quantidade.CompareTo(y.Quantidade);
        }
    }
}
