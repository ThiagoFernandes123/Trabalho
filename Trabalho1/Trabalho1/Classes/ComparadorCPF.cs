using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class ComparadorCPF : IComparer<Vendas>
    {
        public int Compare(Vendas x, Vendas y)
        {
            return x.CPF.CompareTo(y.CPF);
        }

        public int CompareVenda(Vendas x, Vendas y)
        {
            return x.VPreco.CompareTo(y.VPreco);
        }

        public int CompareCategorias(Vendas x, Vendas y)
        {
            return x.vCategoria.CompareTo(y.vCategoria);
        }

        public int CompareProduto(Vendas x, Vendas y)
        {
            return x.CodigoProduto.CompareTo(y.CodigoProduto);
        }

        public int CompareCliente(Vendas x, Vendas y)
        {
            return x.NomeCliente.CompareTo(y.NomeCliente);
        }
    }
}
