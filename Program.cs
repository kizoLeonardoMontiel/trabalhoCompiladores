using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TrabalhoCompiladores
{
    class Program
    {
        void VerificaToken(String linha)
        {
            if(Regex.IsMatch(linha[0], @"^[a-zA-Z]+$")){
                Console.WriteLine("sucesso");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Digite o caminho do arquivo");
            string caminho = Console.ReadLine();
            
            if (File.Exists(caminho))
            {
                string[] linhas = System.IO.File.ReadAllLines(@caminho);

                foreach (string linha in linhas)
                {
                    VerificaToken(linha);
                }
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }

        }
    }
}
