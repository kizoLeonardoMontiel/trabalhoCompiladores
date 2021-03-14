using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace TrabalhoCompiladores
{
    class Program
    {


        static void Main(string[] args)
        {
            int numeroLinha = 0;
            int countSimbolos = 0;
            List<String> tabelaDeToken = new List<String>();
            List<String> tabelaDeSimbolos = new List<String>();
            List<String> tabelaDeSimbolosBruto = new List<String>();
            List<String> erros = new List<String>();
            Console.WriteLine("Digite o caminho do arquivo");
            string caminho = Console.ReadLine();
           
            if (File.Exists(caminho))
            {
                string[] linhas = System.IO.File.ReadAllLines(@caminho);

                foreach (string linha in linhas)
                {
                    numeroLinha++;
                    if (VerificaPalavraReservada(linha))
                    {
                        tabelaDeToken.Add(new String("[" + numeroLinha + "] " + linha.ToUpper()));
                    }

                    else if (VerificaInteiro(linha))
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha))
                        {
                            
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO INTEIRO " + (tabelaDeSimbolosBruto.IndexOf(linha)+1)));
                        }
                        else
                        {
                            countSimbolos++;
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO INTEIRO " + countSimbolos));
                        }
                        
                    }
                    else if (VerificaDecimal(linha))
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha))
                        {
                            
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO REAL " + (tabelaDeSimbolosBruto.IndexOf(linha) + 1)));
                        }
                        else
                        {
                            countSimbolos++;
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "NÚMERO REAL " + countSimbolos));
                        }

                    }
                    else if (VerificaIdentificador(linha))
                    {
                        if (tabelaDeSimbolosBruto.Contains(linha))
                        {
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "IDENTIFICADOR " + (tabelaDeSimbolosBruto.IndexOf(linha) + 1)));
                        }
                        else
                        {
                            countSimbolos++;
                            tabelaDeSimbolos.Add(new String(countSimbolos + " - " + linha));
                            tabelaDeSimbolosBruto.Add(new String(linha));
                            tabelaDeToken.Add(new String("[" + numeroLinha + "] " + "IDENTIFICADOR " + countSimbolos));
                        }
                    }
                    else if (VerificaComentario(linha))
                    {
                        tabelaDeToken.Add(new string("[" + numeroLinha + "] " + "COMENTÁRIO"));
                    }
                    else
                    {
                        erros.Add(new string(numeroLinha.ToString() + " (" + linha + ")"));
                    }
                    
                }
                PrintTabelas(tabelaDeToken, tabelaDeSimbolos, erros);
            }
            else
            {
                Console.WriteLine("Arquivo não existe. Saindo...");
            }
        }

        static void PrintTabelas(List<String> tabelaDeToken, List<String> tabelaDeSimbolos, List<String> erros)
        {
            Console.WriteLine("Tabela de token:");
            foreach (String s in tabelaDeToken)
            {
                Console.WriteLine(s + "\n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Tabela de simbolos: \n");
            foreach (String s in tabelaDeSimbolos)
            {
                Console.WriteLine(s + "\n");
            }
            Console.WriteLine("Erros: \n");
            foreach (String s in erros)
            {
                Console.WriteLine(s + "\n");
            }
        }

        static bool VerificaIdentificador(String linha)
        {
            if (Regex.IsMatch(linha[0].ToString(), @"[a-zA-Z]$"))
            {
                foreach (char c in linha)
                {
                    if (!Regex.IsMatch(c.ToString(), @"[a-zA-Z0-9]$"))
                    {
                        return false;
                    }
                    if (Char.IsWhiteSpace(c))
                    {
                        break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        static Boolean VerificaPalavraReservada(String linha)
        {
            string[] palavras = {"int", "double", "float", "real", "break", "case", "char","const", "continue"};
            foreach(String palavra in palavras)
            {
                if (palavra.Equals(linha))
                {
                    return true;   
                }
            }
            return false;
        }
        static Boolean VerificaInteiro(String linha)
        {
            if(Regex.IsMatch(linha, @"[0-9]$") && linha.Length <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Boolean VerificaDecimal(String linha)
        {
            string[] decimais = linha.Split('.');
            if (decimais.Length == 2)
            {
                if (Regex.IsMatch(decimais[0], @"[0-9]$") && decimais[0].Length <= 2)
                {
                    if (Regex.IsMatch(decimais[1], @"[0-9]$") && decimais[1].Length <= 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static Boolean VerificaComentario(String linha)
        {
            if (linha.Length > 1 && linha[0].Equals('/') && linha[1].Equals('/'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
