using System;
using System.IO;

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
                    // chamar uma função aqui
                }
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }

        }
    }
}
