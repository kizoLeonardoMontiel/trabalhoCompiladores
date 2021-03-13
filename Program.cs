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
            
            if (File.Exists(caminho))
            {
                string[] linhas = System.IO.File.ReadAllLines(@caminho);

                foreach (string linha in linhas)
                {
                    // Rodar método de verificação de sintaxe aqui
                }
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }

        }
    }
}
