using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class ComparadorPreco : IComparer<Vendas>
    {
        public int Compare(Vendas x, Vendas y)
        {
            return x.VPreco.CompareTo(y.VPreco);
        }
    }
}
