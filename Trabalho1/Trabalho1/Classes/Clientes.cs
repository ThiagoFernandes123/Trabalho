using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    class Clientes
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public void ContaClienteRepetido()
        {
            try
            {
                var diferenteCliente = new HashSet<string>();

                using (StreamReader leitor = new StreamReader("clientes.txt"))
                {
                    string linha = leitor.ReadLine();

                    while (linha != null)
                    {
                        string cliente = linha.Substring(linha.IndexOf('|') + 1);
                        diferenteCliente.Add(cliente);
                        linha = leitor.ReadLine();
                    }

                    File.AppendAllText("dados.txt", "F - " + diferenteCliente.Count + Environment.NewLine);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no código");
            }
        }
    }
}
