using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class ComparadorCategoria : IComparer<VendasCategoria>
    {
        public int Compare(VendasCategoria x, VendasCategoria y)
        {
            return x.NomeCategoria.CompareTo(y.NomeCategoria);
        }
    }
}
