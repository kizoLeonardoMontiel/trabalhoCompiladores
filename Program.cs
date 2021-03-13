using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TrabalhoCompiladores
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Digite o caminho do arquivo");
            string caminho = Console.ReadLine();
            int numeroLinha = 0;
            if (File.Exists(caminho))
            {
                string[] linhas = System.IO.File.ReadAllLines(@caminho);

                foreach (string linha in linhas)
                {
                    numeroLinha++;
                    VerificaToken(linha, numeroLinha);
                }
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }

        }
        static void VerificaToken(String linha, int numeroLinha)
        {
            //Verificar se primeiro caracter eh uma letra:
            if (Regex.IsMatch(linha[0].ToString(), @"^[a-zA-Z]+$"))
            {
                foreach (char c in linha)
                {
                    if (!Regex.IsMatch(c.ToString(), @"^[a-zA-Z0-9]+$"))
                    {
                        break;
                    }
                }
                Console.WriteLine(numeroLinha + " Sucesso");
            }
            else
            {
                Console.WriteLine(numeroLinha + " Erro");
            }

        }
    }
}
