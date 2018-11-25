using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class ComparadorProdutos : IComparer<VendasProduto>
    {
        public int Compare(VendasProduto x, VendasProduto y)
        {
            return x.descricaoProduto.CompareTo(y.descricaoProduto);
        }
    }
}
