using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class Program
    {
        private static string RetornaData(string data)
        {
            string ano = data.Substring(0, 4);
            data = data.Remove(0, 4);
            string mes = data.Substring(0, 2);

            string dataF = mes + "/" + ano;

            string dataFormato = Convert.ToDateTime(dataF).ToString("MM/yyyy");

            return dataFormato;
        }

        static void Main(string[] args)
        {
            Dictionary<int, Produtos> dicProduto = new Dictionary<int, Produtos>();
            Dictionary<int, Categorias> dicCategoria = new Dictionary<int, Categorias>();
            Dictionary<string, string> dicCliente = new Dictionary<string, string>();
            List<string> lVendas = new List<string>();

            int faltaProduto = 0;
            int faltaCliente = 0;
            int faltaCategoria = 0;

            #region OP-Code A
            try
            {
                string codigoCategoria;
                string descricaoCategoria;

                using (StreamReader leitor = new StreamReader("categorias.txt"))
                {
                    string linha = leitor.ReadLine();

                    while (linha != null)
                    {
                        codigoCategoria = linha.Substring(0, linha.IndexOf('|')).Trim();
                        descricaoCategoria = linha.Substring(linha.IndexOf('|') + 1);

                        if (!dicCategoria.ContainsKey(int.Parse(codigoCategoria)))
                        {
                            Categorias c = new Categorias();
                            c.Codigo = int.Parse(codigoCategoria);
                            c.Descricao = descricaoCategoria;

                            dicCategoria.Add(c.Codigo, c);
                        }

                        linha = leitor.ReadLine();
                    }
                }

                using (StreamWriter escrito = new StreamWriter("dados.txt"))
                {
                    escrito.WriteLine("A - " + dicCategoria.Count);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message.ToString());
            }
            #endregion

            #region OP-Code B
            try
            {
                string codigoProduto;
                string precoProduto;
                string descricaoProduto;
                string categoriaProduto;

                using (StreamReader leitor = new StreamReader("produtos.txt"))
                {
                    string linha = leitor.ReadLine();

                    while (linha != null)
                    {
                        codigoProduto = linha.Substring(0, linha.IndexOf('|'));
                        linha = linha.Remove(0, linha.IndexOf('|') + 1);
                        precoProduto = linha.Substring(0, linha.IndexOf('|'));
                        linha = linha.Remove(0, linha.IndexOf('|') + 1);
                        descricaoProduto = linha.Substring(0, linha.IndexOf('|'));
                        linha = linha.Remove(0, linha.IndexOf('|') + 1);
                        categoriaProduto = linha.Substring(0, linha.IndexOf('|'));

                        if (dicCategoria.ContainsKey(int.Parse(categoriaProduto)))
                        {
                            Produtos p = new Produtos();
                            p.Codigo = int.Parse(codigoProduto);
                            p.Preco = double.Parse(precoProduto);
                            p.Descricao = descricaoProduto;
                            p.Categoria = categoriaProduto;
                            dicProduto.Add(p.Codigo, p);
                        }

                        linha = leitor.ReadLine();
                    }
                }

                using (StreamWriter escrito = new StreamWriter("dados.txt", true))
                {
                    escrito.WriteLine("B - " + dicProduto.Count);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message.ToString());
            }
            #endregion

            #region OP-Code C
            try
            {
                string cpfCliente;
                string nomeCliente;

                using (StreamReader leitor = new StreamReader("clientes.txt"))
                {
                    string linha = leitor.ReadLine();

                    while (linha != null)
                    {
                        cpfCliente = linha.Substring(0, linha.IndexOf('|'));
                        nomeCliente = linha.Substring(linha.IndexOf('|') + 1);

                        if (!dicCliente.ContainsKey(cpfCliente))
                        {
                            Clientes c = new Clientes();
                            c.CPF = cpfCliente;
                            c.Nome = nomeCliente;
                            dicCliente.Add(cpfCliente, nomeCliente);
                        }

                        linha = leitor.ReadLine();
                    }
                }

                using (StreamWriter escrito = new StreamWriter("dados.txt", true))
                {
                    escrito.WriteLine($"C - {dicCliente.Count()}");
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message.ToString());
            }
            #endregion

            #region OP-Code D | OP-Code E
            try
            {
                var diferenteCod = new HashSet<int>();
                var diferenteProd = new HashSet<string>();

                using (StreamReader leitor = new StreamReader("vendas.txt"))
                {
                    string linha = leitor.ReadLine();

                    while (linha != null)
                    {
                        string lVenda = linha;

                        int codigo = Convert.ToInt32(linha.Substring(0, linha.IndexOf("|")));
                        linha = linha.Remove(0, linha.IndexOf('|') + 1);
                        string cliente = linha.Substring(0, linha.IndexOf('|'));
                        linha = linha.Remove(0, linha.IndexOf('|') + 1);
                        string produto = linha.Substring(0, linha.IndexOf('|'));

                        if (dicCliente.ContainsKey(cliente))
                        {
                            if (dicProduto.ContainsKey(Convert.ToInt32(produto)))
                            {
                                diferenteCod.Add(codigo);
                                diferenteProd.Add(produto);
                                lVendas.Add(lVenda);
                            }
                        }

                        linha = leitor.ReadLine();
                    }
                }

                using (StreamWriter escrito = new StreamWriter("dados.txt", true))
                {
                    escrito.WriteLine($"D - {diferenteCod.Count}" + Environment.NewLine + $"E - {diferenteProd.Count}");
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message.ToString());
            }
            #endregion

            #region OP-Code F
            Clientes nClientes = new Clientes();
            nClientes.ContaClienteRepetido();
            #endregion

            #region OP-Code G
            var comparadorCPF = new ComparadorCPF();
            string linhaAux;

            Dictionary<string, Vendas> VendaCliente = new Dictionary<string, Vendas>();

            foreach (string linha in lVendas)
            {
                linhaAux = linha.Remove(0, linha.IndexOf('|') + 1);

                string clienteV = linhaAux.Substring(0, linhaAux.IndexOf('|'));
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                int produtoV = int.Parse(linhaAux.Substring(0, linhaAux.IndexOf('|')));

                if (!VendaCliente.ContainsKey(clienteV))
                {
                    Vendas v = new Vendas();
                    v.NomeCliente = dicCliente[clienteV];
                    v.CPF = clienteV;
                    v.VPreco = dicProduto[produtoV].Preco;
                    VendaCliente.Add(v.CPF, v);
                }
                else
                {
                    VendaCliente[clienteV].VPreco += dicProduto[produtoV].Preco;
                }
            }

            List<Vendas> lV = VendaCliente.Values.ToList();
            lV.Sort(comparadorCPF);

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                foreach (Vendas i in lV)
                {
                    escritor.WriteLine("G - " + i.CPF + "|" + i.NomeCliente + "|" + i.VPreco);
                }
            }
            #endregion

            #region OP-Code H
            var comparadorProduto = new ComparadorProdutos();
            Dictionary<int, VendasProduto> vendaProduto = new Dictionary<int, VendasProduto>();

            foreach (string p in lVendas)
            {
                linhaAux = p.Remove(0, p.IndexOf('|') + 1);
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                int produtoV = Convert.ToInt32(linhaAux.Substring(0, linhaAux.IndexOf('|')));

                if (!vendaProduto.ContainsKey(produtoV))
                {
                    VendasProduto vp = new VendasProduto();
                    vp.codigoProduto = dicProduto[produtoV].Codigo;
                    vp.descricaoProduto = dicProduto[produtoV].Descricao;
                    vp.quantidadeProduto = 1;
                    vp.somaPrecoProduto = dicProduto[produtoV].Preco;
                    vendaProduto.Add(vp.codigoProduto, vp);
                }
                else
                {
                    vendaProduto[produtoV].quantidadeProduto += 1;
                    vendaProduto[produtoV].somaPrecoProduto += dicProduto[produtoV].Preco;
                }
            }

            List<VendasProduto> listaVendasProduto = vendaProduto.Values.ToList();
            listaVendasProduto.Sort(comparadorProduto);

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                foreach (VendasProduto i in listaVendasProduto)
                {
                    escritor.WriteLine("H - " + i.descricaoProduto + "|" + i.codigoProduto + "|" + i.quantidadeProduto + "|" + i.somaPrecoProduto);
                }
            }
            #endregion

            #region OP-Code I
            Dictionary<int, Vendas> VendaCategoria = new Dictionary<int, Vendas>();

            foreach (string linha in lVendas)
            {
                linhaAux = linha.Remove(0, linha.IndexOf('|') + 1);
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                int produtoV = int.Parse(linhaAux.Substring(0, linhaAux.IndexOf('|')));

                if (!VendaCategoria.ContainsKey(int.Parse(dicProduto[produtoV].Categoria)))
                {
                    Vendas v = new Vendas();
                    v.vCategoria = int.Parse(dicProduto[produtoV].Categoria);
                    v.NCategoria = dicCategoria[v.vCategoria].Descricao;
                    v.VPreco = dicProduto[produtoV].Preco;
                    VendaCategoria.Add(v.vCategoria, v);
                }
                else
                {
                    VendaCategoria[int.Parse(dicProduto[produtoV].Categoria)].VPreco += dicProduto[produtoV].Preco;
                }
            }

            List<Vendas> lvCategoria = VendaCategoria.Values.ToList();
            lvCategoria.Sort(comparadorCPF.CompareCategorias);

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                foreach (Vendas i in lvCategoria)
                {
                    escritor.WriteLine("I - " + i.NCategoria + '|' + i.vCategoria + '|' + i.VPreco);
                }
            }
            #endregion

            #region OP-Code J
            var comparadorData = new ComparadorData();
            Dictionary<DateTime, VendasData> vendasData = new Dictionary<DateTime, VendasData>();
            foreach (string i in lVendas)
            {
                linhaAux = i;
                int codigo = int.Parse(i.Substring(0, linhaAux.IndexOf('|')));
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                int produto = int.Parse(linhaAux.Substring(0, linhaAux.IndexOf('|')));
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                DateTime data = Convert.ToDateTime(RetornaData(linhaAux));

                if (!vendasData.ContainsKey(data))
                {
                    VendasData v = new VendasData();
                    v.Codigo = codigo;
                    v.Quantidade = dicProduto[produto].Preco;
                    v.Data = data;
                    vendasData.Add(data, v);
                }
                else
                {
                    vendasData[data].Quantidade += dicProduto[produto].Preco;
                }
            }

            List<VendasData> listaVendaData = vendasData.Values.ToList();
            listaVendaData.Sort(comparadorData);

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                foreach (VendasData i in listaVendaData)
                {
                    escritor.WriteLine("J - " + i.Data.ToString("MM/yyyy") + "|" + i.Quantidade);
                }
            }
            #endregion

            #region OP-Code K

            // O cliente que mais comprou
            // nome do cliente | valor

            List<Vendas> ListaClienteMaisCompou = VendaCliente.Values.ToList();
            ListaClienteMaisCompou.Sort(comparadorCPF.CompareCliente);
            string maiorComprador = "";
            for (int n = ListaClienteMaisCompou.Count - 1; n > ListaClienteMaisCompou.Count - 10; n--)
            {
                if (ListaClienteMaisCompou[n].VPreco < ListaClienteMaisCompou[n - 1].VPreco)
                {
                    maiorComprador = ListaClienteMaisCompou[n - 1].NomeCliente + "|" + ListaClienteMaisCompou[n - 1].VPreco;
                }
            }

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                escritor.WriteLine("K - " + maiorComprador);
            }
            #endregion

            #region OP-Code L
            List<Vendas> ListaMaisVendido = VendaCliente.Values.ToList();
            ListaMaisVendido.Sort(comparadorCPF.CompareProduto);
            string maiorVendedor = "";

            try
            {
                for (int n = ListaMaisVendido.Count - 1; n > ListaMaisVendido.Count - 10; n--)
                {
                    if (ListaMaisVendido[n].VPreco < ListaMaisVendido[n - 1].VPreco)
                    {
                        maiorVendedor = ListaMaisVendido[n - 1].CodigoProduto + "|" + ListaMaisVendido[n - 1].VPreco;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no Op-Code L");
            }

            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                escritor.WriteLine("L - " + maiorVendedor);
            }
            #endregion

            #region OP-Code M
            listaVendaData.Sort(comparadorData.ComparePreco);

            for (int i = listaVendaData.Count - 2; i >= 0; i--)
            {
                if (listaVendaData[i].Quantidade == listaVendaData[i + 1].Quantidade)
                {
                    using (StreamWriter escritor = new StreamWriter("dados.txt", true))
                    {
                        escritor.WriteLine($"M - {listaVendaData[i + 1].Data}|{listaVendaData[i + 1].Quantidade}");
                    }
                }
                else
                {
                    using (StreamWriter escritor = new StreamWriter("dados.txt", true))
                    {
                        escritor.WriteLine($"M - {listaVendaData[i + 1].Data}|{listaVendaData[i + 1].Quantidade}");
                    }
                    break;
                }
            }
            #endregion

            #region OP-Code N
            lV.Sort(comparadorCPF.CompareVenda);

            for (int i = lV.Count - 2; i >= 0; i--)
            {
                if (lV[i].VPreco == lV[i + 1].VPreco)
                {
                    using (StreamWriter escritor = new StreamWriter("dados.txt", true))
                    {
                        escritor.WriteLine($"N - {lV[i + 1].CodigoVenda}|{lV[i + 1].CPF}|{lV[i + 1].VPreco}");
                    }
                }
                else
                {
                    using (StreamWriter escritor = new StreamWriter("dados.txt", true))
                    {
                        escritor.WriteLine($"N - {lV[i + 1].CodigoVenda}|{lV[i + 1].CPF}|{lV[i + 1].VPreco}");
                    }
                    break;
                }
            }
            #endregion

            #region OP-Code O, OP-Code P
            foreach (string p in lVendas)
            {
                linhaAux = p.Remove(0, p.IndexOf('|') + 1);
                string cliente = linhaAux.Substring(0, linhaAux.IndexOf('|'));
                linhaAux = linhaAux.Remove(0, linhaAux.IndexOf('|') + 1);
                int produto = int.Parse(linhaAux.Substring(0, linhaAux.IndexOf('|')));
                int codigoCategoria = int.Parse(dicProduto[produto].Categoria);
                if (!dicProduto.ContainsKey(produto))
                {
                    faltaProduto++;
                }
                if (!dicCliente.ContainsKey(cliente))
                {
                    faltaCliente++;
                }
                if (!dicCategoria.ContainsKey(codigoCategoria))
                {
                    faltaCategoria++;
                }
            }
            using (StreamWriter escritor = new StreamWriter("dados.txt", true))
            {
                escritor.WriteLine($"O - {faltaProduto}");
                escritor.WriteLine($"P - {faltaCliente}");
                escritor.WriteLine($"Q - {faltaCategoria}");
            }
            #endregion

            uConsole.ReadLine();
        }
    }
}
